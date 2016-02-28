using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAppMVC.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        
        [DisplayName("Product")]
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        [DisplayName("Order Date")]
        [DataType(DataType.Date)]
        public DateTime OrderedDate { get; set; }
    }
}
