using Microsoft.AspNetCore.Identity;
using SiliconSteppeDocuments.API.Common;
using SiliconSteppeDocuments.API.Contracts;
using SiliconSteppeDocuments.API.ViewModels;
using SiliconSteppeDocuments.DB;
using SiliconSteppeDocuments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task AddUserClaimsAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, RegisterViewModel viewModel)
        {
            if (user == null)
                return;

            var userClaims = new List<Claim>();
            if (!string.IsNullOrEmpty(viewModel.Description))
            {
                userClaims.Add(new Claim(JwtSteppeConstants.DESCRIPTION, viewModel.Description));
            }
            if (!string.IsNullOrEmpty(viewModel.FirstName))
            {
                userClaims.Add(new Claim(JwtSteppeConstants.FIRSTNAME, viewModel.FirstName));
            }
            if (!string.IsNullOrEmpty(viewModel.SecondName))
            {
                userClaims.Add(new Claim(JwtSteppeConstants.SECONDNAME, viewModel.SecondName));
            }
            if (!string.IsNullOrEmpty(viewModel.MiddleName))
            {
                userClaims.Add(new Claim(JwtSteppeConstants.MIDDLENAME, viewModel.MiddleName));
            }

            userClaims.Add(new Claim(JwtSteppeConstants.CANLOGIN, bool.TrueString));
            userClaims.Add(new Claim(JwtSteppeConstants.USER_TYPE, ((short)viewModel.Type).ToString()));

            await userManager.AddClaimsAsync(user, userClaims);
        }

        public async Task AddRoleToUserAsync(UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<UserData> GetUserInfoAsync(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            var userInfo = new UserData();

            var claims = await userManager.GetClaimsAsync(user);
            if (claims != null && claims.Any())
            {
                userInfo.FirstName = claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.FIRSTNAME)?.Value;
                userInfo.SecondName = claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.SECONDNAME)?.Value;
                userInfo.MiddleName = claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.MIDDLENAME)?.Value;
                userInfo.CanLogin = bool.Parse(claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.CANLOGIN)?.Value);
                userInfo.Description = claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.DESCRIPTION)?.Value;
                userInfo.Type = Convert.ToInt16(claims.FirstOrDefault(x => x.Type == JwtSteppeConstants.USER_TYPE)?.Value);
            }
            userInfo.Login = user.UserName;
            userInfo.Department = new Organization.DepartmentData(user.Department);

            return userInfo;
        }

        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            //var updatedUser = request.ID.HasValue ?
            //    await _context.UserInfos.FirstOrDefaultAsync(x => x.ID == request.ID) :
            //    new Model.UserInfo { };

            //if (updatedUser == null)
            //    throw new Exception("Не удалось найти сущность");

            //updatedUser = new Model.UserInfo
            //{
            //    ID = request.ID.HasValue ? request.ID.Value : 0,
            //    CanLogin = request.CanLogin,
            //    FirstName = request.FirstName,
            //    SecondName = request.SecondName,
            //    MiddleName = request.MiddleName,
            //    DepartmentID = request.DepartmentID,
            //    Description = request.Description,
            //    Type = (Model.UserType)request.Type,
            //    Token = Md5Helper.GetMd5Hash(request.Login),
            //    Login = request.Login,
            //    Password = Base64Helper.Base64Encode(request.Password)
            //};

            //if (request.ID > 0)
            //    _context.UserInfos.Update(updatedUser);
            //else
            //    await _context.UserInfos.AddAsync(updatedUser);

            //await _context.SaveChangesAsync();
            //return new UpdateUserResponse
            //{
            //    CanLogin = updatedUser.CanLogin,
            //    Department = updatedUser.Department,
            //    Description = updatedUser.Description,
            //    MiddleName = updatedUser.MiddleName,
            //    SecondName = updatedUser.SecondName,
            //    FirstName = updatedUser.FirstName,
            //    Login = updatedUser.Login,
            //    Token = updatedUser.Token,
            //    Type = (short)updatedUser.Type
            //};
            return null;
        }

        public static string GetIdentityUserRoleByUserType(UserType type)
        {
            switch (type)
            {
                case UserType.Consultant:
                    return "Consultant";
                case UserType.OrganizationEmployee:
                    return "Employee";
                default:
                    return string.Empty;
            }
        }
    }
}