using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.Data.Models;

public class Post
{
    public Thread? Thread { get; set; }
    public long ThreadID { get; set; }
    public long PostNumber { get; set; }
    public string? PosterName { get; set; }
    public string? PosterMail { get; set; }
    public string? PosterTrip { get; set; }
    public string? PosterID { get; set; }
    public string? PostDate { get; set; }
    public string? PostContent { get; set; }
}
