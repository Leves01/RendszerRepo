var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Database connection
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//Authentication
builder.Services.AddAuthentication().AddJwtBearer();

//Automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//IService - Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPartService, PartService>();
builder.Services.AddScoped<IStorageService, StorageService>();



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
