using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Garage_Management.Entities
{
    public partial class CarModel1 : DbContext
    {
        public CarModel1()
            : base("name=CarModel1")
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<DaDatHang> DaDatHangs { get; set; }
        public virtual DbSet<Suplier> Supliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DaDatHang>()
                .HasMany(e => e.Cars)
                .WithOptional(e => e.DaDatHang)
                .HasForeignKey(e => e.idDatHang);
        }
    }
}
