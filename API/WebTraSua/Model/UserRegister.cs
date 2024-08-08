using System.ComponentModel.DataAnnotations;

namespace WebTraSua.Model
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        [Compare("PassWord", ErrorMessage = "Mật khẩu không khớp")]
        public string RePass { get; set; }
        public int RoleID { get; set; }
    }
}
