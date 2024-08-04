using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="Vui lòng chọn tên sản phẩm")]
        public int ProductID { get; set; }

        public Product Products { get; set; }
    }
}
