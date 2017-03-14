using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Policy;
using System.IdentityModel.Claims;
using System.Security.Principal;
using System.Threading;

namespace BFH.EADN.QuizManagementService.Implementation
{
    //internal class CustomAuthorizationPolicy : IAuthorizationPolicy
    //{
    //    private string _id;
    //    public string Id => _id;

    //    public CustomAuthorizationPolicy()
    //    {
    //        _id = Guid.NewGuid().ToString();
    //    }

    //    public ClaimSet Issuer
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    public bool Evaluate(EvaluationContext evaluationContext, ref object state)
    //    {
    //        IPrincipal currentPrincipal = Thread.CurrentPrincipal;
    //        //if (QuizManagement.LogIn(currentPrincipal.Identity.Name))
    //        evaluationContext.Properties["Principal"] = currentPrincipal;
    //        return true;
    //    }
    //}
}
