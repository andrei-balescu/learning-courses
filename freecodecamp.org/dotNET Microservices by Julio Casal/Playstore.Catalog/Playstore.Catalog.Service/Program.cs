using Playstore.Catalog.Service.ServiceRegistration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var serviceRegistration = new ServiceRegistration(builder);
serviceRegistration.AddMongoDb();
serviceRegistration.AddRepositories();

// Add services to the container.
builder.Services.AddControllers(o =>
{
    // do not remove 'Async' suffix from method names
    o.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();