using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Constants;
using Jobsity.StockChat.Application.Data;
using Jobsity.StockChat.Application.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly StockChatDbContext dbContext;
        private readonly IDateTime dateTime;
        private readonly IHasher hasher;

        public CreateUserCommandHandler(StockChatDbContext dbContext, IDateTime dateTime, IHasher hasher)
        {
            this.dbContext = dbContext;
            this.dateTime = dateTime;
            this.hasher = hasher;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Nickname == request.Nickname, cancellationToken);
                if (user == null)
                {
                    user = new UserEntity() { Nickname = request.Nickname, CreatedDate = dateTime.Now, LastLoginDate = DateTime.Now };
                    user.PasswordSalt = hasher.GetSalt(UserAuthConstants.PasswordSaltLength);
                    user.PasswordHash = hasher.Generate(request.Password, user.PasswordSalt);
                    await dbContext.AddAsync(user, cancellationToken);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return user;
                }

                throw new CreatingExistingUserException($"'{request.Nickname}' already exists");
            }
            catch(Exception ex)
            {
                throw new UserCreationException($"Error creating user '{request.Nickname}'", ex);
            }
        }
    }
}
