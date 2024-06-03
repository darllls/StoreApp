namespace DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public string Number { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public string StoreName { get; set; }
        public string CityName { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
