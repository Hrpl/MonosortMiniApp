
namespace MonosortMiniApp.Domain.Models;

public class OrderModel
{
    public int UserId { get; set; }
    public int WaitingTime { get; set; }
    public int SummaryPrice { get; set; }
    public string? Status { get; set; }
}
