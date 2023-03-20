using Microsoft.EntityFrameworkCore;

namespace AngularCrudeMaster.Context
{
    public class BCShopDBContext : DbContext
    {
        public BCShopDBContext()
        { }

        public BCShopDBContext(DbContextOptions<BCShopDBContext> options)
            : base(options)
        {
        }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookItem> BookItem { get; set; }
    }

}
