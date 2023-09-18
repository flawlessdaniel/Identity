var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("SimpleUser")
    .AddCookie("SimpleUser")
    .AddCookie("AdvancedUser");

builder.Services.AddAuthorization(builder =>
{
    builder.AddPolicy("ValidSimpleUser", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes("SimpleUser")
            .RequireClaim("id", "simple");
    });
    
    builder.AddPolicy("ValidAdvancedUser", pb =>
    {
        pb.RequireAuthenticatedUser()
            .AddAuthenticationSchemes("AdvancedUser")
            .RequireClaim("id", "advanced");
    });
});

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.Run();