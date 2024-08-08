using System;

namespace WebTraSua.Model
{
    public class UserVM
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public DateTime Birthday { get; set; }
        public byte Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int RoleID { get; set; }
    }
}
