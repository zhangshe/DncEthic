using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DncEthic.Repository
{
    /// <summary>
    /// 异步的方式实现仓储
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region 增
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns>bool</returns>
        Task<bool> AddAsync(TEntity model);
        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="parm">List<T>集合</param>
        /// <returns>bool</returns>
        Task<bool> AddListAsync(List<TEntity> parm);
        #endregion

        #region 删

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>bool</returns>
        Task<bool> DeleteByIdAsync(object id);

        /// <summary>
        ///  根据主键集合删除数据(批量删除)
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns>bool</returns>
        Task<bool> DeleteByIdsAsync(object[] ids);

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>>条件表达式</param>
        /// <returns>bool</returns>
        Task<bool> DeleteByConditionAsync(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// 根据实体对象删除数据
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(TEntity model);

        #endregion

        #region 改

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns>bool</returns>
        Task<bool> UpdateListAsync(List<TEntity> parm);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(TEntity entity, string strWhere);
        /// <summary>
        /// 根据条件表达式更新数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="where">条件表达式</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 修改一条数据，也可用作逻辑删除
        /// </summary>
        /// <param name="columns">修改的列</param>
        /// <param name="where">条件表达式</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">更新列</param>
        /// <param name="lstIgnoreColumns">忽略列</param>
        /// <param name="strWhere">条件</param>
        /// <returns>bool</returns>
        Task<bool> UpdateAsync(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        #endregion

        #region 查
        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns>数据实体</returns>
        Task<TEntity> QueryByIdAsync(object objId);
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        Task<TEntity> QueryByIdAsync(object objId, bool blnUseCache = false);
        /// <summary>
        /// 根据主键数组查询数据
        /// </summary>
        /// <param name="lstIds">主键数组（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        Task<List<TEntity>> QueryByIDsAsync(object[] lstIds);
        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync();
        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(string strWhere);
        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 查询数据列表（排序）
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        /// <summary>
        /// 查询数据列表（排序表达式）
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="isAsc">是否升序(默认true)</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        /// <summary>
        /// 查询一个列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(string strWhere, string strOrderByFileds);
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="topNum">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, int topNum, string strOrderByFileds);
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="topNum">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(string strWhere, int topNum, string strOrderByFileds);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>> whereExpression, int pageIndex, int pageSize, string strOrderByFileds);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(string strWhere, int pageIndex, int pageSize, string strOrderByFileds);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>数据列表</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 0, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null);

        /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        Task<List<TEntity>> QueryAsync(string strWhere, int pageIndex = 0, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null);

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序</param>
        /// <returns>自定义分页数据</returns>
        Task<PagedList<TEntity>> QueryPagedListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string strOrderByFileds = "");

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="strOrderByFileds">排序</param>
        /// <returns>自定义分页数据</returns>
        Task<PagedList<TEntity>> QueryPagedListAsync(string strWhere, int pageIndex = 1, int pageSize = 20, string strOrderByFileds = "");

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        Task<PagedList<TEntity>> QueryPagedListAsync(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, Expression<Func<TEntity, object>> OrderByFiledsExpression = null);

        /// <summary>
        /// 分页查询-自定义分页
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="OrderByFiledsExpression">排序表达式</param>
        /// <returns>自定义分页数据</returns>
        Task<PagedList<TEntity>> QueryPagedListAsync(string strWhere, int pageIndex = 1, int pageSize = 20,Expression<Func<TEntity, object>> OrderByFiledsExpression = null);

        #endregion

        #region DbFirst 生成实体类
        /// <summary>
        /// 生成数据库下所有表的实体类到制定路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool CreateEntity(string filePath);
        #endregion

    }
}
