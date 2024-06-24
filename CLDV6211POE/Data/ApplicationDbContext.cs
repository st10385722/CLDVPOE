using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CLDV6211POE.Models;

namespace CLDV6211POE.Data
{
    //context class
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CLDV6211POE.Models.Products> Products { get; set; } = default!;
        public DbSet<CLDV6211POE.Models.Orders> Orders { get; set; } = default!;
        public DbSet<CLDV6211POE.Models.AdminKey> AdminKey { get; set; } = default!;
        public DbSet<CLDV6211POE.Models.PreviousOrders> PreviousOrders { get; set; } = default!;
        public DbSet<CLDV6211POE.Models.UserViewModel> UserViewModel { get; set; } = default!;


    }
}
