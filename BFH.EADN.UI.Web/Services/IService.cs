using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.UI.Web.Services
{
    interface IService<TContractType, TKey>
    {
        List<TContractType> GetList();
        TContractType Get(TKey id);
        void Create(TContractType newElement);
        void Edit(TKey id, TContractType element);
        void Delete(TKey id);
    }
}
