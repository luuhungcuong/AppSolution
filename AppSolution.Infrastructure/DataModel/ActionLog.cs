namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ActionLog")]
    public partial class ActionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TranID { get; set; }

        [Required]
        [StringLength(256)]
        public string UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string ActionID { get; set; }

        public DateTime TranDate { get; set; }
    }
}
