namespace DTOs
{
    public class ShipmentInvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public string StoreName { get; set; }
        public string EmployeeName { get; set; }
        public int AvailableProductId { get; set; }
    }
}