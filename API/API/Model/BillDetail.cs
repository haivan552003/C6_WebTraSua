using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace API.Model
{
    public class BillDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillDetailID { get; set; }
        public int BillID { get; set; }
        [Required(ErrorMessage = "Hóa đơn không nhận được thông tin sản phẩm, bạn hãy kiểm tra lại!!")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Hóa đơn không nhận được thông tin số lượng sản phẩm, bạn hãy kiểm tra lại!!")]
        public int Quality { get; set; }
        [Required(ErrorMessage = "Số tiền chưa được cộng hoàn chỉnh, bạn cập nhật lại!")]
        public float Subtotal { get; set; }
        public Product Product { get; set; }
        public Bill Bill { get; set; }
    }
}
