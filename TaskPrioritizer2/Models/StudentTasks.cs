namespace TaskPrioritizer2.Models;

public class StudentTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }

    // Marks weight percentage (e.g., 20 for 20%)
    public double MarksWeight { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Computed Priority Score (higher = more urgent/important)
    public double PriorityScore
    {
        get
        {
            double daysLeft = (DueDate - DateTime.UtcNow).TotalDays;
            if (daysLeft < 0.5) daysLeft = 0.5; // Prevent division by zero / negative spike

            // 60% importance on mark weight, 40% importance on urgency (days left)
            double weightScore = MarksWeight * 0.6;
            double urgencyScore = (1.0 / daysLeft) * 40.0;

            return Math.Round(weightScore + urgencyScore, 2);
        }
    }
}