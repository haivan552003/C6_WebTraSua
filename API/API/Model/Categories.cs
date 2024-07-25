using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API.Model
{
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CateID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte Status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
