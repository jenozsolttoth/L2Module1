using StructureMap;
using L2Mentoring.Module1.Entities;
using ServiceInterfaces;
using Services.CustomerService;
using Services.OrderService;
using Services.ProductService;

namespace L2Mentoring.Module1
{
    class ModuleConfiguration
    {
        public Container ConfigureModule()
        {
            Container container = new Container(new ModuleRegistry());
            return container;
        }
    }
    public class ModuleRegistry : Registry
    {
        public ModuleRegistry()
        {
            Scan(_ =>
                {
                    _.AssemblyContainingType<ICustomerParser>();
                    _.AssembliesFromApplicationBaseDirectory();
                    _.AddAllTypesOf<ICustomerParser>();
                });
            For<IRunner>().Use<Runner>();

            For<IProductService>().Use<ProductServiceMock>();
            For<ICustomerService>().Use<CustomerServiceMock>();
            For<IOrderService>().Use<OrderServiceMock>();
            For<ICustomerRepository>().Use<DAL.CustomerRepository>();
            For<IProductRepository>().Use<DAL.ProductRepository>();
            For<IConfigurationRepository>().Use<DAL.ConfigurationRepository>();
            For<IYearCounter>().Use<YearCounter>();
            For<IShoppingCart>().Use<ShoppingCart>();
        }
    }
}
