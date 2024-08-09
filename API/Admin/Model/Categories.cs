using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Admin.Model
{
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CateID { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống.")]
        [StringLength(100, ErrorMessage = "Độ dài tên không thể lớn hơn 100 ký tự.")]
        public string Name { get; set; }

      
        public string Image { get; set; }

        [Required(ErrorMessage = "Trạng thái không được bỏ trống.")]
        [Range(0, 255, ErrorMessage = "Trạng thái phải nằm trong khoảng từ 0 đến 255.")]
        public byte Status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
