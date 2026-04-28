using Application.Activities.DTO;
using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public class Command : IRequest<Result<string>>
    {
        public required CreateActivityDTO ActivityDto { get; set; }
    }

    public class Handler(AppDbContext context/*, IValidator<Command> validator*/) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            //TODO: enable this after moving away from mediatR
            //await validator.ValidateAndThrowAsync(request, cancellationToken);

            var activity = ActivityMap.MapActivityDTO(request.ActivityDto);

            context.Activities.Add(activity);

            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
                return Result<string>.Failure("Failed to update activity", 400);

            return Result<string>.Success(activity.Id);
        }
    }
}