using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace Admin.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [MaxLength(250, ErrorMessage ="Vượt quá độ dài tối đa cho phép")]
        [MinLength(5, ErrorMessage ="Tên sản phẩm quá ngắn")]
        public string Name { get; set; }
        public float Rate { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống mô tả 1")]
        [MaxLength(200, ErrorMessage = "Vượt quá độ dài tối đa cho phép")]
        [MinLength(30, ErrorMessage = "Mô tả 1 quá ngắn")]
        public string Description1 { get; set; }

        [MaxLength(2000, ErrorMessage = "Vượt quá độ dài tối đa cho phép")]
        [MinLength(50, ErrorMessage = "Mô tả 2 quá ngắn")]
        public string Description2 { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        public int CateID { get; set; }
        public int StatusID { get; set; }

        public ICollection<Size_Product> Size_Product { get; set; }
        public ICollection<Image> Image { get; set; }
        public Categories Categories { get; set; }
    }
}
