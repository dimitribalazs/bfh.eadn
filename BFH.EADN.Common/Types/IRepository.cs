using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types
{
    public interface IRepository<T, K> : IDisposable
    {
        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="data">Data of generic type T</param>
        void Create(T data);

        /// <summary>
        /// Get entity by its id
        /// </summary>
        /// <param name="Id">Id of type K</param>
        /// <returns>Entity of type T or null</returns>
        T Get(K Id);

        /// <summary>
        /// Get entity by an expression
        /// </summary>
        /// <param name="Id">Id of type K</param>
        /// <returns>Entity of type T</returns>
        //T Get(Func<T, bool> expr);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of entities of type T</returns>
        List<T> GetAll();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="data">Datas to update</param>
        void Update(T data);

        /// <summary>
        /// Delete entity by its id
        /// </summary>
        /// <param name="Id">Id of type K</param>
        void Delete(K Id);

        /// <summary>
        /// Get a list of elements by its ids
        /// </summary>
        /// <param name="ids">list of ids of the elements</param>
        /// <returns>list of elements of type T</returns>
        List<T> GetListByIds(List<K> ids);
    }
}
