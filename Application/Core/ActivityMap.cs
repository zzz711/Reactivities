using Application.Activities.DTO;
using Domain;

namespace Application.Core;

public class ActivityMap
{
    public static void MapActivity(EditActivityDto activity, ref Activity outActivity)
    {

        outActivity.Id = activity.Id;
        outActivity.Title = activity.Title;
        outActivity.Date = activity.Date;
        outActivity.Description = activity.Description;
        outActivity.Category = activity.Category;
        outActivity.City = activity.City;
        outActivity.Venue = activity.Venue;
        outActivity.Latitude = activity.Latitude;
        outActivity.Longitude = activity.Longitude;
    }

    public static Activity MapActivityDTO(CreateActivityDTO activity)
    {
        return new Activity
        {
            Title = activity.Title,
            Date = activity.Date,
            Description = activity.Description,
            Category = activity.Category,
            City = activity.City,
            Venue = activity.Venue,
            Latitude = activity.Latitude,
            Longitude = activity.Longitude
        };
    }
}