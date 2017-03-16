using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.CommonTests.TestHelper
{
    [TestClass]
    public class TestBaseWithDb
    {
      
        public static void Init(TestContext context)
        {
            var source = Common.Common.GetConfigValue<string>("DBSource");
            var target = Common.Common.GetConfigValue<string>("DBTarget");

            if(string.IsNullOrEmpty(source))
            {
                throw new InvalidOperationException(nameof(source) + "cannot be null. Make sure path to .mdf is set in App.config");
            }

            if (string.IsNullOrEmpty(target))
            {
                throw new InvalidOperationException(nameof(target) + "cannot be null. Make sure target path is set in App.config");
            }

            //if(File.Exists(target))
            //{
            //    File.Delete(target);
            //}

            //File.Create(target);
            
            //File.Copy(source, target, true);
        }

        //public static void Cleanup()
        //{
        //    var source = Common.Common.GetConfigValue<string>("DBSource");
        //    var target = Common.Common.GetConfigValue<string>("DBTarget");

        //    if(File.Exists(target))
        //    {
        //        File.Delete(target);

        //    }
        //}
    }
}
