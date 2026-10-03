namespace Summary.Rubika.Controllers.Api.V1
{
    using Core.Modules;
    using Core.Workflows.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Summary.Query.Services;
    using Summary.Rubika.Workflows.Event.Bot.Message.Receive;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    [Feature(Rubika.Feature.Rubika)]
    [EnableCors("Everywhere")]
    [Route("api/v1/rubika/[controller]/receive")]
    public class BotController : Controller
    {
        private readonly IWorkflowManager _workflow;

        public BotController(IWorkflowManager workflow)
        {
            _workflow = workflow;
        }

        [HttpPost]
        [Route(nameof(Message))]
        public async Task<IActionResult> Message([FromBody] ReceiveUpdateFormInfo form)
        {
            var input = new Dictionary<string, object>
            {
                { "Rubika.Type", form.Update.Type },
                { "Rubika.Chat.Id", form.Update.Chat_Id },
                { "Rubika.Message", form.Update.New_Message.Text },
                { "Rubika.Message.Id", form.Update.New_Message.Message_Id },
                { "Rubika.Message.Time", form.Update.New_Message.Time },
                { "Rubika.Message.IsEdited", form.Update.New_Message.Is_Edited },
                { "Rubika.Message.Sender.Type", form.Update.New_Message.Sender_Type },
                { "Rubika.Message.Sender.Id", form.Update.New_Message.Sender_Id }
            };

            await _workflow.TriggerIntoDBAsync(
                nameof(ReceiveBotMessageRubikaEvent),
                input
            );

            return Ok();
        }
    }
}