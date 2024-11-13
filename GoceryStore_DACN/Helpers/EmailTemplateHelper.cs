namespace GoceryStore_DACN.Helpers
{
    public class EmailTemplateHelper
    {
        private static string LoadTemplate(string templateName)
        {
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(),
                "Templates", "EmailTemplates", $"{templateName}.html");
            return File.ReadAllText(templatePath);
        }
        public static string GetEmailTemplate(string templateName, Dictionary<string, string> replacements)
        {
            var emailTemplate = LoadTemplate(templateName);
            foreach (var replacement in replacements)
            {
                emailTemplate = emailTemplate.Replace($"{{{{ {replacement.Key} }}}}", replacement.Value);
            }
            return emailTemplate;
        }
    }
}
