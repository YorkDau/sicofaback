using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using sicf_BusinessHandlers.BusinessHandlers.Cita;
using sicf_DataBase.Repositories.Cita;
using sicf_BusinessHandlers.BusinessHandlers.Solicitudes;
using sicf_DataBase.Repositories.SolicitudesRepository;
using sicf_DataBase.Compartido;
using sicf_BusinessHandlers.BusinessHandlers.Compartido;
using FluentValidation;
using sicf_Models.Dto.Solicitudes;
using sicf_Models.Validation;
using Microsoft.EntityFrameworkCore;
using sicf_DataBase.Data;
using sicf_DataBase.Repositories;
using sicf_BusinessHandlers.BusinessHandlers.Ciudadano;
using Newtonsoft.Json;
using sicf_BusinessHandlers.BusinessHandlers.EvaluacionPsicologica;
using sicf_DataBase.Mapping;
using System.Data.SqlClient;
using sicf_DataBase.BDConnection;
using sicf_DataBase.Repositories.Apelacion;
using sicf_BusinessHandlers.BusinessHandlers.Apelacion;
using sicf_DataBase.Repositories.Usuario;
using sicfServicesApi.Utility;
using sicf_BusinessHandlers.BusinessHandlers.Usuario;
using sicf_BusinessHandlers.BusinessHandlers.Tarea;
using sicf_DataBase.Repositories.Tarea;
using sicf_DataBase.Remision;
using sicf_BusinessHandlers.BusinessHandlers.Remision;
using Azure.Storage.Blobs;
using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using sicf_DataBase.Repositories.AbogadoRepository;
using sicf_BusinessHandlers.BusinessHandlers.Abogado;
using sicf_BusinessHandlers.BusinessHandlers.Audiencia;
using sicf_DataBase.Repositories.Audiencia;
using sicf_BusinessHandlers.BusinessHandlers.Archivos;
using sicf_DataBase.Repositories.Archivo;
using sicf_BusinessHandlers.BusinessHandlers.Notificacion;
using sicf_DataBase.Repositories.Notificaciones;
using sicf_DataBase.Repositories.EvaluacionPsicologica;
using sicf_DataBase.Repositories.PresolicitudesRepository;
using sicf_BusinessHandlers.BusinessHandlers.Presolicitud;
using sicf_BusinessHandlers.BusinessHandlers.Programacion;
using sicf_DataBase.Repositories.PruebaSolicitud;
using sicf_BusinessHandlers.BusinessHandlers.PruebaSolicitud;
using sicf_DataBase.Repositories.Quorum;
using sicf_BusinessHandlers.BusinessHandlers.Quorum;
using sicf_BusinessHandlers.BusinessHandlers;
using sicf_DataBase.Repositories.Seguimientos;
using sicf_DataBase.Repositories.Plantilla;
using sicf_BusinessHandlers.BusinessHandlers.Seguimientos;
using sicf_DataBase.Repositories.Incumplimiento;
using sicf_BusinessHandlers.BusinessHandlers.Incumplimiento;
using Microsoft.Extensions.DependencyInjection.Extensions;
using sicf_BusinessHandlers.BusinessHandlers.PerfilPermisos;
using sicf_DataBase.Repositories.PerfilPermisos;
using sicf_BusinessHandlers.BusinessHandlers.Plantilla;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using sicf_Models.Dto;
using sicf_BusinessHandlers.BusinessHandlers.Seguridad;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using sicf_BusinessHandlers.BusinessHandlers.Comisaria;
using sicf_DataBase.Repositories.Comisaria;
using sicf_DataBase.Repositories.Dominio;
using sicf_BusinessHandlers.BusinessHandlers.Dominio;
using sicf_DataBase.Repositories.PerfilUsuario;
using sicf_BusinessHandlers.BusinessHandlers.Filter;
using sicf_DataBase.Repositories.Filter;
using sicf_BusinessHandlers.BusinessHandlers.Programacion;
using sicf_DataBase.Repositories.Programacion;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;
using sicf_BusinessHandlers.BusinessHandlers.ReporteSolicitud;
using sicf_DataBase.Repositories.ReporteSolicitud;
using sicf_BusinessHandlers.BusinessHandlers.PruebasPARD;
using sicf_BusinessHandlers.BusinessHandlers.AzureBlogStorage;
using sicf_BusinessHandlers;

var builder = WebApplication.CreateBuilder(args);

var policyName = "_myAllowSpecificOrigins";

/* TODO: Ìmplementar seguridad AZURE B2C*/

//La seguridad cors se puede habilitar en el app service del api en azure.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                          policy.SetIsOriginAllowed(x => x == "http://localhost:4200");
                      });
});

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(PersonProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SicfServices", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,
              
            },
            new List<string>()
          }
        });
});

//Dependency Injection

builder.Services.AddScoped<ICitaRepository, CitaRepository>();
builder.Services.AddScoped<ICitaHandler, CitaHandler>();

builder.Services.AddScoped<IPresolicitudRepository, PresolicitudRepository>();
builder.Services.AddScoped<IPresolicitudService, PresolicitudService>();


builder.Services.AddScoped<ISolicitudesHandler, SolicitudesHandler>();
builder.Services.AddScoped<ISolicitudesRepository, SolicitudesRepository>();

builder.Services.AddScoped<ICompartidoHandler, CompartidoHandler>();
builder.Services.AddScoped<ICompartidoRepository, CompartidoRepository>();


