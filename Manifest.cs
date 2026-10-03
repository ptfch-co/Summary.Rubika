using Core.Modules.Manifest;
using Summary.Rubika;

[assembly: Feature(
    Id = Rubika.Feature.Rubika,
    Name = "پیام رسان روبیکا",
    Description = "مجموعه‌ای از رویداد و تسک‌ها جهت ارتباط با پیام رسان روبیکا.",
    Category = Rubika.Public.Category,
    Dependencies = new[] { "Core.Workflows" }
)]