using Application.Core;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries;

public class GetUserActivities
{
    public class Query : IRequest<Result<List<UserActivityDto>>>
    {
        public required string UserId { get; set; }
        public required string Filter { get; set; } // 'past', 'future', 'isHost'
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<UserActivityDto>>>
    {
        public async Task<Result<List<UserActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentDateTime = DateTime.UtcNow;
            if (!await context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken))
            {
                return Result<List<UserActivityDto>>.Failure("User not found", 404);
            }

            var query = context.ActivityAttendees
                .Where(x => x.UserId == request.UserId)
                .AsQueryable();
            query = request.Filter == "hosting" ? query.Where(x => x.IsHost == true) : query;

            var activitiesQuery = query.Select(x => x.Activity);
            activitiesQuery = request.Filter switch
            {
                "past" => activitiesQuery
                    .OrderByDescending(x => x.Date)
                    .Where(x => x.Date < currentDateTime),
                "future" => activitiesQuery
                    .OrderBy(x => x.Date)
                    .Where(x => x.Date >= currentDateTime),
                _ => activitiesQuery
            };

            var projectedActivities = activitiesQuery.ProjectTo<UserActivityDto>(mapper.ConfigurationProvider);

            var activities = await projectedActivities.ToListAsync(cancellationToken);

            return Result<List<UserActivityDto>>.Success(activities);
        }
    }
}
