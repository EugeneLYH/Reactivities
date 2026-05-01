using System;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    // Constructor
    private readonly AppDbContext _context;
    public ActivitiesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await _context.Activities.ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if(activity == null)
        {
            return BadRequest("Unable to find Activity");
        }
        return activity;
    }
}
