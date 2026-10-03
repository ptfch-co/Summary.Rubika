using Newtonsoft.Json;

namespace Summary.Rubika.Workflows.Event.Bot.Message.Receive
{
    using Core.Workflows;
    using Core.Workflows.Abstractions.Models;
    using Core.Workflows.Activities;
    using Core.Workflows.Models;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReceiveBotMessageRubikaEvent : EventActivity
    {
        private readonly IStringLocalizer<ReceiveBotMessageRubikaEvent> T;
        private readonly HttpRequestClient _client;
        private readonly RubikaSettings _options;

        public ReceiveBotMessageRubikaEvent(
            IStringLocalizer<ReceiveBotMessageRubikaEvent> t,
            HttpRequestClient client,
            IOptions<RubikaSettings> options)
        {
            T = t;
            _client = client;
            _options = options.Value;
        }

        public override string Name => nameof(ReceiveBotMessageRubikaEvent);

        public override LocalizedString DisplayText => T[Rubika.Localization.SOfReceiveBotMessage];

        public override LocalizedString Category => T[Rubika.Public.Category];

        public override IEnumerable<Outcome> GetPossibleOutcomes(
            WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(T[Rubika.Workflows.Done]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(
            WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            var message = workflowContext.Input["Rubika.Message"].ToString();
            var chat_id = workflowContext.Input["Rubika.Chat.Id"].ToString();

            if (message == "/start" && string.IsNullOrWhiteSpace(_options.FirstReplayMessage) is false)
            {
                var body = new
                {
                    chat_id = chat_id,
                    text = _options.FirstReplayMessage
                };

                var response = await _client.SendPostRequestAsync<SendMessageResponseInfo>(
                    $"https://botapi.rubika.ir/v3/{_options.Token}/sendMessage",
                    body
                );

                if (response.Status != Status.OK) throw new WorkflowException(
                    "سرویس پاسخگو خودکار روبیکار با خطاء مواجه شد.",
                    null,
                    JsonConvert.SerializeObject(body),
                    "کارشناس پشتیبانی؛ در صورت مشاهده این خطاء تیکت را به سطح بعدی ارجاع دهید."
                );
            }

            return Outcomes(Rubika.Workflows.Done);
        }
    }
}