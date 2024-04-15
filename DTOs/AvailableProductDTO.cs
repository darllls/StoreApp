using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AvailableProductDTO
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; }
    }
}
