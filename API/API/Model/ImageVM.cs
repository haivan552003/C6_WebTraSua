using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class ImageVM
    {
        public string Name { get; set; }
        public int ProductID { get; set; }
    }
}
