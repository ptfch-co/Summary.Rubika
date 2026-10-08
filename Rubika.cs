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
            public const string SOfSendBotMessage = "تسک ارسال پیغام از طریق بات";
            public const string DOfSendBotMessage = "فعالیتی جهت ارسال پیغام جدید به PV، کانال و گروه از طریق بات پیام رسان روبیکا.";
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