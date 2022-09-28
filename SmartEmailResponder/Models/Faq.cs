namespace SmartEmailResponder.Models;

public class Faq
{
    public string Question { get; set; }
    public string Answer { get; set; }

    public Faq(string question, string answer)
    {
        Question = question;
        Answer = answer;
    }
}