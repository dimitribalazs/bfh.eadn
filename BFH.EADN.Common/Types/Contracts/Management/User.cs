﻿using BFH.EADN.Common.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Common.Types.Contracts
{
    [DataContract(Namespace = Constants.XMLNamespace, Name = "User", IsReference = true)]
    public sealed class User : BaseContract
    {
        [DataMember(Order = 0)]
        public string Name { get; set; }

        [DataMember(Order = 0)]
        public string Password { get; set; }
    }
}
