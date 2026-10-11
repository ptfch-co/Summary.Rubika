namespace Summary.Rubika.Services
{
    using Core.Workflows;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System;
    using System.Threading.Tasks;
    using System.IO;
    using System.Net.Http;
    using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _memory;

        public BotService(IOptions<RubikaSettings> options,
            HttpRequestClient client,
            IMemoryCache memory)
        {
            _options = options.Value;
            _client = client;
            _memory = memory;
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

            else await SendFileMessage(
                text,
                file,
                chat_id
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

        private async Task SendFileMessage(string text,
            string file,
            string chat_id)
        {
            var fileName = Path.GetFileName(new Uri(file).LocalPath);

            if (string.IsNullOrEmpty(fileName)) fileName = "downloaded_file";

            var file_type = GetFileType(fileName);

            if (!_memory.TryGetValue(file, out string file_Id))
            {
                var requestData = new { type = file_type };

                var uploadInfo = await _client.SendPostRequestAsync<RequestSendFileResponseInfo>(
                    $"https://botapi.rubika.ir/v3/{_options.Token}/requestSendFile",
                    requestData
                );

                ThrowExceptionIf.UploadFileIsNotOk(uploadInfo.Status, requestData);

                using var stream = await _client.SendGetRequestAsync<Stream>(file);

                using var formData = new MultipartFormDataContent
                {
                    { new StreamContent(stream), "file", fileName }
                };

                var uploadResult = await _client.SendPostRequestAsync<UploadFileResponseInfo>(
                    uploadInfo.Data.Upload_Url,
                    formData
                );

                ThrowExceptionIf.UploadFileIsNotOk(uploadResult.Status, new { uploadInfo.Data.Upload_Url });

                file_Id = uploadResult.Data.File_Id;

                _memory.Set(file, file_Id, TimeSpan.FromHours(24));
            }

            var data = new
            {
                chat_id = chat_id,
                file_id = file_Id,
                text = text
            };

            var response = await _client.SendPostRequestAsync<SendMessageResponseInfo>(
                $"https://botapi.rubika.ir/v3/{_options.Token}/sendFile",
                data
            );

            ThrowExceptionIf.UploadFileIsNotOk(response.Status, data);
        }

        private string GetFileType(string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".webp":
                    return "Image";

                case ".mp4":
                    return "Video";

                case ".mp3":
                    return "Music";

                default:
                    return "File";
            }
        }
    }
}