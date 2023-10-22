namespace Archaeoboard.Data.Models;
public class Thread
{
    public long ThreadID { get; set; }
    public string? Title { get; set; }
    public string? Board { get; set; }
    public string? FirstPost { get; set; }
    public string? LastPost { get; set; }
    public string? LastBump { get; set; }
    public long PostAmount { get; set; }
    public IEnumerable<Post>? Posts { get; set; }
}
