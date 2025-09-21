using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Wella")));

builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<TaskServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options => options.DefaultAuthenticateScheme = "MySchema")

    .AddJwtBearer("MySchema", option =>
    {
        string Key = "Hello Wella 123 , wkkejlkflfpkfkfokfokop#";
        var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));

        option.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = secretkey,
            ValidateIssuer = false,
            ValidateAudience = false,

        };
    }
    );

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
