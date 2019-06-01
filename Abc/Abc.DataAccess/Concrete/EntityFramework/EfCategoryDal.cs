using Abc.Core.DataAccess.EntityFramework;
using Abc.DataAccess.Abstract;
using Abc.Entities;

namespace Abc.DataAccess.Concrete.EntityFramework
{

    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
    }
}
