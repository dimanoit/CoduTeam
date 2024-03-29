﻿using CoduTeam.Application.Chat.Mappers;
using CoduTeam.Application.ChatFeature.Commands.Common;
using CoduTeam.Application.Common.Interfaces;
using CoduTeam.Domain.Entities;
using CoduTeam.Domain.Enums;

namespace CoduTeam.Application.ChatFeature.Commands.CreateChatCommand;

public record CreateChatCommand(
    ChatType ChatType,
    string Title,
    int[] ParticipantsIds) : BaseChatModifyCommand(ChatType, Title), IRequest
{
}

public class CreateChatCommandHandler(IUser user, IApplicationDbContext dbContext) : IRequestHandler<CreateChatCommand>
{
    public async Task Handle(CreateChatCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(user.Id);
        Guard.Against.Null(command.ParticipantsIds);

        Domain.Entities.Chat chat = command.ToChat();
        dbContext.Chats.Add(chat);

        UserChat userChat = new() { UserId = user.Id.Value, Chat = chat };
        List<UserChat> userChats =
            command.ParticipantsIds.Select(i => new UserChat { UserId = i, Chat = chat }).ToList();

        userChats.Add(userChat);
        dbContext.UserChats.AddRange(userChats);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
