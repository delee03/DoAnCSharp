namespace Garage_Management.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        [Key]
        [StringLength(10)]
        public string idCar { get; set; }

        [Required]
        [StringLength(100)]
        public string nameCar { get; set; }

        public int? idSup { get; set; }

        public DateTime? ngayNhap { get; set; }

        public double price { get; set; }

        public int? idDatHang { get; set; }

        public virtual DaDatHang DaDatHang { get; set; }

        public virtual Suplier Suplier { get; set; }
    }
}
