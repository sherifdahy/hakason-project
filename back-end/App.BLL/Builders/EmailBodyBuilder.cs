using System;
using System.Collections.Generic;
using System.Text;

namespace App.BLL.Builders;

public static class EmailBodyBuilder
{
    public static async Task<string> GenerateEmailBody(string template, Dictionary<string, string> templateValues)
    {
        var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", $"{template}.html");

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Email template '{template}' not found.", templatePath);

        var body = await File.ReadAllTextAsync(templatePath);

        foreach (var item in templateValues)
        {
            body = body.Replace(item.Key, item.Value);
        }

        return body;
    }
}
