using QingLong;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "QingLong.xml"));
});

var app = builder.Build();

using (var context = new DatabaseContext()) {
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
app.UseSwagger(c => {
    c.RouteTemplate = "qing-long/api/{documentName}/swagger/swagger.json";
});
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("swagger/swagger.json", "QingLong v1");
    c.RoutePrefix = "qing-long/api/v1";
});
// }

app.UseAuthorization();
app.MapControllers();
app.Run();
