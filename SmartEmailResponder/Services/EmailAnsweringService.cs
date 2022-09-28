using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenAI.GPT3;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using SmartEmailResponder.Models;

namespace SmartEmailResponder.Services;

public class EmailAnsweringService
{
    private FunctionConfig _config;
    private List<Faq> _faqs;
    private readonly OpenAIService _openAiService;

    public EmailAnsweringService(FunctionConfig config, List<Faq> faqs)
    {
        _config = config;
        _faqs = faqs;
        
        _openAiService = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = _config.OpenAiKey
        });
    }

    public async Task<EmailDisposition> GetEmailDisposition(string emailBody)
    {
        var canAnswer = await CanAnswer(emailBody);
        if (canAnswer)
        {
            var questionWhichAnswers = await WhichQuestionAnswers(emailBody);
            if (questionWhichAnswers != null)
            {
                return new EmailDisposition()
                {
                    CanReply = true,
                    TheReply = questionWhichAnswers.Answer
                };
            }
        }

        return new EmailDisposition() { CanReply = false };
    }

    private async Task<bool> CanAnswer(string emailBody)
    {
        var prompt = PromptService.CraftPrompt1(emailBody, _faqs);
        var result = await CallOpenAi(prompt, 0.9f);
        if (result != null)
        {
            int indexNo = result.IndexOf("No", StringComparison.OrdinalIgnoreCase);
            int indexYes = result.IndexOf("Yes", StringComparison.OrdinalIgnoreCase);
            bool isYes = (indexYes >= 0 && (indexNo == -1 || indexNo > indexYes));
            return isYes;
        }

        return false;
    }

    private async Task<Faq> WhichQuestionAnswers(string emailBody)
    {
        var prompt = PromptService.CraftPrompt2(emailBody, _faqs);
        var result = await CallOpenAi(prompt, 0.7f);
        if (result != null)
        {
            var indexQuestionEndsAt = result.IndexOf('?', StringComparison.Ordinal);
            if (indexQuestionEndsAt >= 0)
            {
                var question = result.Substring(0, indexQuestionEndsAt + 1).Trim();
                var faqQuestion = _faqs.FirstOrDefault(x => x.Question.Equals(question, StringComparison.OrdinalIgnoreCase));
                return faqQuestion;
            }
        }

        return null;
    }

    private async Task<string> CallOpenAi(string input, float temperature)
    {
        var completionResult = await _openAiService.Completions.CreateCompletion(new CompletionCreateRequest()
        {
            Prompt = input,
            MaxTokens = 32,
            Temperature = temperature
        }, OpenAI.GPT3.ObjectModels.Models.TextDavinciV2);

        if (completionResult.Successful)
        {
            ;
            return completionResult.Choices.FirstOrDefault().Text;
        }

        return null;
    }
}