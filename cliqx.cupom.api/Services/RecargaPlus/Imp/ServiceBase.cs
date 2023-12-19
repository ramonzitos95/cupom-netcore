using cliqx.cupom.api.Models.RecargaPlus.Request;
using cliqx.cupom.api.Models.RecargaPlus.Response;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace cliqx.cupom.api.Services.RecargaPlus.Imp
{
    public class ServiceBase
    {
        private readonly IConfiguration configuration;

        protected string UrlBase { get; set; }
        protected RestClient Client { get; }
        protected string TokenRecarga { get; set; }
        protected string Login { get; set; }
        protected string Senha { get; set; }

        public ServiceBase(IConfiguration configuration)
        {

            if (configuration != null)
            {
                this.configuration = configuration;
                UrlBase = configuration.GetSection("RecargaPlus")["urlAuth"];
                Login = configuration.GetSection("RecargaPlus")["loginAuth"];
                Senha = configuration.GetSection("RecargaPlus")["senhaAuth"];
            }
        }

        protected async Task<string> GetToken()
        {
            var client = new RestClient(UrlBase);
            var request = new RestRequest();
            request.Method = Method.Post;
            var user = new RequestAuth();
            user.login = Login;
            user.password = Senha;
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(user), ParameterType.RequestBody);

            try
            {
                var response = client.Execute<TokenRecarga>(request);

                if (response.StatusCode.Equals(HttpStatusCode.OK) && response.Data != null)
                {
                    TokenRecarga = response.Data.token;
                    return TokenRecarga;
                }

                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Stack: " + ex.StackTrace);
                Console.WriteLine("Token inv�lido!");
                return null;
            }
        }
    }
}
