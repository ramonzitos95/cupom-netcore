using System.Net.Http.Headers;

namespace cliqx.cupom.api.Services.RecargaPlus.Imp
{
    public class TokenHandler : HttpClientHandler
    {

        private readonly Func<Task<string>> _getTokenFunc;

        public TokenHandler(Func<Task<string>> getTokenFunc)
        {
            _getTokenFunc = getTokenFunc;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string token = await _getTokenFunc();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
