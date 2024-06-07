using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ShipmentInvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Total amount must be greater than 0")]
        public decimal? TotalAmount { get; set; }
        public string StoreName { get; set; }
        public string EmployeeName { get; set; }
        public int AvailableProductId { get; set; }
    }
}