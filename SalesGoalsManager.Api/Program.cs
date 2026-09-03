using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.Infrastructure.Data;
using SalesGoalsManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SalesGoalsManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesGoalsManagerConnectionString")));

builder.Services.AddScoped<MetaRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<VendedorRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();