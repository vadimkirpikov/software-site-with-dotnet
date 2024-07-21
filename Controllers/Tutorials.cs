using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using test1.Models;

namespace test1.Controllers;

public class Tutorials : Controller
{
    private ProgrammSiteContext db;
    private readonly IMemoryCache _cache;

    public Tutorials(ProgrammSiteContext context, IMemoryCache cache)
    {
        db = context;
        _cache = cache;
    }

    [Route("{lang}/{section}/{shortname}")]
    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Client, NoStore = false)]
    public IActionResult About(string lang, string section, string shortname)
    {

        var url = $"/{lang}/{section}/{shortname}";
        var sectionUrl = $"/{lang}/{section}";
        
        var tutorials = GetTutorialsFromCache(sectionUrl);
        var allSections = GetSectionsFromCache();
        var chaptersMap = GetChapters(allSections.First(s => s.Url.Equals(sectionUrl)).Id);
        var tutorial = tutorials.First(t => t.Url.Equals(url));
        
        var indexOfTutorial = tutorials.IndexOf(tutorial);
        var nextTutorialUrl = indexOfTutorial < tutorials.Count - 1 ? tutorials[indexOfTutorial + 1].Url : "invalid";
        var previousTutorialUrl = indexOfTutorial > 0 ? tutorials[indexOfTutorial - 1].Url : "invalid";

        
        ViewBag.Chapters = chaptersMap;
        ViewBag.Sections = allSections;
        ViewBag.Title = tutorial.Title;
        ViewBag.Tutorial = tutorial;
        ViewBag.Tutorials = tutorials;
        ViewBag.BackLink = previousTutorialUrl;
        ViewBag.ForwardLink = nextTutorialUrl;
        ViewBag.Description = tutorial.Description;
        ViewBag.KeyWords = tutorial.Keywords;
        ViewBag.IsTutorial = true;
        ViewBag.Paragraph = lang;
        return View();
    }

    [Route("{lang}")]
    public IActionResult Main(string lang)
    {
        var langs = GetLangs();
        var Lang = langs.First(l => l.Name.Equals(lang));
        var allSections = GetSectionsFromCache();
        var sections = GetSectionsFromLang(Lang.Id);

        ViewBag.Description = Lang.Description;
        ViewBag.Keywords = Lang.Keywords;
        ViewBag.Sections = allSections;
        ViewBag.Title = Lang.Title;
        ViewBag.IsTutorial = false;
        ViewBag.Tutorials = sections;
        return View(sections);
    }

    [Route("")]
    public IActionResult Main()
    {
        var allLangs = GetLangs();
        var allSections = GetSectionsFromCache();
        ViewBag.Sections = allSections;
        ViewBag.Tutorials = allLangs;
        ViewBag.Title = "progvibe";
        ViewBag.IsTutorial = false;
        ViewBag.Keywords =
            "Программировать, программироование, как научиться, pyhon, c, c++, go, Go, Java, web, html, css, javascript, metanit, habr, метанит, хабр, питон, джава, веб, разарботка, стек оверфлоу, пхп";
        ViewBag.Description =
            "Современный сайт о программировании с большим количеством руководств для начинающих и не только";
        return View(allLangs);
    }

    [Route("{lang}/{section}")]
    public IActionResult Main(string lang, string section)
    {
        var tutorials = GetTutorialsFromCache($"/{lang}/{section}");
        var firstTutorial = tutorials[0];
        return Redirect(firstTutorial.Url);
    }
    
    private List<Tutorial>? GetTutorialsFromCache(string url)
    {
        if (!_cache.TryGetValue(url, out List<Tutorial>? tutorials))
        {
            Console.WriteLine("Uncached url " + url);
            tutorials = db.Tutorials.Where(t => t.Url.Contains(url)).ToList();
            _cache.Set(url, tutorials, TimeSpan.FromMinutes(30));
        }
        return tutorials;
    }

    private List<Section>? GetSectionsFromCache()
    {
        if (!_cache.TryGetValue("all_sections", out List<Section>? allSections))
        {
            allSections = db.Sections.ToList();
            _cache.Set("all_sections", allSections, TimeSpan.FromMinutes(30));
        }
        return allSections;
    }

    private List<Section>? GetSectionsFromLang(int langId) 
    {
        if (!_cache.TryGetValue(langId, out List<Section>? sections))
        {
            sections = db.Sections.Where(s => s.LangId.Equals(langId)).ToList();
            _cache.Set(langId, sections, TimeSpan.FromMinutes(30));
        }
        return sections;
    }

    private List<Lang>? GetLangs()
    {
        if (!_cache.TryGetValue("all_langs", out List<Lang>? langs))
        {
            langs = db.Langs.ToList();
            _cache.Set("all_langs", langs, TimeSpan.FromMinutes(30));
        }
        return langs;
    }

    private Dictionary<int, string>? GetChapters(int sectionId)
    {
        if (!_cache.TryGetValue(sectionId, out Dictionary<int, string>? chaptersMap))
        {
            var chapters = db.Chapters.Where(ch => ch.SectionId.Equals(sectionId));
            chaptersMap = new();
            foreach (var chapter in chapters)
                chaptersMap[chapter.ChapterId] = chapter.Title;
            _cache.Set(sectionId, chaptersMap, TimeSpan.FromMinutes(30));
        }

        return chaptersMap;
    }
}
