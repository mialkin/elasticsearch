namespace Elasticsearch.Api.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public bool? IsInStock { get; set; }
    public decimal Price { get; set; }
    public decimal? Discount { get; set; }
    public DateTime CreatedOn { get; set; }
}