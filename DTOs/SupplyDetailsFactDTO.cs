namespace DTOs
{
    public class SupplyDetailsFactDTO
    {
        public int SupplyDetailsId { get; set; }
        public int? SupplyId { get; set; }
        public string SupplyNumber { get; set; }
        public DateTime? SupplyDate { get; set; }
        public string SupplyStatus { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryConcat { get; set; }
        public string Brand { get; set; }
        public int? UnitBuyDateDifference { get; set; }
    }
}
