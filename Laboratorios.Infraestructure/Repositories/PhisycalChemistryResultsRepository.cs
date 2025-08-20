

using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Laboratorios.Infraestructure.Repositories
{
    public class PhisycalChemistryResultsRepository : IPhisycalChemistryResultsRepository
    {
        private readonly LaboratoriosContext _context;

        public PhisycalChemistryResultsRepository(LaboratoriosContext context)
        {
            _context = context;
        }
        public Task InsertPhisycalChemistryResults(IEnumerable<PhisycalChemistryResultsViewModel> results)
        {
            try
            {
                foreach (var item in results)
                {
                    PhisycalChemistryResults model = new PhisycalChemistryResults(item);
                    Sample sample = new Sample();
                    if (item.sampleId == 0)
                    {
                        var exists = _context.Sample.Where(x => x.matrixId == item.matrixId
                                                           && x.correlative == item.correlative
                                                           && x.datecreated.Value.Date == item.analisysDate.Value.Date).FirstOrDefault();
                        if (exists == null)
                        {
                            sample = new Sample()
                            {
                                sampleId = 0,
                                nsample = 1,
                                correlative = item.correlative,
                                year = item.analisysDate.Value.Year,
                                sampleTypeId = 1,
                                matrixId = item.matrixId,
                                included = true,
                                datecreated = item.analisysDate,
                                observations = "FQ",
                                incomeDetail = new List<IncomeDetail>() {
                                new IncomeDetail{
                                sampleId = 0,
                                analisysId = item.analisysId,
                                areaId = (int)item.areaId,
                                incomeDetailId = 0,
                                onSite = null }
                            },
                                phisycalChemistryResults = new List<PhisycalChemistryResults>
                            {
                                 model
                            }
                            };
                            _context.Sample.Add(sample);
                            _context.IncomeDetail.Add(sample.incomeDetail.First());
                            _context.PhisycalChemistryResults.Add(sample.phisycalChemistryResults!.First());
                            continue;
                        }
                        else
                        {
                            var detalle = new IncomeDetail
                            {
                                sampleId = exists.sampleId,
                                analisysId = item.analisysId,
                                areaId = (int)item.areaId,
                                incomeDetailId = 0,
                                onSite = null
                            };
                            model.sampleId = exists.sampleId;
                            _context.IncomeDetail.Add(detalle);
                            _context.PhisycalChemistryResults.Add(model);
                            continue;
                        }
                    }
                    if (item.phisycalChemistryId > 0)
                    {
                        _context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.PhisycalChemistryResults.Add(model);
                    }
                }
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public async Task<IEnumerable<PhisycalChemistryResultsViewModel>> GetAnalisysPhisycalChemistry(DateTime date, DateTime endDate, int analisysId, int areaId)
        {
            try
            {
                var query = $"EXECUTE dbo.GetAnalisysPhisycalChemistry {date.Date.ToString("yyyyMMdd")}, {endDate.Date.ToString("yyyyMMdd")},{analisysId},{areaId}";
                return await _context.PhisycalChemistryResultsViewModel.FromSqlRaw(query).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<ConfigurationResults>> GetConfigurationResults()
        {
            try
            {
                return await _context.ConfigurationResults.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<object> GetQualityAssurance(int analisysId, int year, int opt)
        {
            try
            {
                var area = await _context.Area_Analisys.Where(h => h.analisysId == analisysId).FirstOrDefaultAsync();
                var data = new QualityAssuranceResultViewModel();
                var optString = "";
                if (opt==0 || opt ==1 || opt == 3 || opt == 4)
                {
                    optString = "BCO FORT";
                }
                else
                {
                    optString = "MX FORT";
                }
                if (area.areaId == 3 && opt < 2)
                {
                    var result = from ph in _context.PhisycalChemistryResults
                                 join id in _context.IncomeDetail
                                 on new { X1 = ph.analisysId, X2 = ph.sampleId } equals new { X1 = id.analisysId, X2 = id.sampleId }
                                 join s in _context.Sample on id.sampleId equals s.sampleId
                                 where s.correlative.Contains(optString)
                                 && s.incomeId == null
                                 && s.datecreated.Value.Year == year
                                 && id.analisysId == analisysId
                                 orderby s.datecreated
                                 select new
                                 {
                                     date = s.datecreated.Value.Date.ToString("dd-MM-yyyy"),
                                     interval = _context.Sample.Where(f => f.incomeId != null && f.matrixId == s.matrixId && f.phisycalChemistryResults.Where(h => h.analisysId == analisysId && h.analisysDate.Value.Date == s.datecreated.Value.Date).Count() > 0).OrderBy(l => l.correlative).Select(p => p.correlative).ToList(),
                                     ph = ph,
                                     realValue = _context.QualityControl.Where(c => c.creationDate.Date == s.datecreated.Value.Date && c.analisysId == analisysId).FirstOrDefault() == null ?0:_context.QualityControl.Where(c => c.creationDate.Date == s.datecreated.Value.Date && c.analisysId == analisysId).FirstOrDefault().realValue,
                                     user = _context.User.Where(c => c.UserId == ph.userReading).FirstOrDefault().UserName,
                                 };

                    data.data = result.Count() == 0 ? result.ToList() : result.ToList().Select(j => new
                    {
                        j.date,
                        intervalo = j.interval == null || j.interval.Count == 0 ? "N/A" : j.interval.FirstOrDefault().ToString() + " - " + j.interval.LastOrDefault().ToString(),
                        j.realValue,
                        j.ph.result,
                        percent = System.Math.Round((j.realValue / decimal.Parse(j.ph.result, CultureInfo.InvariantCulture)) * 100, 2),
                        j.user,
                    });

                    data.columns = new List<Columns>()
                            {
                                new Columns() { dataField = "date", caption = "Fecha"},
                                new Columns() { dataField = "intervalo", caption = "Intervalo"},
                                new Columns() { dataField = "realValue", caption = "Valor Real mg/L"},
                                new Columns() { dataField = "result", caption = "Valor Encontrado mg/L"},
                                new Columns() { dataField = "percent", caption = "% Recuperación"},
                                new Columns() { dataField = "user", caption = "Responsable"},
                            };
                }
                else if (opt < 2 && area.areaId!=3)
                {
                    var dato = analisysId == 30 ? 500 : 50;
                    var result = from ph in _context.PhisycalChemistryResults
                                 join id in _context.IncomeDetail
                                 on new { X1 = ph.analisysId, X2 = ph.sampleId } equals new { X1 = id.analisysId, X2 = id.sampleId }
                                 join s in _context.Sample on id.sampleId equals s.sampleId
                                 where s.correlative.Contains(optString)
                                 && s.incomeId == null
                                 && s.datecreated.Value.Year == year
                                 && id.analisysId == analisysId
                                 orderby s.datecreated
                                 select new
                                 {
                                     date = s.datecreated.Value.Date.ToString("dd-MM-yyyy"),
                                     interval = _context.Sample.Where(f => f.incomeId != null && f.matrixId == s.matrixId && f.phisycalChemistryResults.Where(h => h.analisysId == analisysId && h.analisysDate.Value.Date == s.datecreated.Value.Date).Count() > 0).OrderBy(l => l.correlative).Select(p => p.correlative).ToList(),
                                     ph = ph,
                                     percent = Math.Round(Convert.ToDecimal(ph.result == null ? 0 : decimal.Parse(ph.result, CultureInfo.InvariantCulture)) / dato * 100,2),
                                     user = _context.User.Where(c => c.UserId == ph.userReading).FirstOrDefault().UserName,
                                 };


                    data.data = result.Count() == 0 ? result.ToList() : result.ToList().Select(j => new
                    {
                        j.date,
                        intervalo = j.interval == null || j.interval.Count == 0 ? "N/A" : j.interval.FirstOrDefault().ToString() + " - " + j.interval.LastOrDefault().ToString(),
                        j.ph.sampleVolume,
                        j.ph.factor,
                        j.ph.finalVolume,
                        j.ph.reading,
                        j.ph.factor1,
                        j.ph.readingDuplicate,
                        j.ph.capsulemx1,
                        j.ph.factor2,
                        j.ph.capsulemx2,
                        j.ph.factor3,
                        j.ph.capsulemx3,
                        j.ph.sampleBlank,
                        j.ph.factor4,
                        j.ph.resultTwo,
                        j.ph.result,
                        j.percent,
                        j.user,
                    });

                    if (Enumerable.Range(30, 32).Contains(analisysId))
                    {
                        data.columns = new List<Columns>()
                            {
                                new Columns() { dataField = "date", caption = "Fecha"},
                                new Columns() { dataField = "intervalo", caption = "Intervalo"},
                                new Columns() { dataField = "sampleVolume", caption = "mL"},
                                new Columns() { dataField = "factor", caption = "Factor de Corrección"},
                                new Columns() { dataField = "finalVolume", caption = "mL Corregido"},
                                new Columns() { dataField = "reading", caption = "Peso Capsula Vacio" },
                                new Columns() { dataField = "factor1", caption = "Factor de Corrección" },
                                new Columns() { dataField = "readingDuplicate", caption = "Peso Capsula Vacio Corregido" },
                                new Columns() { dataField = "capsulemx1", caption = "Capsula + Muestra"},
                                new Columns() { dataField = "factor2", caption = "Factor de Corrección"},
                                new Columns() { dataField = "capsulemx2", caption = "Capsula + Muestra Corregido"},
                                new Columns() { dataField = "result", caption = "Resultado"},
                                new Columns() { dataField = "percent", caption = "% Recuperación"},
                                new Columns() { dataField = "user", caption = "Responsable"},
                            };
                    }
                    if (analisysId == 36)
                    {
                        data.columns = new List<Columns>()
                            {
                                new Columns() { dataField = "date", caption = "Fecha"},
                                new Columns() { dataField = "intervalo", caption = "Intervalo"},
                                new Columns() { dataField = "sampleVolume", caption = "mL"},
                                new Columns() { dataField = "factor", caption = "Factor de Corrección"},
                                new Columns() { dataField = "finalVolume", caption = "mL Corregido"},
                                new Columns() { dataField = "capsulemx1", caption = "mL Frasco" },
                                new Columns() { dataField = "factor1", caption = "Factor de Corrección" },
                                new Columns() { dataField = "capsulemx2", caption = "mL Frasco Corregido" },
                                new Columns() { dataField = "factor2", caption = "Factor de Titulación" },
                                new Columns() { dataField = "reading", caption = "mL AgNO3"},
                                new Columns() { dataField = "factor3", caption = "Factor de Corrección"},
                                new Columns() { dataField = "capsulemx3", caption = "mL AgNO3 Corregido"},
                                new Columns() { dataField = "sampleBlank", caption = "mL Blanco"},
                                new Columns() { dataField = "factor4", caption = "Factor de Corrección"},
                                new Columns() { dataField = "resultTwo", caption = "mL Blanco Corregido"},
                                new Columns() { dataField = "result", caption = "Resultado"},
                                new Columns() { dataField = "percent", caption = "% Recuperación"},
                                new Columns() { dataField = "user", caption = "Responsable"},
                            };
                    }
                }
                else if (opt == 2)
                {
                    var result = from ph in _context.PhisycalChemistryResults
                                 join id in _context.IncomeDetail
                                 on new { X1 = ph.analisysId, X2 = ph.sampleId } equals new { X1 = id.analisysId, X2 = id.sampleId }
                                 join s in _context.Sample on id.sampleId equals s.sampleId
                                 where s.correlative.Contains(optString)
                                 && s.incomeId == null
                                 && s.datecreated.Value.Year == year
                                 && id.analisysId == analisysId
                                 orderby s.datecreated
                                 select new
                                 {
                                     date = s.datecreated.Value.Date.ToString("dd-MM-yyyy"),
                                     interval = s.correlative.Replace("Mx Fort - ", ""),
                                     resultOriginal = _context.Sample.Where(x => x.correlative == s.correlative.Replace("Mx Fort - ", "") && x.year == year).FirstOrDefault().phisycalChemistryResults.Where(g => g.analisysId == analisysId).FirstOrDefault().result,
                                     ph = ph,
                                     user = _context.User.Where(c => c.UserId == ph.userReading).FirstOrDefault().UserName,
                                 };

                    data.data = result.Count() == 0 ? result.ToList() : result.ToList().Select(j => new
                    {
                        j.date,
                        intervalo = j.interval,
                        j.ph.sampleVolume,
                        j.ph.factor,
                        j.ph.finalVolume,
                        j.ph.reading,
                        j.ph.factor1,
                        j.ph.readingDuplicate,
                        j.ph.capsulemx1,
                        j.ph.factor2,
                        j.ph.capsulemx2,
                        j.ph.factor3,
                        j.ph.capsulemx3,
                        j.ph.sampleBlank,
                        j.ph.factor4,
                        j.ph.resultTwo,
                        j.ph.result,
                        j.resultOriginal,
                        percent = Math.Round(((decimal.Parse((j.ph.result == null ? "0" : j.ph.result), CultureInfo.InvariantCulture) * (10 + 90) - (decimal.Parse(j.resultOriginal == null ? "0" : j.resultOriginal, CultureInfo.InvariantCulture) * 90)) / (500 * 10)) * 100,2),
                        j.user,
                        //campos de formula
                        ml = 90,
                        mlStock = 10,
                        cStock = 500
                    });

                    data.columns = new List<Columns>()
                    {
                        new Columns() { dataField = "date", caption = "Fecha"},
                        new Columns() { dataField = "intervalo", caption = "Muestra"},
                        new Columns() { dataField = "ml", caption = "mL"},
                        new Columns() { dataField = "resultOriginal", caption = "Conc. real de la Mx mg/L Cloruros"},
                        new Columns() { dataField = "mlStock", caption = "mL STOCK"},
                        new Columns() { dataField = "cStock", caption = "Conc. Del STOCK mg/L Cloruros" },
                        new Columns() { dataField = "result", caption = "Conc. de la mx FORT. mg/L Cloruros" },
                        new Columns() { dataField = "percent", caption = "% Recuperación"},
                        new Columns() { dataField = "user", caption = "Responsable"},
                    };
                }
                else if(opt== 3 || opt == 4 ) {
                    //optString = "BCO FORT";
                    var AorB = opt == 3 ? "1" : "2";
                    var result = from ph in _context.PhisycalChemistryResults
                                 join id in _context.IncomeDetail
                                 on new { X1 = ph.analisysId, X2 = ph.sampleId } equals new { X1 = id.analisysId, X2 = id.sampleId }
                                 join s in _context.Sample on id.sampleId equals s.sampleId
                                 where s.correlative.Contains(optString)
                                 && s.incomeId == null
                                 && s.datecreated.Value.Year == year
                                 && id.analisysId == analisysId
                                 orderby s.datecreated
                                 select new
                                 {
                                     date = s.datecreated.Value.Date.ToString("dd-MM-yyyy"),
                                     interval = _context.Sample.Where(f => f.incomeId != null && f.matrixId == s.matrixId && f.phisycalChemistryResults.Where(h => h.analisysId == analisysId && h.analisysDate.Value.Date == s.datecreated.Value.Date).Count() > 0).OrderBy(l => l.correlative).Select(p => p.correlative).ToList(),
                                     ph = ph,
                                     teoricoDQO = opt == 3 ? 300 : 10,
                                     user = _context.User.Where(c => c.UserId == ph.userReading).FirstOrDefault().UserName,
                                     AorB = ph.comments
                                 };
                    data.data = result.Where(o => o.AorB == AorB).ToList().Select(j => new
                    {
                        j.date,
                        intervalo = j.interval == null || j.interval.Count == 0 ? "N/A" : j.interval.FirstOrDefault().ToString() + " - " + j.interval.LastOrDefault().ToString(),
                        teoric = j.teoricoDQO,
                        j.ph.result,
                        percent = (float)System.Math.Round((decimal.Parse(j.ph.result, CultureInfo.InvariantCulture) /j.teoricoDQO)*100,2),
                        j.user,
                    });

                    data.columns = new List<Columns>()
                    {
                        new Columns() { dataField = "date", caption = "Fecha"},
                        new Columns() { dataField = "intervalo", caption = "Muestra"},
                        new Columns() { dataField = "teoric", caption = "Teorico DQO"},
                        new Columns() { dataField = "result", caption = "Resultado" },
                        new Columns() { dataField = "percent", caption = "% Recuperación"},
                        new Columns() { dataField = "user", caption = "Responsable"},
                    };
                }
                else
                {
                    //optString = "MX FORT";
                    var AorB = opt == 5 ? "1" : "2";
                    var result = from ph in _context.PhisycalChemistryResults
                                 join id in _context.IncomeDetail
                                 on new { X1 = ph.analisysId, X2 = ph.sampleId } equals new { X1 = id.analisysId, X2 = id.sampleId }
                                 join s in _context.Sample on id.sampleId equals s.sampleId
                                 where s.correlative.Contains(optString)
                                 && s.incomeId == null
                                 && s.datecreated.Value.Year == year
                                 && id.analisysId == analisysId
                                 orderby s.datecreated
                                 select new
                                 {
                                     date = s.datecreated.Value.Date.ToString("dd-MM-yyyy"),
                                     interval = s.correlative.Replace("Mx Fort - ", ""),
                                     ph = ph,
                                     resultOriginal = _context.Sample.Where(x => x.correlative == s.correlative.Replace("Mx Fort - ", "") && x.year == year).FirstOrDefault().phisycalChemistryResults.Where(g => g.analisysId == analisysId).FirstOrDefault().result,
                                     user = _context.User.Where(c => c.UserId == ph.userReading).FirstOrDefault().UserName,
                                     AorB = ph.comments,
                                     //campos de formula
                                     ml = opt == 5 ? 70: 99,
                                     mlStock = opt == 5 ? 30: 1,
                                     cStock = 1000
                                 };

                    data.data = result.Where(o => o.AorB == AorB).ToList().Select(j => new
                    {
                        j.date,
                        intervalo = j.interval,
                        j.ml,
                        j.resultOriginal,
                        j.mlStock,
                        j.cStock,
                        j.ph.result,
                        percent = Math.Round((decimal.Parse(j.ph.result, CultureInfo.InvariantCulture) *(j.ml+j.mlStock)-(decimal.Parse(j.resultOriginal, CultureInfo.InvariantCulture) *j.ml))/ (j.mlStock*j.cStock) * 100,2),
                        j.user,
                    });

                    data.columns = new List<Columns>()
                    {
                        new Columns() { dataField = "date", caption = "Fecha"},
                        new Columns() { dataField = "intervalo", caption = "Muestra"},
                        new Columns() { dataField = "ml", caption = "mL Muestra"},
                        new Columns() { dataField = "resultOriginal", caption = "Conc. real de la Mx mg/L DQO" },
                        new Columns() { dataField = "mlStock", caption = "mL STOCK" },
                        new Columns() { dataField = "cStock", caption = "Conc. Del STOCK mg/L DQO" },
                        new Columns() { dataField = "result", caption = "Result" },
                        new Columns() { dataField = "percent", caption = "% Recuperación"},
                        new Columns() { dataField = "user", caption = "Responsable"},
                    };

                }
                
                _context.Database.CloseConnection();

                return data;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
