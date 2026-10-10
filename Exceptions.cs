namespace Summary.Rubika
{
    using Core.Workflows;
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
    }
}