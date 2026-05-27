using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetFollowings
{
    public class Query : IRequest<Result<List<UserProfile>>>
    {
        public string Predicate { get; set; } = "followers";
        public required string UserId { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<List<UserProfile>>>
    {
        public async Task<Result<List<UserProfile>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var profiles = new List<UserProfile>();
            var currentUserId = userAccessor.GetUserId();
            var followers = await context.UserFollowings.Where(x => x.FollowerId != request.UserId).ToListAsync();
            var targets = await context.UserFollowings.Where(x => x.TargetId != request.UserId).ToListAsync();

            switch (request.Predicate)
            {
                case "followers":
                    await context.UserFollowings.Where(x => x.TargetId == request.UserId).Select(x => x.Follower).ForEachAsync(u =>
                     {
                         profiles.Add(ActivityMap.MapUserToUserProfile(u));

                         profiles[^1].FollowersCount = followers.Count;
                         profiles[^1].FollowingCount = targets.Count;
                         if (u.Followings.Any(x => x.FollowerId == currentUserId))
                             profiles[^1].IsFollowing = true;
                     });

                    break;
                case "followings":
                    await context.UserFollowings.Where(x => x.FollowerId == request.UserId).Select(x => x.Target).ForEachAsync(async u =>
                     {
                         profiles.Add(ActivityMap.MapUserToUserProfile(u));

                         profiles[^1].FollowersCount = followers.Count;
                         profiles[^1].FollowingCount = targets.Count;
                         if (u.Followers.Any(x => x.FollowerId == currentUserId))
                             profiles[^1].IsFollowing = true;

                     });
                    break;

                default:
                    break;
            }

            return Result<List<UserProfile>>.Success(profiles);
        }
    }
}