namespace Domain.Entities
{
    public class Cheese
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int PricePerKg { get; set; }
        public byte[] Image { get; set; }
    }
}
