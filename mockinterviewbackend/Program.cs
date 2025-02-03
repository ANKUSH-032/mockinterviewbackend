



using Microsoft.OpenApi.Models;
using mockinterview.core.Interface;
using mockinterview.infrastructure;
using Mockinterview.Generic;
using OfficeOpenXml;
using System.ComponentModel;
using System.Text.Json;
using LicenseContext = OfficeOpenXml.LicenseContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
{
    config.Filters.Add(new ValidationFilter());
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

LicenseContext context = LicenseContext.NonCommercial; // Set the LicenseContext

ExcelPackage.LicenseContext = context;
// Add services to the container.
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200/")
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});
string MyAllowSpecificOrigins = "http://localhost:7003/api";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder
            .WithOrigins(MyAllowSpecificOrigins, "http://localhost:4200")
            // Specify the client app's origin
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            ); // Enable credentials
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.EnableAnnotations();
    setup.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeManagement.API", Version = "v1" });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                        jwtSecurityScheme, Array.Empty<string>() }
                });
});
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddScoped<IUserInterface, UserRepositories>();
builder.Services.AddScoped<IBulkImportRepository, BulkImportRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

app.Run();
