using Ninject.Modules;
using RealtorFirm.BLL.Services;
using RealtorFirm.BLL.Interfaces;

namespace RealtorFirm.PL.Util
{
    public class BindModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<IAppartmentService>().To<AppartmentService>();
            Bind<IApplyService>().To<ApplyService>();
            Bind<IClientService>().To<ClientService>();
        }
    }
}