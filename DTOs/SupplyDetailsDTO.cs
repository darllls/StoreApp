namespace DTOs
{
    public class SupplyDetailsDTO
    {
        public int SupplyDetailsId { get; set; }
        public string ProductName { get; set; }
        public int? Amount { get; set; }
        public decimal? PricePerUnit { get; set; }
        public List<ShipmentInvoiceDTO> ShipmentInvoices { get; set; } = new List<ShipmentInvoiceDTO>();
    }
}
