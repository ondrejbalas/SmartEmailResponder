using System;

namespace SmartEmailResponder;

public class FunctionConfig
{
    public string EmailUser { get; set; }
    public string EmailPassword { get; set; }
    public string PopServer { get; set; }
    public string OpenAiKey { get; set; }

    private FunctionConfig()
    {
        
    }

    public static FunctionConfig Read()
    {
        return new FunctionConfig()
        {
            EmailUser = Environment.GetEnvironmentVariable(nameof(EmailUser)),
            EmailPassword = Environment.GetEnvironmentVariable(nameof(EmailPassword)),
            PopServer = Environment.GetEnvironmentVariable(nameof(PopServer)),
            OpenAiKey = Environment.GetEnvironmentVariable(nameof(OpenAiKey)),
        };
    }
}