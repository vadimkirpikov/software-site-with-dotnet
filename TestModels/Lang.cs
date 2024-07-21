using System;
using System.Collections.Generic;

namespace test1.TestModels;

public class Lang: IDescription
{
    public int Id { get;}

    public string? Name { get; }

    public string? Title { get; }

    public string? Preview { get; }
    public string? Url { get; }
}
