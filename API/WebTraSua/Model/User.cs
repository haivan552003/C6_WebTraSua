using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WebTraSua.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Tên không được bỏ trống")]
        [MinLength(5, ErrorMessage = "Tên phải có ít nhất 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Tên chỉ có thể có tối đa 50 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được bỏ trống")]
        [MinLength(50, ErrorMessage = "Hình ảnh phải có ít nhất 50 ký tự")]
        [MaxLength(1000, ErrorMessage = "Hình ảnh chỉ có thể có tối đa 1000 ký tự")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        [MinLength(50, ErrorMessage = "Tài khoản phải có ít nhất 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Tài khoản chỉ có thể có tối đa 50 ký tự")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        [MinLength(5, ErrorMessage = "Mật khẩu phải có ít nhất 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Mật khẩu chỉ có thể có tối đa 50 ký tự")]
        [CustomValidation(typeof(PasswordValidator), "ValidatePassword")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public byte Gender { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được bỏ trống")]
        [RegularExpression(@"^(0[1-9][0-9]{8,9})$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và chỉ nhập được 10 hoặc 11 số")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
        [MinLength(5, ErrorMessage = "Địa chỉ phải có ít nhất 5 ký tự")]
        [MaxLength(50, ErrorMessage = "Địa chỉ chỉ có thể có tối đa 50 ký tự")]
        public string Address { get; set; }
        public int RoleID { get; set; }
        public Roles Role { get; set; }
        public ICollection<Bill> Bill { get; set; }
    }
    public static class PasswordValidator
    {
        public static ValidationResult ValidatePassword(string password, ValidationContext context)
        {
            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Mật khẩu không được bỏ trống");
            }

            if (password.Length < 5)
            {
                return new ValidationResult("Mật khẩu phải có ít nhất 5 ký tự");
            }

            if (password.Length > 50)
            {
                return new ValidationResult("Mật khẩu chỉ có thể có tối đa 50 ký tự");
            }

            if (Regex.IsMatch(password, @"\s"))
            {
                return new ValidationResult("Mật khẩu không được chứa khoảng trắng");
            }

            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]*$"))
            {
                return new ValidationResult("Mật khẩu không được chứa tiếng Việt");
            }

            return ValidationResult.Success;
        }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
