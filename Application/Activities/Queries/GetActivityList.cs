using Application.Activities.DTO;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityList
{
    public class Query : IRequest<List<ActivityDto>> { }

    public class Handler(AppDbContext context) : IRequestHandler<Query, List<ActivityDto>>
    {
        public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await context.Activities.Include(x => x.Attendees).ThenInclude(x => x.User).ToListAsync(cancellationToken);
            List<ActivityDto> activityList = [];

            foreach (var activity in activities)
            {
                activityList.Add(ActivityMap.MapActivityDTO(activity, activity.Attendees));
            }


            return activityList;
        }
    }
}