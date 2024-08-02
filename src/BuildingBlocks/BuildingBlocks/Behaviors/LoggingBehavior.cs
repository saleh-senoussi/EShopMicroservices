using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("[Start] Handle Request={Request} - Response={Response}", typeof(TRequest).Name,
            typeof(TResponse).Name);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();
        
        timer.Stop();
        var timeTaken = timer.Elapsed;
        if (timeTaken.Seconds > 3)
        {
            logger.LogWarning("[Performance] The request {Request} took {TimeTaken}", typeof(TRequest).Name,
                timeTaken.Seconds);
        }
        
        logger.LogInformation("[END] Handled Request={Request} - Response={Response}", typeof(TRequest).Name,
            typeof(TResponse).Name);

        return response;
    }
}