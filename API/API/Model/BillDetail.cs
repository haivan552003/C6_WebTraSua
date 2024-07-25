using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API.Model
{
    public class BillDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailID { get; set; }
        public int BillID { get; set; }
        public int SizeProductID { get; set; }
        public int Quality { get; set; }
        public float Subtotal { get; set; }
        public Size_Product sizeProduct { get; set; }
        public Bill Bill { get; set; }
    }
}
