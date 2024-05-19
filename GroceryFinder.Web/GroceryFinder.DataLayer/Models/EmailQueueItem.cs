using GroceryFinder.DataLayer.Enums;

namespace GroceryFinder.DataLayer.Models;

public class EmailQueueItem
{
    public Guid Id { get; set; }
    public string EmailInfoJson { get; set; }
    public bool IsSent { get; set; }
    public EmailType EmailType { get; set; }
}

