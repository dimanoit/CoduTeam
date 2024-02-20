namespace CoduTeam.Application.ChatFeature.Filters;

public static class ChatFilter
{
    public static IQueryable<Domain.Entities.Chat> AddOnlyRelatedToCurrentUserFilter(
        this IQueryable<Domain.Entities.Chat> dbQuery,
        bool onlyRelatedToCurrentUser,
        int currentUserId)
    {
        return onlyRelatedToCurrentUser
            ? dbQuery.Where(p => p.UserChats.Select(u => u.UserId).Contains(currentUserId))
            : dbQuery;
    }

    public static IQueryable<Domain.Entities.Chat> AddChatIdFilter(
        this IQueryable<Domain.Entities.Chat> dbQuery,
        int? chatId)
    {
        return chatId.HasValue
            ? dbQuery.Where(p => p.Id == chatId.Value)
            : dbQuery;
    }
}
