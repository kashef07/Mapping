using Mapping.Model;
using Microsoft.EntityFrameworkCore;

namespace Mapping.Data
{
    public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB>options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddresses> CustomersAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddresses>()
            .HasOne(_ => _.Customer)
            .WithMany(a => a.CustomerAddresses)
            .HasForeignKey(p => p.CustomerId);
        }
    }
}
