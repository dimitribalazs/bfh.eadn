using BFH.EADN.Common.Types.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using System.Web.SessionState;

namespace BFH.EADN.UI.Web.Utils
{
    /// <summary>
    /// Session object to add quiz specific data to the session
    /// </summary>
    public class SessionContext
    {
        /// <summary>
        /// Curren quiz
        /// </summary>
        public Quiz CurrentQuiz { get; set; }

        /// <summary>
        /// Evaluation at the end or not
        /// </summary>
        public bool EvaluationAtEnd { get; set; }

        /// <summary>
        /// Answers for end evaluation
        /// </summary>
        public Dictionary<Guid, List<Guid>> AnswersForEndEvaluations { get; } = new Dictionary<Guid, List<Guid>>();
    }

    /// <summary>
    /// Session helpers
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// Get the current session context
        /// </summary>
        /// <param name="session">httpSessionStateBase</param>
        /// <returns>SessionContext</returns>
        public static SessionContext GetSessionContext(this HttpSessionStateBase session)
        {
            if(session["context"] == null)
            {
                session["context"] = new SessionContext();
            }
            return (SessionContext)session["context"];
        }
    }
}