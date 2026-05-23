using System;
using Application.Activities.DTO;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditActivityDto ActivityDto;
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var ActivityDto = await context.Activities.FindAsync([request.ActivityDto.Id], cancellationToken);

            if(ActivityDto == null) return Result<Unit>.Failure("ActivityDto not found", 404);

            mapper.Map(request.ActivityDto, ActivityDto);
            Console.WriteLine(ActivityDto.Title);
            Console.WriteLine(ActivityDto.Description);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to edit activity", 400);
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
