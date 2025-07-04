using Marisa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Marisa.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options) 
        {
            _configuration = configuration;
            Users = Set<User>();
            Address = Set<Address>();
            ProductAdditionalInfos = Set<ProductAdditionalInfo>();
            UserProductLikes = Set<UserProductLike>();
            ProductComments = Set<ProductComment>();
            ProductCommentLikes = Set<ProductCommentLike>();
        }
        
        public DbSet<User> Users { get; private set; }
        public DbSet<Address> Address { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<ProductAdditionalInfo> ProductAdditionalInfos { get; private set; }
        public DbSet<UserProductLike> UserProductLikes { get; private set; }
        public DbSet<ProductComment> ProductComments { get; private set; }
        public DbSet<ProductCommentLike> ProductCommentLikes { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
