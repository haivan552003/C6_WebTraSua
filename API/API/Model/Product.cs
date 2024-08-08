using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace API.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập tên sản phẩm")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Tên sản phẩm không hợp lệ")]
        public string Name { get; set; }
        public float Rate { get; set; } = 0;
        [Required(ErrorMessage = "Bạn chưa nhập mô tả sản phẩm")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Mô tả sản phẩm từ 50 -> 1000 ký tự")]
        public string Description1 { get; set; }
        [Required(ErrorMessage = "Bạn chưa nhập mô tả sản phẩm")]
        [StringLength(1000, MinimumLength = 50, ErrorMessage = "Mô tả sản phẩm từ 50 -> 1000 ký tự")]
        public string Description2 { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn loại sản phẩm")]
        public int CateID { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn trạng thái của sản phẩm")]
        public byte StatusID { get; set; }

        public ICollection<BillDetail> BillDetail { get; set; }
        public ICollection<Size> Size { get; set; }
        public ICollection<Image> Image { get; set; }
        public Categories Categories { get; set; }
    }
}
