namespace efCoreRelationships.Models.Entities
{
    public class Categorie
    {
        public Categorie()
        {
           Products = new HashSet<Product>();  
        }
        public int Id { get; set; } 
        
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
