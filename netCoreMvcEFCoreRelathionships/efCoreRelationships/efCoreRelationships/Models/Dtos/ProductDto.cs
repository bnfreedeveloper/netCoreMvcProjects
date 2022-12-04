namespace efCoreRelationships.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }    
        public decimal? Price { get; set; }
        public string? CategoryName { get; set; } 
        public int CategoryId { get; set; } 
    }
}
