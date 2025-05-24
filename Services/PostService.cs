using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortalMVC.Models;
using System.Net.Http.Json;

namespace NewsPortalMVC.Services;

public class PostService
{
    private readonly HttpClient _http;

    public PostService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _http.GetFromJsonAsync<List<Post>>("https://jsonplaceholder.typicode.com/posts")
               ?? new List<Post>();
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<Post>($"https://jsonplaceholder.typicode.com/posts/{id}")
               ?? new Post();
    }
}