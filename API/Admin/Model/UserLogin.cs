using System.ComponentModel.DataAnnotations;

namespace Admin.Model
{
    public class UserLogin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string PassWord { get; set; }
        public int RoleID { get; set; }
    }
}
