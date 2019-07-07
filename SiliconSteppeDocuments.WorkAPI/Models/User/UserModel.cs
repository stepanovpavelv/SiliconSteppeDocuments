using Microsoft.EntityFrameworkCore;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.Common.Helpers;
using SiliconSteppeDocuments.DB;
using System;
using System.Threading.Tasks;

namespace SiliconSteppeDocuments.API.Models.User
{
    public class UserModel
    {
        private readonly SteppeContext _context;

        public UserModel(SteppeContext context)
        {
            _context = context;
        }

        public async Task<AddUserResponse> AddUser(AddUserRequest request)
        {
            var newUser = new Model.User
            {
               CanLogin = true,
               Login = request.Login,
               Password = Base64Helper.Base64Encode(request.Password),
               Token = Md5Helper.GetMd5Hash(request.Login),
               Type = (Model.UserType)request.Type
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new AddUserResponse
            {
                CanLogin = true,
                Token = newUser.Token,
                Login = newUser.Login,
                Type = (short)newUser.Type
            };
        }

        public async Task<AuthorizeUserResponse> GetUser(AuthorizeUserRequest request)
        {
            var response = new AuthorizeUserResponse();

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Login == request.Login);
            if (existingUser == null)
                response.AddMessage(Common.MessageType.Error, "Пользователь с таким логином не найден");

            if (!existingUser.CanLogin)
                response.AddMessage(Common.MessageType.Error, "Данному пользователю запрещено входить в систему");

            if (Base64Helper.Base64Encode(existingUser.Password) != request.Password)
                response.AddMessage(Common.MessageType.Error, "Введен неправильный пароль пользователя! Повторите попытку");

            return response = new AuthorizeUserResponse
            {
                CanLogin = existingUser.CanLogin,
                Department = existingUser.Department,
                Description = existingUser.Description,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                SecondName = existingUser.SecondName,
                Type = (short)existingUser.Type,
                Login = existingUser.Login,
                Token = existingUser.Token
            };
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            var updatedUser = request.ID.HasValue ?
                await _context.Users.FirstOrDefaultAsync(x => x.ID == request.ID) :
                new Model.User { };

            if (updatedUser == null)
                throw new Exception("Не удалось найти сущность");

            updatedUser = new Model.User
            {
                ID = request.ID.HasValue ? request.ID.Value : 0,
                CanLogin = request.CanLogin,
                FirstName = request.FirstName,
                SecondName = request.SecondName,
                MiddleName = request.MiddleName,
                DepartmentID = request.DepartmentID,
                Description = request.Description,
                Type = (Model.UserType)request.Type,
                Token = Md5Helper.GetMd5Hash(request.Login),
                Login = request.Login,
                Password = Base64Helper.Base64Encode(request.Password)
            };

            if (request.ID > 0)
                _context.Users.Update(updatedUser);
            else
                await _context.Users.AddAsync(updatedUser);

            await _context.SaveChangesAsync();
            return new UpdateUserResponse
            {
                CanLogin = updatedUser.CanLogin,
                Department = updatedUser.Department,
                Description = updatedUser.Description,
                MiddleName = updatedUser.MiddleName,
                SecondName = updatedUser.SecondName,
                FirstName = updatedUser.FirstName,
                Login = updatedUser.Login,
                Token = updatedUser.Token,
                Type = (short)updatedUser.Type
            };
        }
    }
}
