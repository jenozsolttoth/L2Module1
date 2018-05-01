using StructureMap;
using L2Mentoring.Module1.Entities;
using L2Mentoring.Module1.InterfaceImplementations;
using L2Mentoring.Module1.Interfaces;
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
            For<IArgsVerifier>().Use<ArgsVerifier>();
            For<IProductParser>().Use<ProductParser>();
            For<IOrderBuilder>().Use<OrderBuilder>();
            For<IOrderLineBuilder>().Use<OrderLineBuilder>();
            For<IOrderProcessor>().Use<OrderProcessor>();
            For<ICustomerAttendant>().Use<CustomerAttendant>();
            For<ILineSeparator>().Use<LineSeparator>();
        }
    }
}
