using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <returns>Entity of type T</returns>
        T Get(K Id);

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
    }
}
