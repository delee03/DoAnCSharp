namespace Garage_Management.DAO.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillInfo")]
    public partial class BillInfo
    {
        public int id { get; set; }

        public int idBill { get; set; }

        public int idCarSuplier { get; set; }

        public int count { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Suplier Suplier { get; set; }
    }
}
