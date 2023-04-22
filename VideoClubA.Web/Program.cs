using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoClubA.Common.Services;
using VideoClubA.Core.Interfaces;
using VideoClubA.Infrastucture.Data;
using VideoClubA.Web.Profiler;

var builder = WebApplication.CreateBuilder(args);


//Automapper
builder.Services.AddAutoMapper(typeof(MovieWithAvailabilityProfiler));

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("VideoClubDbContextConnection") ??
        throw new InvalidOperationException("Connection string 'VideoClubDbContextConnection' not found.");

builder.Services.AddDbContext<VideoClubDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieCopyService, MovieCopyService>();

builder.Services.AddSingleton<Dictionary<string, int>>(new Dictionary<string, int>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{

    endpoints.MapAreaControllerRoute(
        name: "Movies",
        areaName: "Movies",
        pattern: "Movies/{controller=Home}/{action=Index}"
    );

    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller}/{action}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();