using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace API.Model
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BillId { get; set; }
        public DateTime Date { get; set; }
        public float Total { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }

        public User User { get; set; }
        public Status Status { get; set; }

        public ICollection<BillDetail> BillDetail { get; set; }
    }
}
