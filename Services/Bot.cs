namespace Summary.Rubika.Services
{
    using Core.Workflows;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;

    public interface IBotService
    {
        Task SendMessageAsync(string text,
            string file,
            string chat_id
        );
    }

    public class BotService : IBotService
    {
        private readonly RubikaSettings _options;
        private readonly HttpRequestClient _client;

        public BotService(IOptions<RubikaSettings> options,
            HttpRequestClient client)
        {
            _options = options.Value;
            _client = client;
        }

        public async Task SendMessageAsync(string text,
            string file,
            string chat_id)
        {
            var token = _options.Token;

            ThrowExceptionIf.TokenIsNullOrEmpty(token);

            if (string.IsNullOrWhiteSpace(file)) await SendTextMessage(
                text,
                chat_id
            );

            else SendFileMessage(

            );
        }

        private async Task SendTextMessage(string text,
            string chat_id)
        {
            var data = new
            {
                text = text,
                chat_id = chat_id
            };

            var response = await _client.SendPostRequestAsync<SendMessageResponseInfo>(
                $"https://botapi.rubika.ir/v3/{_options.Token}/sendMessage",
                data
            );

            if (response.Status != Status.OK) throw new WorkflowException(
                response.Status.ToString(),
                null,
                JsonConvert.SerializeObject(data),
                "کاربر گرامی؛ خطاء ناشناخته رخ داده است. درخواست میشود باگ را به واحد پشتیبانی گزارش دهید.",
                Level.Error
            );
        }

        private void SendFileMessage()
        {

        }
    }
}