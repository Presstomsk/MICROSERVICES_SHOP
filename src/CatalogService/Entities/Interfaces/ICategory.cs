namespace CatalogService.Entities.Interfaces
{
    public interface ICategory
    {
        int Id { get; set; }
        string? Name { get; set; }
        string? Url { get; set; }
        string? Icon { get; set; }
    }
}