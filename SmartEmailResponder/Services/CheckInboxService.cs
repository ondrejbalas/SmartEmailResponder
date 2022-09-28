using System.Collections.Generic;

namespace SmartEmailResponder.Services;

public class CheckInboxService
{
    private FunctionConfig _config;
    
    public CheckInboxService(FunctionConfig config)
    {
        _config = config;
    }

    private HashSet<string> ProcessedMessages { get; set; } = new HashSet<string>();

    public IEnumerable<string> GetEmails()
    {
        using var mailClient = new MailKit.Net.Pop3.Pop3Client();
        mailClient.Connect(_config.PopServer, 995, true);
        mailClient.Authenticate(_config.EmailUser, _config.EmailPassword);

        var messageCount = mailClient.GetMessageCount();
        for (int i = 0; i < messageCount; i++)
        {
            var message = mailClient.GetMessage(i);
            if (!ProcessedMessages.Contains(message.MessageId))
            {
                ProcessedMessages.Add(message.MessageId);
                var body = message.TextBody.Replace("\r\n", "\n");
                while (body.Contains("\n\n"))
                {
                    body = body.Replace("\n\n", "\n");
                }
                yield return body;
            }
        }

        mailClient.Disconnect(true);
    }
}