namespace WorkshopBackend.Models
{
    public class BladeShape
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double totalLength { get; set; }
        public double bladeLength { get; set; }
        public double bladeWidth { get; set; }
        public double bladeWeight { get; set; }
        public double sharpeningAngle { get; set; }
        public double? rockwellHardnessUnits { get; set; }
        public double? engravingLocationX { get; set; }
        public double? engravingLocationY { get; set; }
        public double? engravingLocationZ { get; set; }
        public double? engravingRotationX { get; set; }
        public double? engravingRotationY { get; set; }
        public double? engravingRotationZ { get; set; }
        public string bladeShapeModelUrl { get; set; }
        public string sheathModelUrl { get; set; }
    }
}
