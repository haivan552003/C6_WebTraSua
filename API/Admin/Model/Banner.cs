using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class Banner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hình ảnh chưa được nhập")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề chưa được nhập.")]
        [StringLength(30, ErrorMessage = "Không nhập quá 300 ký tự.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Trạng thái chưa được nhập")]
        [Range(0, 1, ErrorMessage = "Trạng thái")]
        public int Status { get; set; }
    }
}
