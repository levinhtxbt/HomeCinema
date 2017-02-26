using HomeCinema.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HomeCinema.Web.Infrastructure.MessageHandlers
{
    public class HomeCinemaAuthHandler : DelegatingHandler
    {
        private IEnumerable<string> authHeaderValues = null;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                request.Headers.TryGetValues("Authorization", out authHeaderValues);

                if (authHeaderValues == null)
                    return base.SendAsync(request, cancellationToken);

                var tokens = authHeaderValues.FirstOrDefault();
                tokens = tokens.Replace("Basic", "").Trim();
                if (!string.IsNullOrEmpty(tokens))
                {
                    byte[] data = Convert.FromBase64String(tokens);
                    string decodedString = Encoding.UTF8.GetString(data);
                    string[] tokensValues = decodedString.Split(':');
                    var membershipService = request.GetMembershipService();

                    var membershipContext = membershipService.ValidateUser(tokensValues[0], tokensValues[1]);
                    if (membershipContext != null)
                    {
                        IPrincipal principal = membershipContext.Principal;
                        Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }
                    else
                    {
                        return getResponse(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    return getResponse(HttpStatusCode.Forbidden);
                }
                return base.SendAsync(request, cancellationToken);
            }
            catch (Exception)
            {
                return getResponse(HttpStatusCode.Forbidden);
            }
        }

        private Task<HttpResponseMessage> getResponse(HttpStatusCode code)
        {
            var response = new HttpResponseMessage(code);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }
    }
}