var dbcontext = builder.Services.AddDbContext<SICOFAContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IUnitofWork, UnitofWork>();
builder.Services.AddTransient<ICiudadanoService, CiudadanoService>();
builder.Services.AddTransient<ISolicitudService, SolicitudService>();
builder.Services.AddTransient<IEvaluacionPsicologicaService, EvaluacionPiscologicaService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); 
builder.Services.AddScoped<IUsuarioHandler, UsuarioHandler>(); 
builder.Services.AddScoped<IValidarAcceso, ValidarAcceso>();

builder.Services.AddTransient<ITareaHandler, TareaHandler>();
builder.Services.AddTransient<ITareaRepository, TareaRepository>();

builder.Services.AddTransient<IAbogadoRepository, AbogadoRepository>();
builder.Services.AddTransient<IAbogadoService, AbogadoService>();

builder.Services.AddTransient<IAudienciaRepository, AudienciaRepository>();
builder.Services.AddTransient<IAudienciaService, AudienciaService>();

builder.Services.AddTransient<INotificacionRepository, NotificacionRepository>();
builder.Services.AddTransient<INotificacionService, NotificacionService>();

// no es codigo de prueba no borrar.
builder.Services.AddTransient<IPruebaSolicitudServicioRepository, PruebaSolicitudServicioRepository>();
builder.Services.AddTransient<IPruebaSolicitudService, PruebaSolicitudService>();

builder.Services.AddTransient<IQuorumServicioRepository, QuorumServicioRepository>();
builder.Services.AddTransient<IQuorumService, QuorumService>();

builder.Services.AddTransient<IApelacionRepository, ApelacionRepository>();
builder.Services.AddTransient<IApelacionService, ApelacionService>();

builder.Services.AddTransient<IProgramacionService, ProgramacionService>();
builder.Services.AddTransient<IProgramacionRepository, ProgramacionRepository>();

builder.Services.AddTransient<IPlantillaService, PlantillaService>();
builder.Services.AddTransient<IPlantillaRepository, PlantillaRepository>();

builder.Services.AddTransient<IFileManagerLogic, FileManagerLogic>();   
builder.Services.AddTransient<IArchivoService, ArchivoService>();

builder.Services.AddTransient<IEvaluacionPsicologicaRepository, EvaluacionPsicologicaRepository>();

builder.Services.AddTransient<ISeguimientosServicioRepository, SeguimientosServicioRepository>();
builder.Services.AddTransient<ISeguimientosService, SeguimientosService>();


builder.Services.AddTransient<IArchivosRepository, ArchivosRepository>();

builder.Services.AddTransient<IIncumplimientoRepository, IncumplimientoRepository>();
builder.Services.AddTransient<IIncumplimientoService, IncumplimientoService>();

builder.Services.AddTransient<IPerfilPermisosService, PerfilPermisosService>();
builder.Services.AddTransient<IPerfilPermisosRepository, PerfilPermisosRepository>();
builder.Services.AddTransient<ISecurityService, SecurityService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));


builder.Services.Configure<Authentication>(options => builder.Configuration.GetSection("Authentication").Bind(options));
builder.Services.AddTransient<ISendgridNotificaciones, SendgridNotificaciones>();


builder.Services.AddTransient<IComisariaService, ComisariaService>();
builder.Services.AddTransient<IComisariaRepository,ComisariaRepository>();

builder.Services.AddTransient<IDominioRepository, DominioRepository>();
builder.Services.AddTransient<IDominioService, DominioService>();

builder.Services.AddTransient<IPerfilUsuarioRepository, PerfilUsuarioRepository>();

builder.Services.AddTransient<IFilterService, FilterService>();
builder.Services.AddTransient<IFilterRepository, FilterRepository>();

builder.Services.AddScoped<IReporteSolicitudHandler, ReporteSolicitudHandler>();
builder.Services.AddScoped<IReporteSolicitudRepository, ReporteSolicitudRepository>();
builder.Services.AddTransient<IPruebasPardService, PruebasPardService>();
builder.Services.AddTransient<IPruebasPardRepository, PruebasPardRepository>();

// Layer dependencies
builder.Services.AgregarBussinessDependencias(builder.Configuration);


// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Authentication:Audience"],
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]))
    };
});


//Register BlobServiceClient

    builder.Services.AddScoped(_ =>
    {
        return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
        //Mirar mas detallado el servicio del blogStorage, como es el comportamiento.
    });

builder.Services.AddScoped<IFileManagerLogic>(provider =>
{
    var useAzure = bool.Parse(provider.GetRequiredService<IConfiguration>()["UseAzureBlobStorage"] ?? "false");

    if (useAzure)
    {
        var blobServiceClient = provider.GetService<BlobServiceClient>();
        if (blobServiceClient == null)
        {
            throw new InvalidOperationException("BlobServiceClientno no registrado.");
        }
        return new AzureBlobStorageService(blobServiceClient, provider.GetRequiredService<IConfiguration>());
    }

    var localPath = provider.GetRequiredService<IConfiguration>()["ConnectionStrings:LocalStoragePath"];
    return new LocalStorageService(localPath);
});




builder.Services.AddScoped<IRemisionRepository, RemisionRepository>();
builder.Services.AddScoped<IRemisionHandler, RemisionHandler>();

//IRemisionRepository

builder.Services.AddMvc().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<IValidator<RequestDatosInvolucrado>, RequestDatosInvolucradoValidator>();
var app = builder.Build();
IConfiguration configuration = app.Configuration;

IWebHostEnvironment environment = app.Environment;


//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SicfServices");
        options.RoutePrefix = string.Empty;
    });
//}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

