using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalReport_Data.Models.FE
{
    public class FE_Context
    {
        [Key]
        public Int64 UNIQUEID { get; set; }

        public int EQIDENT { get; set; }
        public int STATEIDENT { get; set; }

        public DateTime TS_START { get; set; }
        public DateTime TS_END { get; set; }

    }
}
