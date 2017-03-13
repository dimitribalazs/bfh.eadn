using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using System.Web.SessionState;

namespace BFH.EADN.UI.Web.Utils
{
    public class SessionContext
    {
        public Quiz CurrentQuiz { get; set; }
        public bool EvaluationAtEnd { get; set; }
        public Dictionary<Guid, List<Guid>> AnswersForEndEvaluations { get; } = new Dictionary<Guid, List<Guid>>();
    }

    public static class SessionHelper
    {
        public static SessionContext GetSessionContext(this HttpSessionStateBase session)
        {
            if(session["context"] == null)
            {
                session["context"] = new SessionContext();
            }
            return (SessionContext)session["context"];
        }

        public static void SetSessionContext(this HttpSessionStateBase session)
        {
            SessionContext sc = new SessionContext();
            session[HttpContext.Current.Session.SessionID] = sc;
        }
    }
}