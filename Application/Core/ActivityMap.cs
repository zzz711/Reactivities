using Domain;

namespace Application.Core;

public class ActivityMap
{
    public static Activity MapActivity(Activity activity)
    {
        return new Activity
        {
            Id = activity.Id,
            Title = activity.Title,
            Date = activity.Date,
            Description = activity.Description,
            Category = activity.Category,
            IsCancelled = activity.IsCancelled,
            City = activity.City,
            Venue = activity.Venue,
            Latitude = activity.Latitude,
            Longitude = activity.Longitude
        };
    }
}