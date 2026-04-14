using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands
{
    public class EditActivity
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken) 
                    ?? throw new Exception("Cannot find activity");

                //todo: remove auto mapper
               // mapper.Map(request.Activity, activity);
                activity = ActivityMap.MapActivity(request.Activity);

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}