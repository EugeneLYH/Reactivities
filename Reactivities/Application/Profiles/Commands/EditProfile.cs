using System;
using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands;

public class EditProfile
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditProfileDto EditProfileDto;
    }

    public class Handler(IUserAccessor userAccessor, IMapper mapper, AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            User user = await userAccessor.GetUserAsync();

            if (request.EditProfileDto == null)
            {
                return Result<Unit>.Failure("EditProfileDto not found", 404);
            }

            mapper.Map(request.EditProfileDto, user);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update user data", 400);
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
