using FullProject;
using WebAPI.Context;
using WebAPI.Data;
using Core.Application.Interface;
using WebAPI.Repo;
using WebAPI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Net.Http;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazorBootstrap();

builder.Services.AddDbContext<LoginDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ThietLapContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AccountRepository>();

builder.Services.AddScoped<BangCongService>();//Thêm vào đây để database  có thể hiện lên web mà không bị lỗi//
builder.Services.AddScoped<ChamCongService>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddHttpClient ("ApiClient", client =>
{
    client.BaseAddress = new Uri ("http://localhost:7099");
});


// Register HttpClient
builder.Services.AddScoped<HttpClient>();

builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ThietLapService> ();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
