using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortalMVC.Models;
using System.Net.Http.Json;

namespace NewsPortalMVC.Services;

public class CommentService
{
    private readonly HttpClient _http;

    public CommentService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _http.GetFromJsonAsync<List<Comment>>($"https://jsonplaceholder.typicode.com/comments?postId={postId}")
               ?? new List<Comment>();
    }
}