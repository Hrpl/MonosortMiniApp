
namespace MonosortMiniApp.Domain.Models;

public class OrderModel
{
    public int UserId { get; set; }
    public int WaitingTime { get; set; }
    public int SummaryPrice { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
}
