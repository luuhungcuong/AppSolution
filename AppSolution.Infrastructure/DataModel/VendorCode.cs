namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VendorCode")]
    public partial class VendorCode
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(5)]
        public string TableID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string CodeID { get; set; }

        [StringLength(255)]
        public string CodeValue { get; set; }

        [StringLength(255)]
        public string Node { get; set; }
    }
}
