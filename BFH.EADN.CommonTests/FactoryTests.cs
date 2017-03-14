using BFH.EADN.Common;
using BFH.EADN.Common.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.CommonTests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void CreateFactoryPersistenceViaFactory()
        {
            string expectedTypeName = "BFH.EADN.Persistence.EF.FactoryEF";
            IFactoryPersistence result = Factory.CreateInstance<IFactoryPersistence>();
            Assert.IsNotNull(result, "Cannot be null");
            Assert.AreEqual(expectedTypeName, result.GetType().FullName, "Type name is different");
        }
    }
}
