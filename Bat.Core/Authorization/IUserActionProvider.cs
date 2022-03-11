namespace Bat.Core;

public interface IUserActionProvider
{
    IEnumerable<UserAction> GetUserActions(string userId, string urlPrefix = "");
    Task<IEnumerable<UserAction>> GetUserActionsAsync(string userId, string urlPrefix = "");
}