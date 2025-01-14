﻿using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Open.Tests.Sentry {
    public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions> {
        public static bool IsLoggedIn;
        public TestAuthenticationHandler(IOptionsMonitor<TestAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock) { }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync() {
            var authenticationTicket = new AuthenticationTicket(
                new ClaimsPrincipal(Options.Identity),
                new AuthenticationProperties(),
                "Test Scheme");
            var r = IsLoggedIn
                ? Task.FromResult(AuthenticateResult.Success(authenticationTicket))
                : Task.FromResult(AuthenticateResult.NoResult());
            IsLoggedIn = false;
            return r;
        }
    }
}

    
