using System.ComponentModel.DataAnnotations;

namespace test1.Models;

public interface IMetaTag
{
    public string? Keywords { get; }
    public string? Description { get; }
}