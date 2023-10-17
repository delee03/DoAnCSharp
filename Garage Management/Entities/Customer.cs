namespace Garage_Management.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        public bool sex { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(11)]
        public string Phone { get; set; }
    }
}
