using System.Collections.Generic;
using System.Text;
using SmartEmailResponder.Models;

namespace SmartEmailResponder.Services;

public static class PromptService
{
    public static string CraftPrompt1(string emailBody, List<Faq> faqs)
    {
        var promptBuilder = new StringBuilder(); 
        promptBuilder.Append($@"Here is an email with a question:
{emailBody}


And here is a list of frequently asked questions:
");

        foreach (var faq in faqs)
        {
            promptBuilder.Append($"-{faq.Question}\n");
        }

        promptBuilder.Append("\n\nIs there a frequently asked question that answers the email? Answer yes or no:");

        return promptBuilder.ToString();
    }
    
    public static string CraftPrompt2(string emailBody, List<Faq> faqs)
    {
        var promptBuilder = new StringBuilder(); 
        promptBuilder.Append($@"Here is an email with a question:
{emailBody}


And here is a list of frequently asked questions:
");

        foreach (var faq in faqs)
        {
            promptBuilder.Append($"-{faq.Question}\n");
        }

        promptBuilder.Append("\n\nWhich of the frequently asked questions answers the email?");

        return promptBuilder.ToString();
    }
}