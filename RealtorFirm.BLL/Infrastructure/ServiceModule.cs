using Ninject.Modules;
using RealtorFirm.DAL.Interfaces;
using RealtorFirm.DAL.Repositories;

namespace RealtorFirm.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
