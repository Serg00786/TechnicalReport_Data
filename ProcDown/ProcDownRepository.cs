using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalReport_Data.DTO;
using TechnicalReport_Data.Models;
using TechnicalReport_Data.Models.FE;


namespace TechnicalReport_Data.ProcDown
{
    public interface IProcDownRepository
    {
        Task<List<ProcessDowntimes>> GetProcDownList();
        Task<ProcessDowntimes> DetailProcDownList(int id);
        Task<List<Eq_list>> CreateEquipList();
        void CreateNewProcDownItem(ProcessDowntimes procDown);
        Task<ProcessDowntimes> GetProcessDowntimeRecord(int id);
        void DeleteProcDownRecord(int id);
        void EditExistingItem(ProcessDowntimes model);
        Task<List<ProcessDowntimes>> GetProcDownListByTimePeriod(DatebaseDTO dt);

    }
    public class ProcDownRepository : IProcDownRepository
    {
        FE_context db = new FE_context();

        public async Task<ProcessDowntimes> DetailProcDownList(int id)
        {
            var Detail = await db.ProcessDowntime.Where(x => x.id == id).FirstOrDefaultAsync();
            return Detail;
        }

        public async Task<List<Eq_list>> CreateEquipList()
        {
            var EqList = await db.Eq_id.ToListAsync();
            return EqList;
        }

        public async void CreateNewProcDownItem(ProcessDowntimes procDown)
        {
            var GetShortName = await db.Eq_id.Where(x => x.ID == procDown.Eq_id).ToListAsync();
            procDown.Machine = GetShortName[0].SHORT_NAME;
            db.ProcessDowntime.Add(procDown);
            db.SaveChanges();
        }

        public async void EditExistingItem(ProcessDowntimes model)
        {
            var temp = await db.Eq_id.Where(x => x.ID == model.Eq_id).ToListAsync();
            model.Machine = temp[0].SHORT_NAME;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        public async Task<ProcessDowntimes> GetProcessDowntimeRecord(int id)
        {
            var Result = await db.ProcessDowntime.Where(x => x.id == id).FirstOrDefaultAsync();
            return Result;
        }


        public async Task<List<ProcessDowntimes>> GetProcDownList()
        {
            var Result= await db.ProcessDowntime.OrderByDescending(x => x.to_dt).ToListAsync();
            return Result;
        }

        public async void DeleteProcDownRecord(int id)
        {
            ProcessDowntimes model = await db.ProcessDowntime.Where(x => x.id == id).FirstOrDefaultAsync();
            db.ProcessDowntime.Remove(model);
            db.SaveChanges();
        }

        public async Task<List<ProcessDowntimes>> GetProcDownListByTimePeriod(DatebaseDTO data)
        {
            //To do Modify DatebasDTO to Date
            
            var Result = await db.ProcessDowntime.Where(x => (x.from_dt >= data.From && x.from_dt < data.To) || (x.to_dt >= data.From && x.to_dt < data.To) || (x.from_dt < data.From && x.to_dt > data.To)).ToListAsync();
            return Result;


        }
    }
}
