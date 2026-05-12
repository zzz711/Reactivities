using Application.Core;
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

    public class Handler(AppDbContext context) : IRequestHandler<Query, Result<UserProfile>>
    {
        public async Task<Result<UserProfile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync( x => x.Id == request.UserId, cancellationToken);

            if (user == null)
                return Result<UserProfile>.Failure("Profile not found", 404);
            
            var profile = ActivityMap.MapUserToUserProfile(user);

            return Result<UserProfile>.Success(profile);
        }
    }
}