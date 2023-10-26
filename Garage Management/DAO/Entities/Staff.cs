namespace Garage_Management.DAO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        [StringLength(10)]
        public string id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Avartar_image { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(11)]
        public string phone { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }
    }
}
