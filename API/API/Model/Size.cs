using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Model
{
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeID { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống.")]
        [StringLength(100, ErrorMessage = "Độ dài tên kích thước không thể lớn hơn 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mã sản phẩm không được bỏ trống.")]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }
    }
}
