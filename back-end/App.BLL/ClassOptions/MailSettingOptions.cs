using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.ClassOptions;

public class MailSettings
{
    [Required]
    public string Mail { get; set; } = string.Empty; 
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string Host { get; set; } = string.Empty;
    [Required]
    public int Port { get; set; }
}
