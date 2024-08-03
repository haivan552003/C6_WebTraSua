using System;

namespace Admin.Model
{
    public class BillVM
    {
        public DateTime Date { get; set; }
        public float Total { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
    }
}
