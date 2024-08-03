using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Model
{
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeID { get; set; }
        public string Name { get; set; }
        public ICollection<Size_Product> Size_Product { get; set; }
    }
}
