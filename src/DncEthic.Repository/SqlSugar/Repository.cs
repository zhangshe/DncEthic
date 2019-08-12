using DncEthic.Core.Helper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace DncEthic.Repository.SqlSugar
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private DbContext _context;
        private SqlSugarClient _db;
        private SimpleClient<TEntity> _entityDb;
        /// <summary>
        /// SqlSugar上下文
        /// </summary>
        public DbContext Context
        {
            get { return _context; }
            set { _context = value; }
        }
        /// <summary>
        /// SqlSugar数据库对象
        /// </summary>
        internal SqlSugarClient Db
        {
            get { return _db; }
            private set { _db = value; }
        }
        /// <summary>
        /// SqlSugar实体对象
        /// </summary>
        internal SimpleClient<TEntity> entityDb
        {
            get { return _entityDb; }
            private set { _entityDb = value; }
        }
        public Repository()
        {
            #region 读取配置文件中的数据库连接配置
            string dbType = ConfigManager.Configuration["ConnectionStrings:DataType"];
            string connectionString = ConfigManager.Configuration[dbType];
            #endregion
            DbContext.Init(connectionString, (DbType)Enum.Parse(typeof(DbType), dbType));
            _context = DbContext.GetDbContext();
            _db = _context.Db;
            _entityDb = _context.GetEntityDB<TEntity>(_db);
        }

        #region 增

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>bool</returns>
        public async Task<bool> AddAsync(TEntity entity)
        {
            var i = await Task.Run(() => _db.Insertable(entity).ExecuteReturnBigIdentity());
            return i > 0;
        }

        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="parm">List<T>集合</param>
        /// <returns>bool</returns>
        public async Task<bool> AddListAsync(List<TEntity> model)
        {
            var i = await Task.Run(() => _db.Insertable(model.ToArray()).ExecuteCommand());
            return i > 0;
        }

        #endregion

        #region 删

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteByIdAsync(object id)
        {
            var i = await Task.Run(() => _db.Deleteable<TEntity>(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        ///  根据主键集合删除数据(批量删除)
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            var i = await Task.Run(() => _db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>>条件表达式</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteByConditionAsync(Expression<Func<TEntity, bool>> where)
        {
            var i = await Task.Run(() => _db.Deleteable<TEntity>().Where(where).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 根据实体对象删除数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var i = await Task.Run(() => _db.Deleteable(entity).ExecuteCommand());
            return i > 0;
        }

        #endregion

        #region 改

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => _db.Updateable(entity).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateListAsync(List<TEntity> parm)
        {
            var i = await Task.Run(() => _db.Updateable(parm.ToArray()).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i > 0;
        }

        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(TEntity entity, string strWhere)
        {
            return await Task.Run(() => _db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
        }

        /// <summary>
        /// 根据条件表达式更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where)
        {
            var i = await Task.Run(() => _db.Updateable<TEntity>().UpdateColumns(it => entity).Where(where).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 修改一条数据，也可用作逻辑删除
        /// </summary>
        /// <param name="columns">修改的列</param>
        /// <param name="where">条件表达式</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where)
        {
            //需要注意 当WhereColumns和UpdateColumns一起用时，需要把wherecolumns中的列加到UpdateColumns中
            var i = await Task.Run(() => _db.Updateable<TEntity>().UpdateColumns(columns).Where(where).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 执行更新sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="parameters">sqlsugar类型参数</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string strSql, SugarParameter[] parameters = null)
        {
            return await Task.Run(() => _db.Ado.ExecuteCommand(strSql, parameters) > 0);
        }

        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">更新列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdateAsync(
          TEntity entity,
          List<string> lstColumns = null,
          List<string> lstIgnoreColumns = null,
          string strWhere = ""
            )
        {
            IUpdateable<TEntity> up = await Task.Run(() => _db.Updateable(entity));
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = await Task.Run(() => up.Where(strWhere));
            }
            return await Task.Run(() => up.ExecuteCommand()) > 0;
        }

        #endregion

        #region 查
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByIdAsync(object objId)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().InSingle(objId));
        }
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByIdAsync(object objId, bool blnUseCache = false)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
        }

        /// <summary>
        /// 根据主键数组查询数据
        /// </summary>
        /// <param name="lstIds">主键数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDsAsync(object[] lstIds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().In(lstIds).ToList());
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync()
        {
            return await Task.Run(() => _entityDb.GetList());
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(string strWhere)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => _entityDb.GetList(whereExpression));
        }
        /// <summary>
        /// 查询数据列表（排序）
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToList());
        }

        /// <summary>
        /// 查询数据列表（排序表达式）
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序(默认true)</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).ToList());
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToList());
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="topNum">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>> whereExpression,
            int topNum,
            string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).Take(topNum).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToList());
        }

        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="topNum">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(
            string strWhere,
            int topNum,
            string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(topNum).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToList());
        }

        #region 默认分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>> whereExpression,
            int pageIndex = 0,
            int pageSize = 20,
            string strOrderByFileds = null)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(
          string strWhere,
          int pageIndex,
          int pageSize,
          string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 0, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(OrderByFiledsExpression != null, OrderByFiledsExpression).ToPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> QueryAsync(string strWhere, int pageIndex = 0, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).OrderByIF(OrderByFiledsExpression != null, OrderByFiledsExpression).ToPageList(pageIndex, pageSize));
        }

        #endregion

        #region 自定义分页查询

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序</param>
        /// <returns>自定义分页数据</returns>
        public async Task<PagedList<TEntity>> QueryPagedListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string strOrderByFileds = "")
        {
            var totalCount = 0;
            var list = Task.Run(() =>
                 {
                     var result = _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToPageList(pageIndex, pageSize, ref totalCount);
                     return new PagedList<TEntity>(result, pageIndex, pageSize, totalCount);
                 });
            return await list;
        }

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        public async Task<PagedList<TEntity>> QueryPagedListAsync(string strWhere, int pageIndex = 1, int pageSize = 20, string strOrderByFileds = "")
        {
            var totalCount = 0;
            var list = Task.Run(() =>
            {
                var result = _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).ToPageList(pageIndex, pageSize, ref totalCount);
                return new PagedList<TEntity>(result, pageIndex, pageSize, totalCount);
            });
            return await list;
        }

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        public async Task<PagedList<TEntity>> QueryPagedListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null)
        {
            var totalCount = 0;
            var list = Task.Run(() =>
            {
                var result = _db.Queryable<TEntity>().WhereIF(whereExpression != null, whereExpression).OrderByIF(OrderByFiledsExpression != null, OrderByFiledsExpression).ToPageList(pageIndex, pageSize, ref totalCount);
                return new PagedList<TEntity>(result, pageIndex, pageSize, totalCount);
            });
            return await list;
        }

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        public async Task<PagedList<TEntity>> QueryPagedListAsync(string strWhere, int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null)
        {
            var totalCount = 0;
            var list = Task.Run(() =>
            {
                var result = _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).OrderByIF(OrderByFiledsExpression != null, OrderByFiledsExpression).ToPageList(pageIndex, pageSize, ref totalCount);
                return new PagedList<TEntity>(result, pageIndex, pageSize, totalCount);
            });
            return await list;
        }
        #endregion


        #endregion

        #region DbFirst 生成实体类
        /// <summary>
        /// 生成数据库下所有表的实体类到制定路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool CreateEntity(string filePath)
        {
            try
            {
                _db.DbFirst.CreateClassFile(filePath, "DncEthic.Domain");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }

}
