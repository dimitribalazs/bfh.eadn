using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using BFH.EADN.QuizService.Implementation;

namespace BFH.EADN.QuizService.Host
{
    public partial class QuizServiceHost : ServiceBase
    {
        ServiceHost serviceHost = null;
        public QuizServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                serviceHost = new ServiceHost(typeof(Implementation.QuizService));
            }
            catch(Exception ex)
            {
                OnStop();
            }
        }

        protected override void OnStop()
        {
            serviceHost?.Close();
        }
    }
}
