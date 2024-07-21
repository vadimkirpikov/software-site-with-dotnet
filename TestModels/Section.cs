using System;
using System.Collections.Generic;

namespace test1.TestModels;

public class Section: IDescription
{
    public int Id { get; }

    public string? Name { get; }

    public string? Title { get; }

    public string? Preview { get; }

    public int? LangId { get; }
    public string? Url { get; }
}
