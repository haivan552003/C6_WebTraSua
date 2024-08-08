using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebTraSua.Model
{
    public class BillDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailID { get; set; }
        public int BillID { get; set; }
        public int ProductID { get; set; }
        public int Quality { get; set; }
        public float Subtotal { get; set; }
        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
