using DncEthic.Core.Enums;
using DncEthic.Core.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DncEthic.WebAPI
{
    /// <summary>
    /// 配置的服务和应用程序的请求管道
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 应用程序启动
        /// </summary>
        /// <param name="configuration">配置项</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 配置信息
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// API名称
        /// </summary>
        private const string ApiName = "DncEthic.Core";
        /// <summary>
        /// 服务注册配置应用程序的服务This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">服务</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region 添加Swagger
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            //var basePath = AppDomain.CurrentDomain.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                var enumGroup = typeof(ApiGroups);
                //遍历出全部的版本，做文档信息展示
                enumGroup.GetEnumNames().ToList().ForEach(name =>
                {
                    var value = Convert.ToInt32(Enum.Parse(enumGroup, name));
                    string description = EnumExtension.GetDescription(enumGroup, value);
                    c.SwaggerDoc(name, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = name,
                        Title = $"{ApiName} 接口文档",
                        Description = description,
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Dncthic.WebAPI", Email = "Dncthic.WebAPI@xxx.com", Url = "https://www.baidu.com" }
                    });
                    // 按相对路径排序
                    c.OrderActionsBy(o => o.RelativePath);
                });
                #region 获取接口描述并赋默认值（废弃）
                //c.DocInclusionPredicate((docName, description) =>
                //{
                //    description.TryGetMethodInfo(out MethodInfo mi);

                //    var attr = mi.DeclaringType.GetCustomAttribute<ApiExplorerSettingsAttribute>();
                //    if (attr != null)
                //    {
                //        return attr.GroupName == docName;
                //    }
                //    else {
                //        return attr.GroupName == "Default";
                //    }
                //    //else if(string.IsNullOrEmpty(groupName.ToString()))
                //    //{
                //    //    return docName == "Default";
                //    //}
                //    //if (!description.TryGetMethodInfo(out MethodInfo mi)) return false;
                //    //var groupName = mi.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(attr => attr.GroupName);
                //    //if (groupName.FirstOrDefault() == null)
                //    //{
                //    //    return docName == "Default";
                //    //}
                //    //else
                //    //{
                //    //    return groupName.Any(g => g.ToString() == docName);
                //    //}
                //});
                #endregion
                //添加读取注释服务
                var apiXmlPath = Path.Combine(basePath, "DncEthic.WebAPI.xml");//控制器层注释（true表示显示控制器注释）
                c.IncludeXmlComments(apiXmlPath, true);
                var entityXmlPath = Path.Combine(basePath, "DncEthic.Domain.xml");//实体层注释
                c.IncludeXmlComments(entityXmlPath, true);
                //添加对控制器的标签(描述)
                //c.DocumentFilter<SwaggerDocTag>();
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                c.AddSecurityRequirement(security);//添加一个必须的全局安全信息，和AddSecurityDefinition方法指定的方案名称要一致，这里是Bearer。
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 参数结构: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });
            });
            #endregion

            #region 跨域CORS
            services.AddCors(c =>
            {
                c.AddPolicy("Any", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
                c.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:8083")//运行跨越访问的请求地址么,有多个的话用逗号隔开
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithHeaders("authorization");
                });
            });
            #endregion
        }


        /// <summary>
        /// 创建应用程序的请求处理管道This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            #region 添加Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/G2/swagger.json", "API文档-业务");//业务接口文档首先显示
                //c.SwaggerEndpoint("/swagger/G1/swagger.json", "API文档-平台");//基础接口文档放后面后显示
                //c.SwaggerEndpoint("/swagger/Default/swagger.json", "API文档-默认");
                //c.RoutePrefix = string.Empty;//设置后直接输入IP就可以进入接口文档
                var enumGroup = typeof(ApiGroups);
                //根据分组名称倒序遍历展示
                enumGroup.GetEnumNames().OrderByDescending(p=> Convert.ToInt32(Enum.Parse(enumGroup, p))).ToList().ForEach(name =>
                {
                    var value = Convert.ToInt32(Enum.Parse(enumGroup, name));
                    string description = EnumExtension.GetDescription(enumGroup, value);
                    c.SwaggerEndpoint($"/swagger/{name}/swagger.json", $"{description}");
                });
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("DncEthic.WebAPI.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
                c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
            });
            #endregion

            app.UseMvc();

            #region 静态资源
            app.UseStaticFiles();//用于访问wwwroot下的文件 
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
            //    System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "ExcelModel")),
            //    RequestPath = "/ExcelModel"
            //});
            #endregion

            #region 解决Ubuntu Nginx 代理不能获取IP问题
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });
            #endregion
        }
    }
}
