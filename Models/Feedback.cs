using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPortalMVC.Models;

public class Feedback
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string Sentimiento { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
}