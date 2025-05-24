using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortalMVC.Models;

public class PostEnriched
{
    public Post Post { get; set; } = new();
    public User User { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
