using System;
using System.Collections.Generic;

namespace test1.Models;

public class Tutorial: IMetaTag
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public string? Text { get; set; }

    public int? ChapterId { get; set; }

    public string? Description { get; set; }

    public string? Keywords { get; set; }
}
