using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace DncEthic.Core.Repository
{
    /// <summary>
    /// 异步的方式实现仓储
    /// </summary>
    /// <typeparam name="TEntity">实体对象</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        #region 增
        /// <summary>
        /// 异步新增一条数据
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        Task<bool> Add(TEntity model);
        /// <summary>
        /// 异步批量添加数据
        /// </summary>
        /// <param name="parm">List<T>集合</param>
        /// <returns></returns>
        Task<bool> AddList(List<TEntity> parm);
        #endregion

        #region 删

        /// <summary>
        /// 根据主键删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        Task<bool> DeleteById(object id);

        /// <summary>
        ///  根据主键集合删除数据(批量删除)
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns></returns>
        Task<bool> DeleteByIds(object[] ids);

        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">Expression<Func<T, bool>>条件表达式</param>
        /// <returns></returns>
        Task<bool> DeleteByCondition(Expression<Func<TEntity, bool>> where);


       /// <summary>
       /// 根据实体对象删除数据
       /// </summary>
       /// <param name="model">实体对象</param>
       /// <returns></returns>
        Task<bool> Delete(TEntity model);

        #endregion

        #region 改

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity model);
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="parm">List<T></param>
        /// <returns></returns>
        Task<bool> UpdateList(List<TEntity> parm);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity, string strWhere);
        /// <summary>
        /// 根据条件更新多个数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 修改一条数据，也可用作逻辑删除
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> where);
        /// <summary>
        /// 根据条件修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lstColumns"></param>
        /// <param name="lstIgnoreColumns"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");


        #endregion

        #region 查
        Task<TEntity> FindById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);
        Task<List<TEntity>> FindAll();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);

        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);

        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);

        Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);
        #endregion
    }
}
