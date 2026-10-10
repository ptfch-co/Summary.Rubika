namespace Summary.Rubika.Workflows.Task.PV.Message.Send
{
    using Core.Workflows.Display;
    using System;

    public class SendPVMessageInRubikaDisplay : ActivityDisplayDriver<SendPVMessageInRubikaTask,
        SendPVMessageInRubikaViewModel>
    {
        protected override void EditActivity(SendPVMessageInRubikaTask activity,
            SendPVMessageInRubikaViewModel model)
        {
            model.Text = activity.Text;
            model.Chat_Id = activity.Chat_Id;
            model.File = activity.File;
        }

        protected override void UpdateActivity(SendPVMessageInRubikaViewModel model,
            SendPVMessageInRubikaTask activity)
        {
            activity.Text = model.Text;
            activity.Chat_Id = model.Chat_Id;
            activity.File = model.File;
        }
    }
}