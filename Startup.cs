namespace Summary.Rubika
{
    using Core.DisplayManagement.Handlers;
    using Core.Modules;
    using Core.Navigation;
    using Core.Security.Permissions;
    using Core.Settings;
    using Core.Workflows.Helpers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Summary.Query.Services;
    using Summary.Rubika.Services;
    using Summary.Rubika.Workflows.Event.Bot.Message.Receive;
    using Summary.Rubika.Workflows.Task.Channel.Message.Send;
    using Summary.Rubika.Workflows.Task.Group.Message.Send;
    using Summary.Rubika.Workflows.Task.PV.Message.Send;

    [Feature(Rubika.Feature.Rubika)]
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Everywhere", builder =>
                    builder.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddMemoryCache();

            services.AddScoped<INavigationProvider, Menu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            services.AddScoped<IDisplayDriver<ISite>, RubikaSettingsDisplayDriver>();
            services.AddScoped<IQueryService, QueryService>();
            services.AddScoped<IBotService, BotService>();

            services.AddTransient<IConfigureOptions<RubikaSettings>, RubikaSettingsConfiguration>();

            services.AddActivity<ReceiveBotMessageRubikaEvent, ReceiveBotMessageRubikaEventDisplay>();
            services.AddActivity<SendChannelMessageInRubikaTask, SendChannelMessageInRubikaDisplay>();
            services.AddActivity<SendGroupMessageInRubikaTask, SendGroupMessageInRubikaDisplay>();
            services.AddActivity<SendPVMessageInRubikaTask, SendPVMessageInRubikaDisplay>();
        }
    }
}