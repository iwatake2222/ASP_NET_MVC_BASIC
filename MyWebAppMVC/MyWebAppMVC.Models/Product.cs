using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAppMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [DisplayName("Unit Price")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal UnitPrice { get; set; }
        [MaxLength(255)]
        public string MakerName { get; set; }

        // child tables
        public virtual ICollection<Order> Orders { get; set; }
    }
}
