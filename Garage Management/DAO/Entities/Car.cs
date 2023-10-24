namespace Garage_Management.DAO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Car()
        {
            Bills = new HashSet<Bill>();
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(10)]
        public string idCar { get; set; }

        [Required]
        [StringLength(100)]
        public string nameCar { get; set; }

        [Column(TypeName = "image")]
        public byte[] imageCar { get; set; }

        public int? idSup { get; set; }

        public DateTime? ngayNhap { get; set; }

        public double price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill> Bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual Suplier Suplier { get; set; }
    }
}
