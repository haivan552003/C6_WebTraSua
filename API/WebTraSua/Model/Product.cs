using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace WebTraSua.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int CateID { get; set; }
        public byte StatusID { get; set; }

        public ICollection<BillDetail> BillDetail { get; set; }
        public ICollection<Size> Size { get; set; }
        public ICollection<Image> Image { get; set; }
        public Categories Categories { get; set; }
    }
}
