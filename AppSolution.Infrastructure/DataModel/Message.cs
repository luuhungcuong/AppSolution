namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public long ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Sender { get; set; }

        [Required]
        [StringLength(256)]
        public string Reciever { get; set; }

        public DateTime TranDate { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Column("Message")]
        [Required]
        [StringLength(256)]
        public string Message1 { get; set; }

        public int Status { get; set; }
    }
}
