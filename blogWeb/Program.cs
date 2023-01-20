using blogWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
var myConString = builder.Configuration.GetConnectionString("myString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(myConString));
var app = builder.Build();
if(!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler();
	app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());


app.Run();
