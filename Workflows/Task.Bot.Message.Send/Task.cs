namespace Summary.Rubika.Workflows.Task.Bot.Message.Send
{
    using Core.Workflows.Abstractions.Models;
    using Core.Workflows.Activities;
    using Core.Workflows.Models;
    using Microsoft.Extensions.Localization;
    using Summary.Rubika.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SendBotMessageInRubikaTask : TaskActivity
    {
        private readonly IStringLocalizer<SendBotMessageInRubikaTask> T;
        private IBotService _bot;

        public SendBotMessageInRubikaTask(IStringLocalizer<SendBotMessageInRubikaTask> t,
            IBotService bot)
        {
            T = t;
            _bot = bot;
        }

        public override string Name => nameof(SendBotMessageInRubikaTask);

        public override LocalizedString DisplayText => T[Rubika.Localization.SOfSendMessage];

        public override LocalizedString Category => T[Rubika.Public.Category];

        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            return Outcomes(
                T[Rubika.Workflows.Done]
            );
        }

        public string Text
        {
            get => GetProperty(() => string.Empty);
            set => SetProperty(value);
        }

        public string File
        {
            get => GetProperty(() => string.Empty);
            set => SetProperty(value);
        }

        public string Chat_Id
        {
            get => GetProperty(() => string.Empty);
            set => SetProperty(value);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext,
            ActivityContext activityContext)
        {
            var text = workflowContext.GetInputOrDefault(Text);
            var file = workflowContext.GetInputOrDefault(File);
            var chat_id = workflowContext.GetInputOrDefault(Chat_Id);

            await _bot.SendMessageAsync(text,
                file,
                chat_id
            );

            return Outcomes(Rubika.Workflows.Done);
        }
    }
}