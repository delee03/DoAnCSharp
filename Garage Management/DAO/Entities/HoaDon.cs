namespace Garage_Management.DAO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [StringLength(10)]
        public string idHoaDon { get; set; }

        [Required]
        [StringLength(100)]
        public string tenKH { get; set; }

        [Required]
        [StringLength(11)]
        public string sdt { get; set; }

        [Required]
        [StringLength(50)]
        public string tenNV { get; set; }

        [Required]
        [StringLength(10)]
        public string idCar { get; set; }

        [Column(TypeName = "image")]
        public byte[] imageCar { get; set; }

        public DateTime? ngayLap { get; set; }

        public virtual Car Car { get; set; }
    }
}
