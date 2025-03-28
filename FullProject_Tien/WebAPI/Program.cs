using WebAPI.Context;
using WebAPI.Data;
using Core.Application.Interface;
using WebAPI.Repo;
using WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WebAPI.GraphQL;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers ();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();



builder.Services.AddScoped<AccountRepository>();


builder.Services.AddDbContext<LoginDbContext> (options => options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection")));
builder.Services.AddDbContext<ThietLapContext> (options => options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection")));

builder.Services.AddScoped<IThietLapService, ThietLapService> (); 
builder.Services.AddScoped<IChamCongService, ChamCongService> ();
builder.Services.AddScoped<IBangCongService, BangCongService> ();
builder.Services.AddScoped<IAccountService, AccountService> ();

builder.Services.AddCors (options =>
{
    options.AddPolicy ("AllowAll", builder =>
    {
        builder.AllowAnyOrigin ()
               .AllowAnyMethod ()
               .AllowAnyHeader ();
    });
});

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7099, listenOptions => listenOptions.UseHttpProtocol());
//});




// graph ql
builder.Services
    .AddGraphQLServer()
    .AddQueryType<WebAPI.GraphQL.Query>()
    .AddMutationType<Mutation>()
    .AddType<NhanVienType>()
    .AddFiltering()
    .AddSorting();



var app = builder.Build ();


// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment () ) {
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

//app.UseHttpsRedirection ();

app.UseAuthorization ();

app.UseCors ("AllowAll");
app.MapGraphQL();



app.MapControllers ();

app.Run ();
