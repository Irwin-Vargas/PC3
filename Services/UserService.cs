using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortalMVC.Models;
using System.Net.Http.Json;


namespace NewsPortalMVC.Services;

public class UserService
{
    private readonly HttpClient _http;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<User>($"https://jsonplaceholder.typicode.com/users/{id}")
               ?? new User();
    }
}