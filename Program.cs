using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using FinalWorkshop.Database;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints();
bld.Services.AddDbContext<DatabaseContext>();
bld.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

var app = bld.Build();
app.UseFastEndpoints();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("MyPolicy");
app.Run();
