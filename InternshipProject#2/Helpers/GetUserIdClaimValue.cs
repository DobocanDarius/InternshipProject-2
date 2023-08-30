namespace InternshipProject_2.Helpers;

public class GetUserIdClaimValue
{
    public int? GetClaimValue(HttpContext httpContext)
    {
        var userIdClaimString = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("userId"));

        if (userIdClaimString?.Value != null)
        {
            if (int.TryParse(userIdClaimString.Value, out int userIdClaimInt))
            {
                return userIdClaimInt;
            }
            
        }

        return null;
    }
}
