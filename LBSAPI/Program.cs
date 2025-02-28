using BusinessObject;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Repository;
using Repositories;
using Repositories.IRepository;
using ImgurAPI.Core;

var builder = WebApplication.CreateBuilder(args);


var mongoSettings = builder.Configuration.GetSection("MongoSettings").Get<MongoSettings>();

builder.Services.AddSingleton<LBSMongoDBContext>(sp =>
    new LBSMongoDBContext(mongoSettings.ConnectionString, mongoSettings.DatabaseName));

builder.Services.AddDbContext<LBSDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LBSConnection")));


builder.Services.AddSingleton<EmailSender, EmailSender>();
builder.Services.AddScoped<ImageManager>();
builder.Services.AddScoped<Imgur>(c =>
{
    return new Imgur("577b7aebc4e3478a45dcfe0a537c6026821873c1", "a706e97c0890496e29c9dd00ae7285fdef38d27f");
});
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

// For Identity
builder.Services.AddIdentity<Account, IdentityRole>()
    .AddEntityFrameworkStores<LBSDbContext>()
    .AddDefaultTokenProviders();


// Add services to the container.
builder.Services.AddControllers();
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

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
