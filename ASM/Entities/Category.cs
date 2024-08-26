namespace ASM.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; } 
        public  List<Product_Category>? Product_Category { get; set; } 
    }
}
