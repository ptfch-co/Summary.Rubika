namespace Summary.Rubika.Workflows.Task.Group.Message.Send
{
    using Core.Workflows.Display;
    using System;

    public class SendGroupMessageInRubikaDisplay : ActivityDisplayDriver<SendGroupMessageInRubikaTask,
        SendGroupMessageInRubikaViewModel>
    {
        protected override void EditActivity(SendGroupMessageInRubikaTask activity,
            SendGroupMessageInRubikaViewModel model)
        {
            model.Text = activity.Text;
            model.Chat_Id = activity.Chat_Id;
            model.File = activity.File;
        }

        protected override void UpdateActivity(SendGroupMessageInRubikaViewModel model,
            SendGroupMessageInRubikaTask activity)
        {
            activity.Text = model.Text;
            activity.Chat_Id = model.Chat_Id;
            activity.File = model.File;
        }
    }
}