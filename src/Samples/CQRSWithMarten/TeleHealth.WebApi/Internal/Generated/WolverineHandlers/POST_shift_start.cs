// <auto-generated/>
#pragma warning disable
using Marten;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using Wolverine.Http;

namespace Internal.Generated.WolverineHandlers
{
    // START: POST_shift_start
    public class POST_shift_start : Wolverine.Http.HttpHandler
    {
        private readonly Wolverine.Http.WolverineHttpOptions _options;
        private readonly Marten.ISessionFactory _sessionFactory;

        public POST_shift_start(Wolverine.Http.WolverineHttpOptions options, Marten.ISessionFactory sessionFactory) : base(options)
        {
            _options = options;
            _sessionFactory = sessionFactory;
        }



        public override async System.Threading.Tasks.Task Handle(Microsoft.AspNetCore.Http.HttpContext httpContext)
        {
            await using var querySession = _sessionFactory.QuerySession();
            await using var documentSession = _sessionFactory.OpenSession();
            var (command, jsonContinue) = await ReadJsonAsync<TeleHealth.WebApi.StartProviderShift>(httpContext);
            if (jsonContinue == Wolverine.HandlerContinuation.Stop) return;
            (var board, var provider, var result1) = await TeleHealth.WebApi.StartProviderShiftEndpoint.LoadAsync(command, querySession).ConfigureAwait(false);
            if (!(result1 is Wolverine.Http.WolverineContinue))
            {
                await result1.ExecuteAsync(httpContext).ConfigureAwait(false);
                return;
            }

            var result2 = TeleHealth.WebApi.StartProviderShiftEndpoint.Validate(provider, board);
            if (!(result2 is Wolverine.Http.WolverineContinue))
            {
                await result2.ExecuteAsync(httpContext).ConfigureAwait(false);
                return;
            }

            (var shiftStartingResponse, var startStream) = TeleHealth.WebApi.StartProviderShiftEndpoint.Create(command, board, provider);
            
            // Placed by Wolverine's ISideEffect policy
            startStream.Execute(documentSession);

            ((Wolverine.Http.IHttpAware)shiftStartingResponse).Apply(httpContext);
            
            // Commit any outstanding Marten changes
            await documentSession.SaveChangesAsync(httpContext.RequestAborted).ConfigureAwait(false);

            await WriteJsonAsync(httpContext, shiftStartingResponse);
        }

    }

    // END: POST_shift_start
    
    
}

