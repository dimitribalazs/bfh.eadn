using System;
using BFH.EADN.Common.Types;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace BFH.EADN.Persistence.EF.Repositories
{
    public abstract class BaseRepository<T, K> : IRepository<T, K>
    {
        protected QuizDataContext Context { get; } = new QuizDataContext();
        public abstract void Create(T data);
        public abstract void Delete(K Id);
        public abstract T Get(K Id);
        //public abstract T Get(Func<T, bool> expr);
        public abstract List<T> GetAll();
        public abstract void Update(T data);
        public void Dispose()
        {
            Context?.Dispose();
        }

    }
}
