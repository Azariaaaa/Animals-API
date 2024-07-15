using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using FinalWorkshop.Database;
using FinalWorkshop.Repository;
using FinalWorkshop.Service;

var bld = WebApplication.CreateBuilder();
bld.Services.AddFastEndpoints();

bld.Services.AddDbContext<DatabaseContext>();

bld.Services.AddScoped<AnimalRepository>();
bld.Services.AddScoped<AnimalService>();

bld.Services.AddScoped<RaceRepository>();
bld.Services.AddScoped<RaceService>();

bld.Services.AddScoped<UserRepository>();
bld.Services.AddScoped<UserService>();

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
