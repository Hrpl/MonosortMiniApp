﻿
namespace MonosortMiniApp.Domain.Models;

public class OrderModel
{
    public int UserId { get; set; }
    public int WaitingTime { get; set; }
    public int SummaryPrice { get; set; }
    public int StatusId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
