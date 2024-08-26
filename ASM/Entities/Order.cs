namespace ASM.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
