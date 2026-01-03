
using System.ComponentModel.DataAnnotations;

namespace CodeZone.DAL;

public class NotEqualToZeroAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is int intValue && intValue == 0)
        {
            return false;
        }
        return true;
    }
}
