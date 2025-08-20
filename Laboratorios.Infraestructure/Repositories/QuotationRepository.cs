using Laboratorios.Core.Entities;
using Laboratorios.Core.Interfaces;
using Laboratorios.Core.ViewModels;
using Laboratorios.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OrderTrack.Infraestructure.Data;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Laboratorios.Core.ViewModels.LoginSapViewModel;

namespace Laboratorios.Infraestructure.Repositories
{
    public class QuotationRepository : IQuotationRepository
    {
        static ServiceLayerCN serviceLayer = new ServiceLayerCN();
        public async Task<Quotation> GetQuotation(int DocNum)
        {
            try
            {
                if (serviceLayer.loginResponse.Token < DateTime.Now)
                {
                    serviceLayer.Login();
                }

                var quotation = new Quotation();
                var path = @"$crossjoin(Quotations,BusinessPartners)?$expand=Quotations($select=DocNum,DocEntry,CardName,Address),BusinessPartners($select=ContactPerson)&$filter=Quotations/CardCode eq BusinessPartners/CardCode and Quotations/DocNum eq " + DocNum;
                serviceLayer.client.DefaultRequestHeaders.Add("Cookie", "B1SESSION="+serviceLayer.loginResponse.SessionId+";HttpOnly");
                HttpResponseMessage response = await serviceLayer.client.GetAsync(path);
                if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString()=="401")
                {
                    serviceLayer = new ServiceLayerCN();
                    await GetQuotation(DocNum);
                }
                //if(response.IsSuccessStatusCode)
                if (response != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var Jsonquotation = JsonConvert.DeserializeObject<QuotationViewModel>(jsonString);
                    if(Jsonquotation.value.Count > 0)
                    {
                        quotation.DocNum = Jsonquotation.value[0].Quotations.DocNum;
                        quotation.CardName = Jsonquotation.value[0].Quotations.CardName;
                        quotation.Address = Jsonquotation.value[0].Quotations.Address;
                        quotation.DocEntry = Jsonquotation.value[0].Quotations.DocEntry;
                        quotation.ContactPerson = Jsonquotation.value[0].BusinessPartners.ContactPerson == null ? "NO ASIGNADO" : Jsonquotation.value[0].BusinessPartners.ContactPerson;
                    } 
                }
                return quotation;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
