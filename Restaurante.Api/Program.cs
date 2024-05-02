using Restaurante.Infrastrecture.Identity;
using Restaurante.Infrastrecture.Persistence;
using Restaurante.Core.Application;
using Microsoft.AspNetCore.Identity;
using Restaurante.Infrastrecture.Identity.Entities;
using Restaurante.Infrastrecture.Identity.Seeds;
using Restaurante.Api.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(o =>
{
    o.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(o =>
{
    o.SuppressInferBindingSourcesForParameters = true;
    o.SuppressMapClientErrors = true;
});



//--------------------------
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
//este servicio es para ver el estado de la API
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddApiVersioningExtension();
builder.Services.AddSwaggerExtension();
builder.Services.AddSession();
//------------------------------


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedsAsync(userManager, roleManager);
        await UserAdministrador.SeedsAsync(userManager, roleManager);
        await UserMesero.SeedsAsync(userManager, roleManager);
        await UserSuperAdmin.SeedsAsync(userManager, roleManager);


    }
    catch (Exception ex) { Console.WriteLine(ex); }
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseHealthChecks("/healt");
app.UseSession();
app.MapControllers();

app.Run();
