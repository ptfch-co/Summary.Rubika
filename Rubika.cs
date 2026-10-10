using Newtonsoft.Json.Serialization;

namespace Summary.Rubika
{
    public static class Rubika
    {
        public static class Feature
        {
            public const string Rubika = "Summary.Rubika";
        }

        public static class Localization
        {
            public const string SOfReceiveBotMessage = "رویداد دریافت پیغام‌های ارسالی به بات روبیکا";
            public const string DOfReceiveBotMessage = "این رویداد زمانی فراخوانی میگردد که کاربری بعد از عضویت در بات نسبت به تعامل با بات اقدام نمایید.";
            public const string SOfSendChannelMessage ="ارسال پست به کانال";
            public const string DOfSendChannelMessage = "این فعالیت برای ارسال پست به کانال روبیکا استفاده می‌شود.";
            public const string SOfSendMessage ="ارسال پیام به پیوی";
            public const string DOfSendMessage = "این فعالیت برای ارسال پیام به پیوی روبیکا استفاده می‌شود.";
            public const string SOfSendGroupMessage ="ارسال پیام به گروه";
            public const string DOfSendGroupMessage = "این فعالیت برای ارسال پیام به گروه روبیکا استفاده می‌شود.";
        }

        public static class Public
        {
            public const string Category = "Rubika";
        }

        public static class Workflows
        {
            public const string Done = "Done";
        }
    }
}