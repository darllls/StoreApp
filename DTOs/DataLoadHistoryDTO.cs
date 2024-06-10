namespace DTOs
{
    public class DataLoadHistoryDTO
    {
        public int DataLoadHistoryId { get; set; }
        public int? LoadRows { get; set; }
        public DateTime? LoadDatetime { get; set; }
        public int? AffectedTableCount { get; set; }
        public int? SourceTableCount { get; set; }
        public int? LoadTime { get; set; }
    }
}
