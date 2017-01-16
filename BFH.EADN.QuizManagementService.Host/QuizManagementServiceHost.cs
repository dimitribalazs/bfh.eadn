using BFH.EADN.QuizManagementService.Contracts;
using BFH.EADN.QuizManagementService.Implementation;
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

namespace BFH.EADN.QuizManagementService.Host
{
    public partial class QuizManagementServiceHost : ServiceBase
    {

        private ServiceHost _quizManagementHost;
        public QuizManagementServiceHost()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _quizManagementHost = new ServiceHost(typeof(Implementation.QuizManagement));
            _quizManagementHost.AddServiceEndpoint(
                typeof(IQuizManagement), 
                new WSDualHttpBinding(), 
                "http://localhost:5001/QuizManagement");
            _quizManagementHost.Open();
        }

        protected override void OnStop()
        {
        }
    }
}
