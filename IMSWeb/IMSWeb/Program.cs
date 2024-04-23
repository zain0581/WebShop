using IMSWeb.Dal;
using IMSWeb.Interface;
using IMSWeb.Repo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ICategory, CategoryRepo>();
builder.Services.AddScoped<IInventoryItem, InventoryItemsRepo>();
builder.Services.AddScoped<IOrder,OrderRepo>();
builder.Services.AddScoped<ISupplier, SupplierRepo>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("coffe",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                                 
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});

builder.Services.AddDbContext<IMSContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connectionToDB"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("coffe");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
