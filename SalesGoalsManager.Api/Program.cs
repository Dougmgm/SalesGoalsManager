using Microsoft.EntityFrameworkCore;
using SalesGoalsManager.RegraDeNegocio.Cadastro;
using SalesGoalsManager.RegraDeNegocio.Contexto;
using SalesGoalsManager.RegraDeNegocio.Interfaces;
using SalesGoalsManager.RegraDeNegocio.Repositorios;
using SalesGoalsManager.RegraDeNegocio.Validacoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SalesGoalsManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesGoalsManagerConnectionString")));

builder.Services.AddScoped<IMetaRepositorio, MetaRepositorio>();
builder.Services.AddScoped<MetaRepositorio>();

builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<ProdutoRepositorio>();

builder.Services.AddScoped<IVendedorRepositorio, VendedorRepositorio>();
builder.Services.AddScoped<VendedorRepositorio>();

builder.Services.AddScoped<MetaVendedorValidacao>();
builder.Services.AddScoped<CadastroProdutoValidacao>();
builder.Services.AddScoped<CadastroVendedorValidacao>();

builder.Services.AddScoped<MetaCadastro>();
builder.Services.AddScoped<ProdutoCadastro>();
builder.Services.AddScoped<VendedorCadastro>();

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