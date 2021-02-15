using Domain.Models.Products;
using Domain.Models.Users;
using Moq;

namespace Tests.Mocks
{
    public abstract class MyMocks
    {
        protected Mock<IUserRepository> UsersRepository;
        protected Mock<IProductRepository> ProductRepository;
        protected UserService UserService;
        protected ProductServices ProductServices;

        public MyMocks()
        {
            UsersRepository = new Mock<IUserRepository>();
            ProductRepository = new Mock<IProductRepository>();
            UserService = new UserService(UsersRepository.Object);
            ProductServices = new ProductServices(ProductRepository.Object);
        }
}
}