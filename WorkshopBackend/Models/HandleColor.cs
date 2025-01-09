namespace WorkshopBackend.Models
{
    public class HandleColor
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string? ColorCode { get; set; }
        public string Material {  get; set; }
        public bool IsActive { get; set; } = true;
        public string? ColorMapUrl { get; set; }
        public string? NormalMapUrl { get; set; }
        public string? RoughnessMapUrl { get; set; }
    }
}
