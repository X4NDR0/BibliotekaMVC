using Biblioteka.Interfaces;
using Biblioteka.Services;
using Biblioteka.Facades.SQL.Contracts;
using Biblioteka.Facades.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISqlData,SqlFacade>();
builder.Services.AddScoped<IBookstore,BookstoreService>();
builder.Services.AddScoped<IBook,BookService>();
builder.Services.AddScoped<IGenre,GenreService>();

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

//app.MapControllerRoute(
//    name: "Default",
//    pattern: "{controller=Bookstore}/{action=Bookstore/Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
