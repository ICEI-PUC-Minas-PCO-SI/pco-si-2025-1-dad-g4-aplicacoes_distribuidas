using System;
using System.Collections.Generic;

public class Notifications
{
    public string Recipient { get; set; }
    public List<string> CarbonCopyRecipients { get; set; }
    public List<string> BlindCarbonCopyRecipients { get; set; }
    public string Sender { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<Attachment> Attachments { get; set; }
    public string Status { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Priority { get; set; }
    public int Retries { get; set; }
    public string ErrorMessage { get; set; }

    public Notifications()
    {
        CarbonCopyRecipients = new List<string>();
        BlindCarbonCopyRecipients = new List<string>();
        Attachments = new List<Attachment>();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

public class Attachment
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}