using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;

namespace WebTraSua.Model
{
    public class Size_Product
    {
        public int SizeProductID { get; set; }
        public int ProductID { get; set; }
        public int SizeID { get; set; }
        public float Price { get; set; }
        public Product Product { get; set; }
        public Size Size { get; set; }
    }
}
