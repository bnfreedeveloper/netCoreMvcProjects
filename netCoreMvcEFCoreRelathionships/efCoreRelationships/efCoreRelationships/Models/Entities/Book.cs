namespace efCoreRelationships.Models.Entities
{
    public class Book
    {
        public Book()
        {
            Genres = new HashSet<Genre>();
        }
        public int Id { get; set; }
        public string Title { get; set; }   
        
        public ICollection<Genre>? Genres { get; set; }
    }
}
