using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class SupplyDetailsDTO
    {
        public int SupplyDetailsId { get; set; }
        public string ProductName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public int? Amount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than 0")]
        public decimal? PricePerUnit { get; set; }
        public List<ShipmentInvoiceDTO> ShipmentInvoices { get; set; } = new List<ShipmentInvoiceDTO>();
    }
}
