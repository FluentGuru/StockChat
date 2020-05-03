using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Exceptions;

namespace Jobsity.StockChat.Application.Handlers
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTime dateTime;
        private readonly IHasher hasher;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IHasher hasher)
        {
            this.unitOfWork = unitOfWork;
            this.dateTime = dateTime;
            this.hasher = hasher;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await unitOfWork.GetSingleAsync<UserEntity>(u => u.Nickname == request.Nickname);
                if (user == null)
                {
                    user = new UserEntity() { Nickname = request.Nickname, CreatedDate = dateTime.Now, LastLoginDate = DateTime.Now };
                    user.PasswordSalt = hasher.GetSalt(UserAuthConstants.PasswordSaltLength);
                    user.PasswordHash = hasher.Generate(request.Password, user.PasswordSalt);
                    await unitOfWork.AddAndSaveAsync(user);
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
