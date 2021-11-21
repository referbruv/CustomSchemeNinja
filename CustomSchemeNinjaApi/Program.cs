using CustomSchemeNinjaApi.Providers.AuthHandlers;
using CustomSchemeNinjaApi.Providers.AuthHandlers.Constants;
using CustomSchemeNinjaApi.Providers.AuthHandlers.Scheme;

var builder = WebApplication.CreateBuilder(args);

#region Configure-Services

var services = builder.Services;

services.AddAuthentication(
    options => options.DefaultScheme = AuthSchemeConstants.MyNinjaAuthScheme)
    .AddScheme<MyNinjaAuthSchemeOptions, MyNinjaAuthHandler>(
        AuthSchemeConstants.MyNinjaAuthScheme, options => { });

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Configure-Pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

#endregion

app.Run();
