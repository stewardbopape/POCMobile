
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POC.IProvider;
using POC.Provider;
using StatsSA.SystemArchitecture.Resolver;


namespace BusinessLogicLayer
{

    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IUserProvider, UserProvider>();
            registerComponent.RegisterType<IInformationProvider, InformationProvider>();

        }
    }
}
