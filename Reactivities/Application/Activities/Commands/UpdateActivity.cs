using System;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest
    {
        public required Activity Activity;
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken)
                ?? throw new Exception("Activity not found");

            mapper.Map(request.Activity, activity);
            Console.WriteLine(activity.Title);
            Console.WriteLine(activity.Description);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
