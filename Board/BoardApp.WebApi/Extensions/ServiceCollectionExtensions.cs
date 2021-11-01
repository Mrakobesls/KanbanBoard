using BoardApp.BLL.Hashing;
using BoardApp.BLL.Mappings;
using BoardApp.BLL.Services;
using BoardApp.BLL.Validators.Base;
using BoardApp.DAL;
using BoardApp.DAL.Model;
using BoardApp.DAL.Repositories;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Jwt.Options;
using BoardApp.WebApi.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using BoardApp.DAL.UnitOfWork;

namespace BoardApp.WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            AddApiServices(services, configuration);
            AddBllServices(services);
            AddDalServices(services, configuration);
            AddAutomapperServices(services);
        }

        private static void AddAutomapperServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServicesProfile), typeof(ApiProfile));
        }

        private static void AddBllServices(IServiceCollection services)
        {
            AddServicesServices(services);
            AddValidationServices(services);
            AddPasswordCryptServices(services);
        }

        private static void AddApiServices(IServiceCollection services, IConfiguration configuration)
        {
            AddJwtServices(services, configuration);
        }

        private static void AddJwtServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("Jwt").Bind);
            services.AddTransient<ITokenService, TokenService>();
        }

        private static void AddServicesServices(IServiceCollection services)
        {
            services.AddTransient<IBoardService, BoardService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IBoardAccessService, BoardAccessService>();
            services.AddTransient<IColumnService, ColumnService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ILabelService, LabelService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<IUserService, UserService>();
        }

        private static void AddValidationServices(IServiceCollection services)
        {
            services.AddTransient<IValidationService, ValidationService>();
        }

        public static void AddPasswordCryptServices(IServiceCollection services)
        {
            services.AddTransient<IPasswordCrypt, PasswordCrypt>();
        }

        private static void AddDalServices(IServiceCollection services, IConfiguration configuration)
        {
            AddDbContextServices(services, configuration.GetConnectionString("DefaultConnection"));
            AddRepositoryServices(services);
            AddUnitOfWorkService(services);
        }

        private static void AddDbContextServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BoardDbContext>(builder =>
            {
                builder.EnableSensitiveDataLogging();
                builder.LogTo(message => Debug.WriteLine(message));
                builder.UseLazyLoadingProxies().UseSqlServer(connectionString);
            });
        }

        private static void AddRepositoryServices(IServiceCollection services)
        {
            services.AddTransient<IGenericRepository<Board>, GenericRepository<Board>>();
            services.AddTransient<IGenericRepository<BoardAccess>, GenericRepository<BoardAccess>>();
            services.AddTransient<IGenericRepository<Card>, GenericRepository<Card>>();
            services.AddTransient<IGenericRepository<Column>, GenericRepository<Column>>();
            services.AddTransient<IGenericRepository<Comment>, GenericRepository<Comment>>();
            services.AddTransient<IGenericRepository<Label>, GenericRepository<Label>>();
            services.AddTransient<IGenericRepository<Permission>, GenericRepository<Permission>>();
            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
        }
        private static void AddUnitOfWorkService(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
