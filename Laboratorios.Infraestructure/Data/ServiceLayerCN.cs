using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using RestSharp;
//using Sap.Data.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static Laboratorios.Core.ViewModels.LoginSapViewModel;

namespace OrderTrack.Infraestructure.Data
{
    public class ServiceLayerCN
    {
        public HttpClient client = new HttpClient();
        public LoginResponse loginResponse = new LoginResponse();
        public ServiceLayerCN()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri("https://172.16.146.16:50000/b1s/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //static string variableConexionBD = @"Server = 172.16.146.16:30013; UserID = B1USER; Password = GrupoCB1Admin; databaseName = NDB; Current Schema = TS_ZOLIHN;";
        //static string ConectionBD = @"Server = 172.16.146.16:30013; UserID = B1USER; Password = GrupoCB1Admin; databaseName = NDB; Current Schema = DEVELOPERS_TS_ZOLIHN;Pooling=false";

        //public ServiceLayerConnection con = new ServiceLayerConnection();
        public async void Login(string username = "HNTSDS01", string password = "Admin.123#")
        {
            try
            {
                //var credentials = new LoginBody() { CompanyDB = "DEVELOPERS_TS_ZOLIHN", UserName = "HNTSIT03", Password = "Admin.123#" };
                //var credentials = new LoginBody() { CompanyDB = "IA_PRODHN", UserName = "ADMIN", Password = "PAdm#37col" };
                var credentials = new LoginBody()
                {
                    //CompanyDB = "PRUEBAS_IA_PRODHN",
                    CompanyDB = "IA_PRODHN",
                    UserName = username,
                    Password = password
                };
                //var credentials = new LoginBody()
                //{
                //    CompanyDB = "PRUEBA_IACA",
                //    UserName = "HNTSDS01",
                //    Password = "Admin.123#"
                //};
                var stringcredentials = JsonConvert.SerializeObject(credentials);
                var httpContent = new StringContent(stringcredentials, Encoding.UTF8, "application/json");

                var loginRequest = new RestRequest("Login");
                loginRequest.AddJsonBody(new LoginBody() { CompanyDB = "yourCompanyDb", UserName = "user1", Password = "myPassword" });

                HttpResponseMessage response1 = client.PostAsync("Login", httpContent).Result;
                if (response1 != null)
                {
                    var jsonstring = await response1.Content.ReadAsStringAsync();
                    loginResponse = JsonConvert.DeserializeObject<LoginResponse>(jsonstring);
                    loginResponse.Token = DateTime.Now.AddMinutes(30);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
