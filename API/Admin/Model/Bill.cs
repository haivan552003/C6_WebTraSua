using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Generic;

namespace Admin.Model
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int BillId { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Số tiền chưa được cộng hoàn chỉnh, bạn cập nhật lại!")]
        public float Total { get; set; }
        [Required(ErrorMessage = "Hóa đơn chưa cập nhật được thông tin của bạn, hãy xác nhận lại!")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Bạn chưa chọn trạng thái của hóa đơn")]
        public int StatusID { get; set; }

        public User User { get; set; }
        public Status Status { get; set; }

        public ICollection<BillDetail> BillDetail { get; set; }
    }
}
