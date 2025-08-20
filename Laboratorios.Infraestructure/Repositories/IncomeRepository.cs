using Laboratorios.Core.Entities;
using Laboratorios.Core.ViewModels;
using Laboratorios.Core.Interfaces;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Laboratorios.Infraestructure.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly LaboratoriosContext _context;

        public IncomeRepository(LaboratoriosContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<_Income>> GetIncome(int year)
        {
            try
            {
                var incomedb = await _context._Income.Where(x=>x.datecreated.Date.Year == year).OrderBy(x=>x.incomeId).ToListAsync();
                return incomedb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<Income> GetIncomeById(int incomeId)
        {
            try
            {
                var incomedb = new Income();
                var contador = _context.Income.Count(x => x.incomeId == incomeId);
                if (contador > 0)
                {
                    incomedb = await _context.Income.Where(x => x.incomeId == incomeId).Include(x => x.samples).ThenInclude(x=>x.incomeDetail).FirstOrDefaultAsync();
                }
                return incomedb;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public void ValidateSamples(Income income)
        {
            if (income.nSamples != income.samples.Count)
            {
                income.samples.Remove(income.samples.Last());
                ValidateSamples(income);
            }
            income.samples = income.samples.OrderBy(x => x.nsample).ToList();
        }

        public Income InsertIncome(Income income)
        {
            try
            {
                ValidateSamples(income);
                income.datecollected = null;
                income.dateendcollected = null;
                if (!income.collected)
                {
                    income.datecollected = income._datecollected;
                    income.dateendcollected = income._dateendcollected;
                }
                income.datereception = (DateTime)income._datereception!;
                income.datecreated = DateTime.UtcNow;
                _context.Income.Add(income);
                var idNew = income.incomeId;
                foreach (var item in income.samples)
                {
                    _context.Sample.Add(item);
                    foreach (var obj in item.incomeDetail)
                    {
                        _context.IncomeDetail.Add(obj);
                    }
                }
                _context.SaveChanges();

                _context.Database.ExecuteSqlRaw($"EXEC [dbo].[SP_CreateControlHistoryReport] {income.incomeId}, {income.customerId}");
                return income;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public Income EditIncome(Income income, bool? draft)
        {
            try
            {
                var incomeDB = _context.Income.AsNoTracking().Where(x => x.incomeId == income.incomeId).FirstOrDefault();
                ValidateSamples(income);
                income.datecollected = null;
                income.dateendcollected = null;
                if (!income.collected)
                {
                    income.datecollected = income._datecollected;
                    income.dateendcollected = income._dateendcollected;
                }
                income.datereception = (DateTime)income._datereception!;
                income.datecreated = incomeDB.datecreated;
                income.datemodified = DateTime.UtcNow;
                _context.Entry(income).State = EntityState.Modified;
                foreach (var item in income.samples)
                {
                    if (item.sampleId > 0)
                    {
                        _context.Entry(item).State = EntityState.Modified;
                        foreach (var obj in item.incomeDetail)
                        {
                            var incomeDetailDB = _context.IncomeDetail.AsNoTracking().Where(x => x.sampleId == item.sampleId && x.areaId == obj.areaId && x.analisysId == obj.analisysId).FirstOrDefault();
                            if (incomeDetailDB != null)
                            {
                                obj.incomeDetailId = incomeDetailDB.incomeDetailId;
                                obj.sampleId = incomeDetailDB.sampleId;
                                _context.Entry(obj).State = EntityState.Modified;
                            }
                            else
                            {
                                obj.sampleId = item.sampleId;
                                _context.IncomeDetail.Add(obj);
                            }
                        }
                    }
                    else
                    {
                        _context.Sample.Add(item);
                        var idSample = item.sampleId;
                        foreach (var obj in item.incomeDetail)
                        {
                            obj.sampleId = idSample;
                            _context.IncomeDetail.Add(obj);
                        }
                    }

                }


                if (draft == true)
                {
                    var report = _context.ReportHistory.AsNoTracking().Where(x => x.incomeId == income.incomeId).FirstOrDefault();
                    if (report is not null)
                    {
                        if (report.draftDate is null)
                        {
                            report.draftDate = DateTime.Now.AddHours(-6);
                            _context.Entry(report).State = EntityState.Modified;
                        }
                    }
                }

                _context.SaveChanges();

                return income;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task EditIncomeDate(IncomeView income)
        {
            try
            {
                var incomeDB = _context.Income.AsNoTracking().Where(x => x.incomeId == income.incomeId).FirstOrDefault();
                incomeDB.sendDate = income.sendDate;
                incomeDB.phone = income.phone;
                incomeDB.cusName = income.cusName;
                incomeDB.path = income.path;
                _context.Entry(incomeDB).State = EntityState.Modified;

                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task DeleteIncomeDetail(IncomeDetail incomeDetail)
        {
            try
            {
                _context.Remove(incomeDetail);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }

        }
        public async Task<IEnumerable<IncomeViewModel>> GetDetailsIncome(int year)
        {
            try
            {
                return await _context.IncomeViewModel.FromSqlRaw($"EXECUTE dbo.ObtencionIngresos {year}").ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public Task UpdateSample(Sample samples)
        {
            try
            {
                var sampleDB = _context.Sample.AsNoTracking().Where(x => x.sampleId == samples.sampleId).FirstOrDefault();
                sampleDB.collectionPoint = samples.collectionPoint;
                sampleDB.observations = samples.observations;
                sampleDB.identification = samples.identification;
                _context.Entry(sampleDB).State = EntityState.Modified;

                var report = _context.ReportHistory.AsNoTracking().Where(x => x.incomeId == sampleDB.incomeId).FirstOrDefault();
                if(report is not null)
                {
                    if(report.draftDate is null)
                    {
                        report.draftDate = DateTime.Now.AddHours(-6);
                        _context.Entry(report).State = EntityState.Modified;
                    }
                }
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }

            return Task.CompletedTask;
        }
    }
}
