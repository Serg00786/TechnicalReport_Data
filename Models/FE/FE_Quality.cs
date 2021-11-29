using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalReport_Data.Models.FE
{
    public class FE_Quality
    {
       public int id { get; set; }
       public int Eq_id { get; set; }
       public int mes_id { get; set; }
       public DateTime Datetimestamp { get; set; }
       public string shifts { get; set; }
       public int broken { get; set; }
       public int nio { get; set; }
       public int changed { get; set; }
       public DateTime Record_dt { get; set; }
       public int Burn_PVD { get; set; }
    }
}
