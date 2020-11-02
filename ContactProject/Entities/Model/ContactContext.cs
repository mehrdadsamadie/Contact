using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Entities.Model
{
    public class ContactContext: DbContext
    {

        public ContactContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(d => d.ContactInfo)
                .WithMany(p => p.Addresses)
                .HasForeignKey(d => d.ContactId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ContactInfo>();
        }
    }
}
