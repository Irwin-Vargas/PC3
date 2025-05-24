using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsPortalMVC.Models;
using NewsPortalMVC.Services;
using NewsPortalMVC.Rest;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace NewsPortalMVC.Controllers;

public class HomeController : Controller
{
    private readonly PostService _postService;
    private readonly UserService _userService;
    private readonly CommentService _commentService;
    private readonly HttpClient _http;
    private readonly FeedbackContext _context;

    public HomeController(
        PostService postService,
        UserService userService,
        CommentService commentService,
        IHttpClientFactory httpFactory,
        FeedbackContext context)
    {
        _postService = postService;
        _userService = userService;
        _commentService = commentService;
        _http = httpFactory.CreateClient();
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var posts = await _postService.GetPostsAsync();
        var topPosts = posts.Take(5);

        var enrichedPosts = new List<PostEnriched>();

        foreach (var post in topPosts)
        {
            var user = await _userService.GetUserByIdAsync(post.UserId);
            var comments = await _commentService.GetCommentsByPostIdAsync(post.Id);

            enrichedPosts.Add(new PostEnriched
            {
                Post = post,
                User = user,
                Comments = comments
            });
        }

        return View(enrichedPosts);
    }

    [HttpPost]
    public async Task<IActionResult> React(int postId, string sentimiento)
    {
        var feedback = new Feedback
        {
            PostId = postId,
            Sentimiento = sentimiento,
            Fecha = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(feedback);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var feedbackUrl = Url.Action("Post", "Feedback", new { }, Request.Scheme)!;

        var response = await _http.PostAsync(feedbackUrl, content);

        TempData["Msg"] = response.IsSuccessStatusCode
            ? "Gracias por tu voto"
            : "Ya votaste por este post.";

        return RedirectToAction("Index");
    }

    // ✅ NUEVA ACCIÓN: Mostrar historial de votos
    public IActionResult Historial()
    {
        var feedbacks = _context.Feedbacks
            .OrderByDescending(f => f.Fecha)
            .ToList();

        return View(feedbacks);
    }
}
