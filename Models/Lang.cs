namespace test1.Models;

public class Lang: IMetaTag, IDescription
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public string? Preview { get; set; }

    public string? Url { get; set; }

    public string? Keywords { get; set; }

    public string? Description { get; set; }
}
