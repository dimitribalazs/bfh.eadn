﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFH.EADN.Persistence.EF.Entities
{
    internal class Answer : BaseEntity
    {
        public virtual Question Question { get; set; }
        public string Text { get; set; }
        public bool IsSolution { get; set; }
    }


}
