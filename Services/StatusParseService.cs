using GesHomeLibrary.Models;

namespace GesHomeLibrary.Services;

public class StatusParseService
{
    public StatusesList StatusParseByName(string statusName)
    {
        return statusName switch
        {
            "In Stock" => StatusesList.InStock,
            "Read" => StatusesList.Read,
            "Given away" => StatusesList.GivenAway,
            "Being read"=> StatusesList.BeingRead,
            _ => throw new ArgumentException($"Status {statusName} not found")
        };
    }
}