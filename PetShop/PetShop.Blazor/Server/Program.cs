using Microsoft.AspNetCore.ResponseCompression;
using PetShop.EF.Context;
using PetShop.EF.Repos;
using PetShop.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PetShopContext>();
builder.Services.AddScoped<IEntityRepo<Customer>,CustomerRepo>();
builder.Services.AddScoped<IEntityRepo<Pet>, PetRepo>();
builder.Services.AddScoped<IEntityRepo<PetFood>, PetFoodRepo>();
builder.Services.AddScoped<IEntityRepo<Employee>, EmployeeRepo>();
builder.Services.AddScoped<IEntityRepo<Transaction>, TransactionRepo>();
builder.Services.AddScoped<IEntityRepo<MonthlyLedger>, MonthlyLedgerRepo>();
builder.Services.AddScoped<IEntityRepo<PetReport>, PetReportRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
