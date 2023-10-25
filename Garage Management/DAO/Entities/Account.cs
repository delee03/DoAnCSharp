namespace Garage_Management.DAO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public Account(string s) { 
        
        }

        public Account(string name,  string pass) 
        {
           this.UserName = name;
            this.PassWord = pass;
        }

        [Key]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(1000)]
        public string PassWord { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
