using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Garage_Management.Entities
{
    public partial class CarModel : DbContext
    {
        public CarModel()
            : base("name=CarModel")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillInfo> BillInfoes { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DaDatHang> DaDatHangs { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Suplier> Supliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Bill)
                .HasForeignKey(e => e.idBill)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .HasMany(e => e.Bills)
                .WithRequired(e => e.Car)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DaDatHang>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.DaDatHang)
                .HasForeignKey(e => e.idDatHang);

            modelBuilder.Entity<Staff>()
                .Property(e => e.phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Suplier>()
                .HasMany(e => e.BillInfoes)
                .WithRequired(e => e.Suplier)
                .HasForeignKey(e => e.idCarSuplier)
                .WillCascadeOnDelete(false);
        }
    }
}
