using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

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
        /// 服务注册配置应用程序的服务This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region 添加Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1.1.0",
                    Title = "接口文档",
                    Description = "接口文档-平台",
                    TermsOfService = "",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Maverick", Email = "1312719913@qq.com", Url = "http://www.cnblogs.com/" }
                });
                c.SwaggerDoc("v2", new Info
                {
                    Version = "v1.1.0",
                    Title = "接口文档",
                    Description = "接口文档-业务",
                    TermsOfService = "",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Maverick", Email = "1312719913@qq.com", Url = "http://www.cnblogs.com/" }
                });
                //添加读取注释服务
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var apiXmlPath = Path.Combine(basePath, "DncEthic.WebAPI.xml");
                c.IncludeXmlComments(apiXmlPath, true);
                var entityXmlPath = Path.Combine(basePath, "DncEthic.WebAPI.xml");
                c.IncludeXmlComments(entityXmlPath, true);//控制器层注释（true表示显示控制器注释）
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
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "接口文档-业务");//业务接口文档首先显示
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "接口文档-平台");//基础接口文档放后面后显示
                c.RoutePrefix = string.Empty;//设置后直接输入IP就可以进入接口文档
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
