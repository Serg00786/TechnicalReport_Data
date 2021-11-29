using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalReport_Data;
using TechnicalReport_Data.DTO;
using TechnicalReport_Data.Models;
using TechnicalReport_Data.Models.FE;

namespace TechnicalReport_Data.FE.Availability
{
    public interface IFERepository
    {
        Task<List<FE_Context>> GetFeAvailabilityData(DatebaseDTO datebaseDTO);
        Task<List<ProcessDowntimes>> GetProcessDowntimes(DatebaseDTO datebaseDTO);
        Task<List<FE_Quality>> GetFeQualityData(DatebaseDTO datebaseDTO);
        Task<List<FE_Efficiency>> GetFeEfficiencyData(DatebaseDTO datebaseDTO);
        List<Eq_list> GetEquipmentList();

    }
    public class FERepository : IFERepository
    {
        FE_context db = new FE_context();

        public async Task<List<FE_Context>> GetFeAvailabilityData(DatebaseDTO datebaseDTO)
        {
            var data = AutomapperConvertion(datebaseDTO);
            DateTime zero = DateTime.MinValue.AddYears(1899);
            var FE_Av = await db.Availability_raw.Where(x => ((x.TS_START < data.From && x.TS_END > data.To) || (x.TS_END > data.From && x.TS_END < data.To) || (x.TS_START < data.To && x.TS_END > data.To)) && x.EQIDENT == data.Eq_id).ToListAsync();
            var Fe_Av1 = await db.Availability_raw.Where(x => (x.TS_START > DateTime.Now.AddMonths(-1) && x.TS_END==zero)).OrderByDescending(x => x.UNIQUEID).Take(1).ToListAsync();
            FE_Av.AddRange(Fe_Av1);
            return FE_Av;
        }

        public async Task<List<ProcessDowntimes>> GetProcessDowntimes(DatebaseDTO datebaseDTO)
        {
            var data = AutomapperConvertion(datebaseDTO);
            var ProcDown = await db.ProcessDowntime.Where(x => (x.from_dt >= data.From && x.from_dt < data.To) || (x.to_dt >= data.From && x.to_dt < data.To) || (x.from_dt < data.From && x.to_dt > data.To)).ToListAsync();

            return ProcDown;
        }

        public async Task<List<FE_Quality>> GetFeQualityData(DatebaseDTO datebaseDTO)
        {
            var data = AutomapperConvertion(datebaseDTO);
            var WafBr = await db.wafer_broken.Where(u => u.Record_dt >= data.From && u.Record_dt < data.To && u.changed < 2).ToListAsync();
            return WafBr;
        }

        public async Task<List<FE_Efficiency>> GetFeEfficiencyData(DatebaseDTO datebaseDTO)
        {
            var data = AutomapperConvertion(datebaseDTO);
            var WafProd = await db.WaferProduced.Where(u => u.Datetimestamp >= data.From && u.Datetimestamp < data.To).ToListAsync();
            return WafProd;
        }

        public List<Eq_list> GetEquipmentList()
        {

            var EqList= db.Eq_id.OrderBy(x => x.SHORT_NAME).ToList();
            return EqList;
        }
        private Date AutomapperConvertion(DatebaseDTO datebaseDTO)
        {
            return Mapping.Mapping.Mapper.Map<Date>(datebaseDTO);
            
        }
    }
}
