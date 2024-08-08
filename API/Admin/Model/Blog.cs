namespace Admin.Model
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations;

    public class Blog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được bỏ trống")]
        [MinLength(5, ErrorMessage = "Tiêu đề phải có ít nhất 5 ký tự")]
        [MaxLength(250, ErrorMessage = "Tiêu đề chỉ có thể có tối đa 250 ký tự")]
        public string Title { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Nội dụng không được bỏ trống")]
        [MinLength(50, ErrorMessage = "Nội dụng phải có ít nhất 50 ký tự")]
        [MaxLength(1000, ErrorMessage = "Nội dụng có chỉ thể có tối đa 1000 ký tự")]
        public string Decription { get; set; }
    }


}
