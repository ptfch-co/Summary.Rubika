using Core.DisplayManagement.Entities;
using Core.DisplayManagement.Handlers;
using Core.DisplayManagement.Views;
using Core.Environment.Shell;
using Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Core.Entities;
using Core.Workflows;
using Microsoft.AspNetCore.Authorization;

namespace Summary.Rubika
{
    public class RubikaSettings
    {
        public string Token { get; set; }
        public string FirstReplayMessage { get; set; }
        public string ActiveEndpoint { get; set; }
    }

    public class RubikaSettingsDisplayDriver : SectionDisplayDriver<ISite,
        RubikaSettings>
    {
        private readonly IShellHost _host;
        private readonly ShellSettings _shell;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IAuthorizationService _authorize;
        private readonly HttpRequestClient _client;

        public RubikaSettingsDisplayDriver(IShellHost host,
            ShellSettings settings,
            IHttpContextAccessor httpContext,
            IAuthorizationService authorize,
            HttpRequestClient client)
        {
            _host = host;
            _shell = settings;
            _httpAccessor = httpContext;
            _authorize = authorize;
            _client = client;
        }

        public override async Task<IDisplayResult> EditAsync(RubikaSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;
            if (user is null || !await _authorize.AuthorizeAsync(user, Permissions.ManageRubikaSettings))
            {
                return null;
            }

            var init = Initialize<RubikaSettings>("RubikaSettings_Edit", model =>
            {
               model.Token = settings.Token;
               model.FirstReplayMessage = settings.FirstReplayMessage;
               model.ActiveEndpoint = settings.ActiveEndpoint;
            });

            return init.Location("Content:5").OnGroup("Rubika");
        }

        public override async Task<IDisplayResult> UpdateAsync(RubikaSettings settings,
            BuildEditorContext context)
        {
            var user = _httpAccessor.HttpContext?.User;

            if (user is null ||
                !await _authorize.AuthorizeAsync(user, Permissions.ManageRubikaSettings)) return null;

            if (context.GroupId != "Rubika") return await EditAsync(settings, context);

            await context.Updater.TryUpdateModelAsync(
                settings,
                Prefix
            );

            if (string.IsNullOrWhiteSpace(settings.Token) is false)
            {
                var body = new
                {
                    url = $"https://{_httpAccessor.HttpContext.Request.Host.Host}/api/v1/rubika/bot/receive/message",
                    type = "ReceiveUpdate"
                };

                settings.ActiveEndpoint = body.url;

                var response = await _client.SendPostRequestAsync<EndpointResponseInfo>(
                    $"https://botapi.rubika.ir/v3/{settings.Token}/updateBotEndpoints",
                    body
                );

                if (response.Status != Status.OK) throw new WorkflowException(
                    "فراخوانی سرویس وب‌هوک با خطاء مواجه شد.",
                    null,
                    null,
                    "کارشناس پشتیبانی؛ در صورت مشاهده این خطاء تیکت را به سطح بعدی ارجاع دهید.",
                    Level.Error
                );
            }

            await _host.ReloadShellContextAsync(_shell);

            return await EditAsync(settings, context);
        }
    }

    public class RubikaSettingsConfiguration : IConfigureOptions<RubikaSettings>
    {
        private readonly ISiteService _site;

        public RubikaSettingsConfiguration(ISiteService site)
        {
            _site = site;
        }

        public void Configure(RubikaSettings options)
        {
            var settings = _site.GetSiteSettingsAsync().GetAwaiter().GetResult().As<RubikaSettings>();
            options.Token = settings.Token;
            options.FirstReplayMessage = settings.FirstReplayMessage;
        }
    }
}