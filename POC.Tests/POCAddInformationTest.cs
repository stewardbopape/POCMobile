using System;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using POC.BusinessObjects;
using POC.IManager;
using POC.IProvider;
using POC.Manager;
using POC.Provider;
using POC.WebApi.Controllers;

namespace POC.Tests
{
    [TestClass]
    public class POCAddInformationTest
    {
        [TestMethod]
        public void When_Information_Added_Should_Return_True()
        {
            IUnitOfWork unitOfWork = new UnitOfWork();
            IInformationProvider provider = new InformationProvider(unitOfWork);
            INFORMATION info = new INFORMATION();

            info.ID_NO = "9812135489081";

            InformationController controller = new InformationController(provider);

            PostObject<object> post = new PostObject<object>();

            post.Data = JsonConvert.SerializeObject(info);

            var res = controller.SaveInformation(post);
            Assert.IsNotNull(res);

         //   var result = provider.AddInformation(info);

           // Assert.IsNotNull(result.Success);


        }
    }
}
