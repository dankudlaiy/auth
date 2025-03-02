using auth.Domain.Diagram;

namespace auth.Services;

public class DbService
{
    public static async Task<List<TelephonyAction>> GetActions()
    {
        // simulate available actions retrieval
        return new List<TelephonyAction> { new() { Name = "Action 1"}, new() { Name = "Action 2"}, new() { Name = "Action 3"}};
    }
}