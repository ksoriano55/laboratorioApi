using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Repositories;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

var dbName = builder.Configuration["ConnectionStrings:DatabaseName"];
var credenciales = Environment.GetEnvironmentVariable("SQL_CREDENTIAL");
var fullConnectionString = $"Database={dbName};{credenciales}";

// Add services to the container.
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cVWhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX5fcXVQTmVdVkV/Wg==");
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddCors(p => p.AddPolicy(name: "corspolicity",
                      policy =>
                      {
                          policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      }));
builder.Services.AddDbContext<LaboratoriosContext>(options => options.UseSqlServer(fullConnectionString));
builder.Services.AddTransient<IMatrixRepository, MatrixRepository>();
builder.Services.AddTransient<IAnalisysRepository, AnalisysRepository>();
builder.Services.AddTransient<IAreaRepository, AreaRepository>();
builder.Services.AddTransient<IQuotationRepository, QuotationRepository>();
builder.Services.AddTransient<IIncomeRepository, IncomeRepository>();
builder.Services.AddTransient<ISampleTypeRepository, SampleTypeRepository>();
builder.Services.AddTransient<ITableNMPRepository, TableNMPRepository>();
builder.Services.AddTransient<ITableNMPDetailRepository, TableNMPDetailRepository>();
builder.Services.AddTransient<IMetodologyRepository, MetodologyRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRolesRepository, RolesRepository>();
builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>();
builder.Services.AddTransient<IMicrobiologyResultsRepository, MicrobiologyResultsRepository>();
builder.Services.AddTransient<IAutenticacionRepository, AutenticacionRepository>();
builder.Services.AddTransient<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddTransient<IFileManagerRepository, FileManagerRepository>();
builder.Services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IDocumentChangesLogRepository, DocumentChangesLogRepository>();
builder.Services.AddTransient<IMasterListRepository, MasterListRepository>();
builder.Services.AddTransient<IVendorsRepository, VendorsRepository>();
builder.Services.AddTransient<ISamplesOutsourcedRepository, SamplesOutsourcedRepository>();
builder.Services.AddTransient<ISamplesOutsourcedDetailRepository, SamplesOutsourcedDetailRepository>();
builder.Services.AddTransient<IVendorAssessmentRepository, VendorAssessmentRepository>();
builder.Services.AddTransient<ILocationRepository, LocationRepository>();
builder.Services.AddTransient<IEquipmentRepository, EquipmentRepository>();
builder.Services.AddTransient<IEquipmentCalibrationRepository, EquipmentCalibrationRepository>();
builder.Services.AddTransient<IIncomeControlSupplyRepository, IncomeControlSupplyRepository>();
builder.Services.AddTransient<IIncomeControlSupplyDetailRepository, IncomeControlSupplyDetailRepository>();
builder.Services.AddTransient<IPhisycalChemistryResultsRepository, PhisycalChemistryResultsRepository>();
builder.Services.AddTransient<IQualityControlRepository, QualityControlRepository>();
builder.Services.AddTransient<IRecipeRepository, RecipeRepository>();
builder.Services.AddTransient<IProductionOrderRepository, ProductionOrderRepository>();
builder.Services.AddTransient<IContainerRepository, ContainerRepository>();
builder.Services.AddTransient<IConfigurationRecipesRepository, ConfigurationRecipesRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IReportHistoryRepository, ReportHistoryRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ISubAnalisysRepository, SubAnalisysRepository>();
builder.Services.AddTransient<ISamplesOutsourcedResultRepository, SamplesOutsourcedResultRepository>();
builder.Services.AddTransient<IPackageRepository, PackageRepository>();
builder.Services.AddTransient<IShelvesRepository, ShelvesRepository>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Laboratorios API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Porfavor ingrese un token valido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseCors("corspolicity");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
