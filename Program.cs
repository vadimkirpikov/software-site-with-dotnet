
using Microsoft.EntityFrameworkCore;
using test1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddResponseCaching();
builder.Services.AddMvc();
string? connectionString1 = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProgrammSiteContext>(options=>options.UseMySQL(connectionString1));
var app = builder.Build();


app.UseExceptionHandler("/Error/page/was/not/found");
app.UseHsts();
app.UseResponseCaching();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.MapControllerRoute(
    name: "default",
    pattern: ""
);
app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404)
        {
            context.Response.Redirect("/Error/page/was/not/found");
        }
    }
);
app.Run();