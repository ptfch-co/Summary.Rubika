namespace Summary.Rubika.Workflows.Task.Channel.Message.Send
{
    using Core.Workflows.Display;
    using System;

    public class SendChannelMessageInRubikaDisplay : ActivityDisplayDriver<SendChannelMessageInRubikaTask,
        SendChannelMessageInRubikaViewModel>
    {
        protected override void EditActivity(SendChannelMessageInRubikaTask activity,
            SendChannelMessageInRubikaViewModel model)
        {
            model.Text = activity.Text;
            model.Chat_Id = activity.Chat_Id;
            model.File = activity.File;
        }

        protected override void UpdateActivity(SendChannelMessageInRubikaViewModel model,
            SendChannelMessageInRubikaTask activity)
        {
            activity.Text = model.Text;
            activity.Chat_Id = model.Chat_Id;
            activity.File = model.File;
        }
    }
}