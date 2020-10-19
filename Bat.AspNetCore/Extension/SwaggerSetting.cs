namespace Bat.AspNetCore
{
    public class SwaggerContact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }

    public class SwaggerLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class SwaggerSetting
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public string JsonUrl { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public SwaggerContact Contact { get; set; }
        public SwaggerLicense License { get; set; }
    }
}