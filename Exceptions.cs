namespace Summary.Rubika
{
    using Core.Workflows;
    using Newtonsoft.Json;
    using System;

    public class ThrowExceptionIf
    {
        public static void TokenIsNullOrEmpty(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) throw new WorkflowException(
                "مقدار پارامتر توکن خالی است.",
                null,
                "",
                "کاربر گرامی؛ مقدار پارامتر توکن در تنظیمات افزونه مقداردهی نشده است. درخواست میشود نسبت به پیکربندی افزونه اقدام فرمایید."
            );
        }

        public static void UploadFileIsNotOk(Status status, object data)
        {
            if (status != Status.OK) throw new WorkflowException(
                status.ToString(),
                null,
                JsonConvert.SerializeObject(data),
                "کاربر گرامی؛ خطاء ناشناخته رخ داده است. درخواست میشود باگ را به واحد پشتیبانی گزارش دهید.",
                Level.Error
            );
        }
    }
}