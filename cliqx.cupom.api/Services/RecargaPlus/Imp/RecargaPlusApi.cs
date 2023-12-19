using Refit;

namespace cliqx.cupom.api.Services.RecargaPlus.Imp
{
    public class RecargaPlusApi : ServiceBase
    {
        private readonly HttpClient _httpClient;

        public RecargaPlusApi(IConfiguration configuration, string url) : base(configuration)
        {
            var httpClient = new HttpClient(new TokenHandler(GetToken))
            {
                Timeout = TimeSpan.FromMinutes(5),
                BaseAddress = new Uri(url),
            };

            Client = RestService.For<IRecargaPlusApi>(httpClient);

        }

        public IRecargaPlusApi Client { get; }
    }
}
