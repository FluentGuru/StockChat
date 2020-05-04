using Jobsity.StockChat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Commons
{
    public static class EntityFactory
    {
        public static UserEntity GetUser(string nickname, string password)
        {
            var hasher = InfrastructureFactory.GetSha1Hasher();
            var dateTime = InfrastructureFactory.GetMachineDateTime();
            return new UserEntity()
            {
                Nickname = nickname,
                PasswordSalt = "Salt",
                PasswordHash = hasher.Generate(password, "Salt"),
                CreatedDate = dateTime.Now,
                LastLoginDate = dateTime.Now
            };
        }

        public static ChatEntity GetChat(string stock, string owner)
        {
            return new ChatEntity()
            {
                OwnerNickname = owner,
                Stock = stock,
                CreateDate = InfrastructureFactory.GetMachineDateTime().Now
            };
        }

        public static UserTokenEntity GetToken(string token, string nickname, DateTime expirationDate)
        {
            return new UserTokenEntity()
            {
                Token = token,
                Nickname = nickname,
                CreatedDate = expirationDate,
                ExpirationDate = expirationDate
            };
        }

        public static ChatMessageEntity GetChatMessage(string stock, string nickname, string message, DateTime sentTime)
        {
            return new ChatMessageEntity()
            {
                Stock = stock,
                FromNickName = nickname,
                Message = message,
                SentTime = sentTime
            };
        }

        public static ChatParticipantEntity GetParticipant(string stock, string nickname)
        {
            return new ChatParticipantEntity()
            {
                Stock = stock,
                Nickname = nickname
            };
        }
    }
}
