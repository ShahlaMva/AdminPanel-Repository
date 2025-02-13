using AdminPanel.Repositories.CategoryRepo;
using AdminPanel.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using AdminPanel.Services.CategoryServ;
using AdminPanel.Repositories.ProductRepo;
using AdminPanel.Services.ProductServ;

namespace AdminPanel.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();


            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

        }
    }
}
