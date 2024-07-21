using System;
using System.Collections.Generic;

namespace test1.TestModels;

public class Chapter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public int? SectionId { get; set; }

    public int? ChapterId { get; set; }
}
