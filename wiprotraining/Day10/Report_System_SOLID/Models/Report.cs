namespace Report_System.Models
{
    // Base report class adhering to Liskov Substitution Principle
    public abstract class Report
    {
        public abstract string GetContent();
    }
}