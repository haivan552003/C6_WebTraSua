using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class ProductVM
    {
        public string Name { get; set; }
        public float Rate { get; set; } = 0;
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int CateID { get; set; }
        public byte StatusID { get; set; }
    }
}
