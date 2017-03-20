using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common
{
    public static class Constants
    {
        /// <summary>
        /// Defines the XML namespace for the WCF contracts
        /// </summary>
        public const string XMLNamespace = "http://bfh.eadn.semesterarbeit.ch";

        /// <summary>
        /// Defines the threshold wenn data can be deleted
        /// </summary>
        public const int DeletionThreshold = 5;

        /// <summary>
        /// Role name used for authorization in web controller
        /// </summary>
        public const string AdminRoleName = "QuizAdmin";

        /// <summary>
        /// Session Id key for Message Inspection
        /// </summary>
        public const string SessionId = "SessionId";

        /// <summary>
        /// UserName for Message Inspection
        /// </summary>
        public const string UserName = "UserName";

        /// <summary>
        /// Name to defined if seed be made
        /// </summary>
        public const string SeedingActive = "SeedingActive";
    }
}
