namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FunctionRoles
    {
        [Key]
        [StringLength(50)]
        public string FunctionId { get; set; }

        [StringLength(128)]
        public string RoleId { get; set; }

        [StringLength(256)]
        public string Note { get; set; }
    }
}
