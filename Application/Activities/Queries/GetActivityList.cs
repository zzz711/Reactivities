using Application.Activities.DTO;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityList
{
    public class Query : IRequest<List<ActivityDto>> { }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Query, List<ActivityDto>>
    {
        public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await context.Activities.Include(x => x.Attendees).ThenInclude(x => x.User).ToListAsync(cancellationToken);
            List<ActivityDto> activityList = [];

                var currentUserId = userAccessor.GetUserId();
                var followers = await context.UserFollowings.Where(x => x.TargetId != currentUserId).ToListAsync();
                
                
                // if (followers.Any(x => x.FollowerId == currentUserId))
                //     profile.IsFollowing = true;

            

            foreach (var activity in activities)
            {                
                activityList.Add(ActivityMap.MapActivityDTO(activity, activity.Attendees, currentUserId, followers));
            }


            return activityList;
        }
    }
}