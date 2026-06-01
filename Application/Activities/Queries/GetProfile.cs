using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetProfile
{
    public class Query : IRequest<Result<UserProfile>>
    {
        public required string UserId { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Query, Result<UserProfile>>
    {
        public async Task<Result<UserProfile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            var followers = await context.UserFollowings.Where(x => x.FollowerId != request.UserId).ToListAsync();
            var targets = await context.UserFollowings.Where(x => x.TargetId != request.UserId).ToListAsync();

            if (user == null)
                return Result<UserProfile>.Failure("Profile not found", 404);

            user.Followers = followers;
            user.Followings = targets;

            var profile = ActivityMap.MapUserToUserProfile(user);

            var currentUserId = userAccessor.GetUserId();
            if (followers.Any(x => x.FollowerId == currentUserId))
                profile.IsFollowing = true;

            return Result<UserProfile>.Success(profile);
        }
    }
}