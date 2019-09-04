﻿using RsvpYes.Domain.Users;

namespace RsvpYes.Domain.Messaging
{
    public interface IMessage
    {
        MessageId Id { get; }
        User From { get; }
        User To { get; }
        string MessageBody { get; }
    }
}
