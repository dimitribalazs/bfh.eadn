using BFH.EADN.Common.Wcf;
using BFH.EADN.QuizManagementService.Contracts;
using BFH.EADN.QuizManagementService.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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
            try
            {
                _quizManagementHost = new ServiceHost(typeof(QuizManagement), new Uri("net.tcp://localhost:5001"));
                //_quizManagementHost.AddServiceEndpoint(
                //    typeof(IQuizManagement), 
                //    new BasicHttpBinding(), 
                //    "QuizManagement");


                //ServiceEndpoint sessionEndpoint = _quizManagementHost.AddServiceEndpoint(typeof(ISession), new NetTcpBinding(), "Session");
                //endpoint.EndpointBehaviors.Add(new CustomBehavior());
                //foreach (ServiceEndpoint endpoint in _quizManagementHost.Description.Endpoints)
                //{
                   
                //    endpoint.EndpointBehaviors.Add(new CustomBehavior());
                //}
                //_quizManagementHost.
                _quizManagementHost.Open();
            }
            catch (Exception ex)
            {
                OnStop();
            }
        }

        protected override void OnStop()
        {
            _quizManagementHost?.Close();
            base.OnStop();
        }
    }
}
