using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

      
        public string Image { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được bỏ trống.")]
        [StringLength(100, ErrorMessage = "Độ dài tiêu đề không thể lớn hơn 100 ký tự.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Trạng thái không được bỏ trống.")]
        [Range(0, 255, ErrorMessage = "Trạng thái phải nằm trong khoảng từ 0 đến 255.")]
        public byte Status { get; set; }
    }
}
