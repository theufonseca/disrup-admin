using Domain.Interfaces;
using Infra.Elasticsearch;
using Infra.MySql;
using Infra.MySql.Services;
using Infra.RabbitMQ.Services;
using Infra.Storage;
using Infra.Storage.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nest;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});


// Add services to the container.
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<GoogleCloudStorage>();
builder.Services.AddSingleton<IPhotoStorageService, PhotoStorageService>();
builder.Services.AddSingleton<ICompanyService, CompanyService>();
builder.Services.AddSingleton<ICompanyPhotoService, CompanyPhotoService>();
builder.Services.AddSingleton<ICompanyNotificationService, CompanyNotificationService>();
builder.Services.AddSingleton<ICompanySearchService, CompanySearchService>();

builder.Services.AddDbContext<DataContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

//Configure Elasticsearch
ElasticsearchSettings.IndexName = builder.Configuration.GetSection("ElasticsearchSettings:IndexName").Value!;
ElasticsearchSettings.Url = builder.Configuration.GetSection("ElasticsearchSettings:Url").Value!;

builder.Services.AddSingleton<IElasticClient>(s =>
{
    var settings = new ConnectionSettings(new Uri(ElasticsearchSettings.Url));
    return new ElasticClient(settings);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.CreateTables();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();