using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SmartEmailResponder.Data;
using SmartEmailResponder.Services;

namespace SmartEmailResponder
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var config = FunctionConfig.Read();
            var faqs = FaqLoader.GetAllFaqs();
            var inboxService = new CheckInboxService(config);
            var answerService = new EmailAnsweringService(config, faqs.ToList());
            
            foreach (var email in inboxService.GetEmails())
            {
                var disposition = answerService.GetEmailDisposition(email).Result;
                log.LogInformation($"An email has been processed:\nEmail Body: {email}\nCan answer: {(disposition.CanReply ? "Yes" : "No")}\nThe Answer: {disposition.TheReply}");
            }
        }
    }
}
