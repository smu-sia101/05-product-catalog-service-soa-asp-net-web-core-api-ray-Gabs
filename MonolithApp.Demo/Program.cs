using ProductsBLL.DI;
using ProductsDAL.DI;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.AddControllersWithViews();


string connectionString = "mongodb://localhost:27017"; 
string dbName = "products";
string collectionName = "productCollection"; 


services.AddProductsServices(connectionString, dbName, collectionName); 

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

app.Run();
