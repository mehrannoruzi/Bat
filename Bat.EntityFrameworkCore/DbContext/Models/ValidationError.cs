namespace Bat.EntityFrameworkCore;

public class ValidationError
{
    public string Field { get; set; }
    public string Value { get; set; }
    public string ValidationSource { get; set; }
    public string ValidationMessage { get; set; }
}