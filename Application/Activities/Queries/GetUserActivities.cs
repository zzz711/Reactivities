using Application.Core;
using Application.Profiles.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetUserActivities
{
    public class Query : IRequest<Result<List<UserActivity>>>
    {
        public required string UserId { get; set; }
        public required string Filter { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Result<List<UserActivity>>>
    {
        public async Task<Result<List<UserActivity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = context.ActivityAttendees.Where(x => x.UserId == request.UserId).OrderBy(a => a.Activity.Date).Select(s => s.Activity).AsQueryable();

            var today = DateTime.UtcNow;
            query = request.Filter switch
            {
                "past" => query.Where( a => a.Date < today && a.Attendees.Any( x => x.UserId == request.UserId)),
                "future" => query.Where( a => a.Date >= today && a.Attendees.Any( x => x.UserId == request.UserId)),
                "hosting" => query.Where( a => a.Attendees.Any(x => x.UserId == request.UserId)),
                _ => query.Where(x => x.Date == today && x.Attendees.Any(x => x.UserId == request.UserId))
            };

            var activities =  await query.ToListAsync(cancellationToken);
            List<UserActivity> userActivities = [];

            foreach (var userActivity in activities)
            {
                userActivities.Add(ActivityMap.MapUserActivity(userActivity));
            }

            return Result<List<UserActivity>>.Success(userActivities);
        }
    }
}