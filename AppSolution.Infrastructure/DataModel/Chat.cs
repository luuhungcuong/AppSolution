namespace AppSolution.Infrastructure.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chat")]
    public partial class Chat
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

        [Required]
        [StringLength(256)]
        public string Message { get; set; }

        public int Status { get; set; }
    }
}
