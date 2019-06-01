using Abc.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Abc.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T,bool>> filter=null);    //id,string tc, gibi farklı alanlara göre getirme işlemlerimiçin expression yapılır.Parametre geçmezse filter=null
        List<T> GetList(Expression<Func<T, bool>> filter = null); //null olursa tümünü listele
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
