using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using REST_API.Models;
using REST_API.Data;
using Microsoft.EntityFrameworkCore;

namespace REST_API.Repository
{
    public class DataRepository : IDataRepository<EmployeeRecord>
    {
        private readonly AppDBContext _dbContext;

        public DataRepository(AppDBContext _context)
        {
            _dbContext = _context;
        }

        public async Task<IEnumerable<EmployeeRecord>> GetAllEmployee()
        {
            return await _dbContext.EmployeeRecords.ToListAsync();
        }

        public Task<List<EmployeeRecord>> GetFilteredList(Request req)
        {
            var query = _dbContext.EmployeeRecords.AsQueryable();

            if (!String.IsNullOrEmpty(req.employeeNumber.ToString()))
                query = query.Where(rec => rec.EmployeeNumber == req.employeeNumber);

            if (!String.IsNullOrEmpty(req.firstName))
                query = query.Where(rec => rec.FistName.Contains(req.firstName));

            if (!string.IsNullOrEmpty(req.lastName))
                query = query.Where(rec => rec.LastName.Contains(req.lastName));

            if (req.tempStart > 0 || req.tempEnd > 0)
            {
                req.tempStart = !String.IsNullOrEmpty(req.tempStart.ToString()) ? req.tempStart : req.tempEnd;
                req.tempEnd = !String.IsNullOrEmpty(req.tempEnd.ToString()) ? req.tempEnd : req.tempStart;

                query = query.Where(rec => rec.Temperature >= req.tempStart && rec.Temperature <= req.tempEnd);
            }

            if (req.recordStartDate != null || req.recordEntDate != null)
            {
                req.recordStartDate = req.recordStartDate != null ? req.recordStartDate : req.recordEntDate;
                req.recordEntDate = req.recordEntDate != null ? req.recordEntDate : req.recordStartDate;

                query = query.Where(rec => rec.RecordDate >= req.recordStartDate && rec.RecordDate <= req.recordEntDate);
            }

            return query.ToListAsync();
        }

        public async Task<EmployeeRecord> NewEmployee(Request req)
        {
            //Compute Employee Number based on current year + total record count increment by 1.
            int year = DateTime.Now.Year;
            req.employeeNumber = Convert.ToInt32(string.Format("{0}{1}", year, this.getTotalCount()));


            var entity = req.ToEntity();

            _dbContext.Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<EmployeeRecord> RemoveEmployee(Request req)
        {
            var entity = _dbContext.EmployeeRecords.Where(e => e.EmployeeNumber == req.employeeNumber).FirstOrDefault();

            if (entity != null)
            {
                entity.isActive = false;

                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<EmployeeRecord> UpdateEmployee(Request req)
        {
            var entity = _dbContext.EmployeeRecords.Where(e => e.EmployeeNumber == req.employeeNumber).FirstOrDefault();

            if (entity != null)
            {
                entity.FistName = req.firstName;
                entity.LastName = req.lastName;
                entity.Temperature = req.temperature;
                entity.ModifiedDate = DateTime.Now;

                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            else
            {
                return null;
            }
        }

        private int getTotalCount()
        {
            return _dbContext.EmployeeRecords.Count() + 1;
        }
    }
}
