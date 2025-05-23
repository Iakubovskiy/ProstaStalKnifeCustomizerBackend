namespace API.DTO
{
    public class KnifeDTO
    {
        public Guid ShapeId { get; set; }
        public Guid BladeCoatingColorId { get; set; }
        public Guid HandleColorId { get; set; }
        public Guid SheathColorId { get; set; }
        public Guid? FasteningId { get; set; }
        public string? EngravingsJson { get; set; }
    }
}
