namespace DTOs
{
    public class OrderDetailsFactDTO
    {
        public int OrderDetailsId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryConcat { get; set; }
        public string Brand { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int? CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public decimal? DiscountRate { get; set; }
        public int? OrderCount { get; set; }
        public DateTime? PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
        public decimal? AverageCustomerSum { get; set; }
        public decimal? AverageSalesQuantityChangePercentage { get; set; }
    }
}
