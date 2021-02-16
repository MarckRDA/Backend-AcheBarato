using Domain.Models.Products;
using Domain.Models.Users;
using Moq;

namespace Tests.Mocks
{
    public abstract class MyMocks
    {
        protected Mock<IUserRepository> UsersRepository = new Mock<IUserRepository>();
        protected Mock<IProductRepository> ProductRepository = new Mock<IProductRepository>();
        protected UserService UserService;
        protected Mock<ProductServices> MockProductServices;
        protected ProductServices ProductServices;

        public MyMocks()
        {
            UserService = new UserService(UsersRepository.Object);
            MockProductServices = new Mock<ProductServices>(ProductRepository.Object);
            ProductServices = new ProductServices(ProductRepository.Object);
        }
    }
}