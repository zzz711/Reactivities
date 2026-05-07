using Application.Activities.DTO;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries
{
    public class GetActivityDetails
    {
        public class Query : IRequest<Result<ActivityDto>>
        {
            public required string Id { get; set; }
        }

        public class Handler(AppDbContext context) : IRequestHandler<Query, Result<ActivityDto>>
        {
            public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await context.Activities
                    .Include(x => x.Attendees)
                    .ThenInclude(x => x.User)
                    .FirstOrDefaultAsync(x => request.Id == x.Id, cancellationToken);

                if (activity == null) return Result<ActivityDto>.Failure("Activity not found", 404);

                var mappedActivity = ActivityMap.MapActivityDTO(activity, activity.Attendees);

                return Result<ActivityDto>.Success(mappedActivity);
            }
        }
    }
}