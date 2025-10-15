using BlatchAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AzureBlobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var service = new AzureBlobService();

await service.ReadBlobAsync();

app.MapGet("/users", async (AzureBlobService service) =>
{
    var json = await service.ReadBlobAsync();
    //Parse JSON and return users

    return new[] { new { Id = "1", FirstName = "John" } };
});

app.MapApiCalls();

app.Run();
