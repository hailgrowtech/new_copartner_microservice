using System;

namespace ContactService.Dto; // Updated namespace

public class ContactUsFormDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string FormId { get; set; } // This can be used to identify which form was filled out
}
