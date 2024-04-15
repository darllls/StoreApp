using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WarrantyMonths { get; set; }
        public string Description { get; set; }
        public string UsageInstructions { get; set; }
        public bool RecyclingInfo { get; set; }
        public decimal PackageLength { get; set; }
        public decimal PackageWidth { get; set; }
        public decimal PackageHeight { get; set; }
        public decimal PackageWeight { get; set; }
        public decimal Price { get; set; }
        public int ProductCategoryId { get; set; }
        public string Configuration { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public List<AttributeDTO> Attributes { get; set; }
        public List<AvailableProductDTO> AvailableProducts { get; set; }
    }

}
