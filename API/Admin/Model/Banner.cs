using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class Banner
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề chưa được nhập.")]
        [StringLength(300, ErrorMessage = "Không nhập quá 300 ký tự.")]
        public string Title { get; set; }
        [Range(0, 1, ErrorMessage = "Trạng thái")]
        public int Status { get; set; }
    }
}
