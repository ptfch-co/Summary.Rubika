namespace Summary.Rubika.Workflows.Task.Bot.Message.Send
{
    using Core.Workflows.Display;
    using System;

    public class SendBotMessageInRubikaDisplay : ActivityDisplayDriver<SendBotMessageInRubikaTask,
        SendBotMessageInRubikaViewModel>
    {
        protected override void EditActivity(SendBotMessageInRubikaTask activity,
            SendBotMessageInRubikaViewModel model)
        {
            model.Text = activity.Text;
            model.ChatId = activity.Chat_Id;
            model.File = activity.File;
        }

        protected override void UpdateActivity(SendBotMessageInRubikaViewModel model,
            SendBotMessageInRubikaTask activity)
        {
            activity.Text = model.Text;
            activity.Chat_Id = model.ChatId;
            activity.File = model.File;
        }
    }
}