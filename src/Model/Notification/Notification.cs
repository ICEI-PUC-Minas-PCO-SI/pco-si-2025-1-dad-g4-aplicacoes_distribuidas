using System;
using System.Collections.Generic;

public class Notification
{
    public int Id { get; set; }
    public string Recipient { get; set; }
    public string Sender { get; set; }
    public string Body { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Priority { get; set; }
    public int Retries { get; set; }
    public string ErrorMessage { get; set; }
    public Notification()
    {

    }
}

//public class Attachment
//{
//    public string FileName { get; set; }
//    public string FilePath { get; set; }
//}