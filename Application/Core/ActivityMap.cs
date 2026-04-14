using Domain;

namespace Application.Core;

public class ActivityMap
{
    public static void MapActivity(Activity activity, ref Activity outActivity)
    {

        outActivity.Id = activity.Id;
        outActivity.Title = activity.Title;
        outActivity.Date = activity.Date;
        outActivity.Description = activity.Description;
        outActivity.Category = activity.Category;
        outActivity.IsCancelled = activity.IsCancelled;
        outActivity.City = activity.City;
        outActivity.Venue = activity.Venue;
        outActivity.Latitude = activity.Latitude;
        outActivity.Longitude = activity.Longitude;
    }
}