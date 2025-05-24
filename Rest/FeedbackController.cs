using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPortalMVC.Models;

namespace NewsPortalMVC.Rest;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly FeedbackContext _context;

    public FeedbackController(FeedbackContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Feedback feedback)
    {
        if (_context.Feedbacks.Any(f => f.PostId == feedback.PostId))
            return BadRequest("Ya existe feedback para este post.");

        feedback.Fecha = DateTime.UtcNow;
        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Feedbacks.ToList());
    }
}