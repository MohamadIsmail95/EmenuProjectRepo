using EmenuBLL.EmenuServices;
using EmenuBLL.IEmenuServices;
using EmenuDAL.EmenuDbContext;
using EmenuDAL.IRepository;
using EmenuDAL.Model.Seeder;
using EmenuDAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Enable Cors
builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials();
}));

//Add auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Return Error
builder.Services.AddMvc()
       .ConfigureApiBehaviorOptions(options =>
       {
           //options.SuppressModelStateInvalidFilter = true;
           options.InvalidModelStateResponseFactory = actionContext =>
           {
               var errorMes = "";
               var modelState = actionContext.ModelState.Values;

               foreach (var s in modelState)
               {
                   foreach (var m in s.Errors)
                   {
                       errorMes = m.ErrorMessage;
                   }

               }
               return new BadRequestObjectResult(new { ErrorMessage = errorMes });

           };
       });


//add connection with sql database
builder.Services.AddDbContext<EmenuAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add services + repo
builder.Services.AddScoped<IProductAttributeRepository, ProductAttributeRepository>();
builder.Services.AddScoped<IAttributeRepository, AttributeRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAttributeService, AttributeService>();

builder.Services.AddTransient<SeederData>();

//--------------------------------------Add seeder data


var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seederdata")
    SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<SeederData>();
        service.Seed();
    }
}





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
