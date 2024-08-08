using System.ComponentModel.DataAnnotations;

namespace WebTraSua.Model
{
    public class UserLogin
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string PassWord { get; set; }
        public int RoleID { get; set; }
    }
}
