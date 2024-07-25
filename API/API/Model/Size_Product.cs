using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace API.Model
{
    public class Size_Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int SizeProductID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public float Price { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
        public ICollection<BillDetail> billDetail { get; set; }
    }
}
