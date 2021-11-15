using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Presistence;
using Aplication.Core;

namespace Aplication.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<Activity>>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<List<Activity>>>
        {
            private readonly DataContext _context;
            private readonly ILogger<List> _logger;
            public Handler(DataContext context, ILogger<List> logger)
            {
                this._logger = logger;
                this._context = context;
            }


            public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
            {

                /*try
                {
                    for (var i = 0; i < 10; i++)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        await Task.Delay(1000, cancellationToken);
                        _logger.LogInformation($"Task {i} has completed");
                    }
                }
                catch (Exception e) when (e is TaskCanceledException)
                {
                    _logger.LogInformation("Task was cancelled");
                }*/

                return Result<List<Activity>>.Success( await _context.Activities.ToListAsync(cancellationToken));
            }
        }
    }
}