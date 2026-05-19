using Application.Activities.DTO;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands;

public class AddComment
{
    public class Command : IRequest<Result<CommentDto>>
    {
        public required string Body { get; set; }
        public required string ActivityId { get; set; }
    }

    public class Handler(AppDbContext context, IUserAccessor userAccessor) : IRequestHandler<Command, Result<CommentDto>>
    {
        public async Task<Result<CommentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.ActivityId, cancellationToken);

            if (activity == null)
                return Result<CommentDto>.Failure("Could not find activity", 404);

            var user = await userAccessor.GetUserAsync();

            var comment = new Comment
            {
                UserId = user.Id,
                ActivityId = activity.Id,
                Body = request.Body,
                User = user
            };

            activity.Comments.Add(comment);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            return result ? Result<CommentDto>.Success(ActivityMap.MapComment(comment)) 
                : Result<CommentDto>.Failure("Failed to add comment", 400);
        }
    }
}