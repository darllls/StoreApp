namespace DTOs
{
    public class SupplyDTO
    {
        public int SupplyId { get; set; }
        public string SupplyNumber { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal? Sum { get; set; }
        public int? SupplyStatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? SupplyDate { get; set; }
        public List<SupplyDetailsDTO> SupplyDetails { get; set; } = new List<SupplyDetailsDTO>();
    }

}