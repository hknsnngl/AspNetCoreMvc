using Abc.Core.DataAccess;  //Core katmanı
using Abc.Entities;         //Entities katmanı

namespace Abc.DataAccess.Abstract
{
    //Product için Repositorydeki tüm metodlar hazır olur
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
