using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.QuizManagementService.Implementation
{
    /// <summary>
    /// Util class for management service
    /// </summary>
    internal static class Util
    {
        /// <summary>
        /// Checks if a question or a quiz can be deleted
        /// </summary>
        /// <param name="lastUsed">datetime of entity which should be deleted</param>
        /// <param name="deletionThreshold">threshold</param>
        /// <returns></returns>
        internal static bool CanBeDeleted(DateTime? lastUsed, int deletionThreshold)
            => lastUsed.HasValue == false || (DateTime.Now - lastUsed.Value).Days > deletionThreshold;
    }
}
