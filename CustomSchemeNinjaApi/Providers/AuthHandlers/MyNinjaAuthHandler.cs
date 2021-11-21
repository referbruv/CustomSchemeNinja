using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using CustomSchemeNinjaApi.Providers.AuthHandlers.Constants;
using CustomSchemeNinjaApi.Providers.AuthHandlers.Models;
using CustomSchemeNinjaApi.Providers.AuthHandlers.Scheme;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace CustomSchemeNinjaApi.Providers.AuthHandlers
{
    public class MyNinjaAuthHandler
        : AuthenticationHandler<MyNinjaAuthSchemeOptions>
    {
        public MyNinjaAuthHandler(
            IOptionsMonitor<MyNinjaAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TokenModel model;

            // validation comes in here
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
            }

            var header = Request.Headers[HeaderNames.Authorization].ToString();
            var tokenMatch = Regex.Match(header, AuthSchemeConstants.NToken);

            if (tokenMatch.Success)
            {
                // the token is captured in this group
                // as declared in the Regex
                var token = tokenMatch.Groups["token"].Value;

                try
                {
                    // convert the input token down from Base64 into normal
                    byte[] fromBase64String = Convert.FromBase64String(token);
                    var parsedToken = Encoding.UTF8.GetString(fromBase64String);

                    // deserialize the JSON string obtained from the byte array
                    model = JsonConvert.DeserializeObject<TokenModel>(parsedToken);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception Occured while Deserializing: " + ex);
                    return Task.FromResult(AuthenticateResult.Fail("TokenParseException"));
                }

                // success branch
                // generate authTicket
                // authenticate the request
                if (model != null)
                {
                    // create claims array from the model
                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                    new Claim(ClaimTypes.Email, model.EmailAddress),
                    new Claim(ClaimTypes.Name, model.Name) };

                    // generate claimsIdentity on the name of the class
                    var claimsIdentity = new ClaimsIdentity(claims,
                                nameof(MyNinjaAuthHandler));

                    // generate AuthenticationTicket from the Identity
                    // and current authentication scheme
                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                    // pass on the ticket to the middleware
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
            }

            // failure branch
            // return failure
            // with an optional message
            return Task.FromResult(AuthenticateResult.Fail("Model is Empty"));
        }
    }
}