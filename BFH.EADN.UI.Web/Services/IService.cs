using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.UI.Web.Services
{
    /// <summary>
    /// Interface which all CRUD service must implement
    /// </summary>
    /// <typeparam name="TViewModel">view model type</typeparam>
    /// <typeparam name="TKey">key type</typeparam>
    interface IService<TViewModel, TKey>
    {
        /// <summary>
        /// Get a list of items
        /// </summary>
        /// <returns></returns>
        List<TViewModel> GetList();

        /// <summary>
        /// Get a item by key
        /// </summary>
        /// <param name="id">key of type TKey</param>
        /// <returns>item of type TViewModel</returns>
        TViewModel Get(TKey id);

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="newElement">newElement of type TViewModel</param>
        void Create(TViewModel newElement);

        /// <summary>
        /// Edit item
        /// </summary>
        /// <param name="id">id of type TKey</param>
        /// <param name="element">element of type TViewModel</param>
        void Edit(TKey id, TViewModel element);

        /// <summary>
        /// Delete item by key
        /// </summary>
        /// <param name="id">id of type id</param>
        void Delete(TKey id);
    }
}
