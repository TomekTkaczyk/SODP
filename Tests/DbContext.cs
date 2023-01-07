using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Tests
{
    public class FakeDbSet<T> : DbSet<T>, IDbSet<T> where T : class
    {
        protected readonly List<T> _data;
        public FakeDbSet()
        {
            _data = new List<T>();
        }

        public override ObservableCollection<T> Local => base.Local;

        public Type ElementType => typeof(T);

        public Expression Expression => throw new NotImplementedException();

        public IQueryProvider Provider => throw new NotImplementedException();

        public override T Add(T entity)
        {
            return base.Add(entity);
        }

        public override T Attach(T entity)
        {
            return base.Attach(entity);
        }

        public override T Create()
        {
            return base.Create();
        }

        public override T Find(params object[] keyValues)
        {
            return base.Find(keyValues);
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override T Remove(T entity)
        {
            return base.Remove(entity);
        }

        TDerivedEntity IDbSet<T>.Create<TDerivedEntity>()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
