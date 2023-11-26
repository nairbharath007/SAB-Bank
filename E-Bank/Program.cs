using E_Bank.Data;
using E_Bank.MiddleWare;
using E_Bank.Models;
using E_Bank.Repository;
using E_Bank.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using static E_Bank.Repository.IRepository;

namespace E_Bank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
          


            //adding connection to database as Mycontext-1
            builder.Services.AddDbContext<MyContext>( options=>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString"));
            }
                );

            builder.Services.AddControllers();

            //to avoid json cycle -2
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //next change based on registering

            builder.Services.AddCors(option =>
            {
                option.AddPolicy("AllowLocalhost4200", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("*");
                });
            });//for connecting frond end

            //repository Normal

            builder.Services.AddTransient<IUserRepo,UserRepo>();//old methodregistering needed must added or doesnt work

            //Genetic repository
            builder.Services.AddTransient(typeof(IRepository<>), typeof(EntityRepository<>));//note
            builder.Services.AddTransient<ICustomerService, CustomerService>();//new generatic used registering services
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
            builder.Services.AddTransient<IAdminService, AdminService>();
            builder.Services.AddTransient<IQueryService, QueryService>();
            builder.Services.AddTransient<IDocService,DocService>();
           


            //inbuild  only change in front connect
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //added auth  schema from appsetting
            //added auth scheme 6
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    builder.Configuration.GetSection("AppSettings:Key").Value!)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
           // app.UseRouting();//delete
            app.UseCors("AllowLocalhost4200");//front end connection
            //authentication needed
            app.UseAuthentication();

            app.UseAuthorization();



          app.UseMiddleware<ErrorHandler>();//middleware added errorhandle

            app.MapControllers();

            app.Run();
        }
    }
}