using Grpc.Core;
using grpcserver.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace grpcserver
{
    public class UserService : User.UserBase
    {
        private readonly ILogger<UserService> _logger;
        private readonly DataContext _context;
        public UserService(ILogger<UserService> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override async Task<UsersList> GetUsers(Empty request, ServerCallContext context)
        {
            UsersList usersList = new UsersList();
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                usersList.Users.Add(user);
            }

            return usersList;

        }

        public override async Task<Response> PostUser(UserModel request, ServerCallContext context)
        {
            UserModel searchedUser = await _context.Users.FirstOrDefaultAsync(x => x.DocumentId == request.DocumentId);

            if ((request.Age != "" || request.Name != "" || request.DocumentId != "") && searchedUser == null)
            {
                UserModel user = new UserModel { DocumentId = request.DocumentId, Age = request.Age, Name = request.Name };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return await Task.FromResult(new Response
                {
                   
                    IsSuccess = true,
                    Message = "User submited.",
                    Results = user
                }); ;
            }


            return await Task.FromResult(
                new Response
                {
                IsSuccess = false,
                Message = "There was a problem: There is missing data or the user with that documentId is already created."
                });
        }

        public override async Task<UserModel> GetUser(UserDocument request, ServerCallContext context)
        {
            UserModel searchedUser = await _context.Users.FirstOrDefaultAsync(x => x.DocumentId == request.Document);
            if (searchedUser != null)
            {
                return new UserModel { DocumentId = searchedUser.DocumentId, Age = searchedUser.Age, Name = searchedUser.Name };
            }

            throw new  Exception("Error");
        }

        public override async Task<Response> DeleteUser(UserDocument request, ServerCallContext context)
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.DocumentId == request.Document); 
            
            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = $"User with id:{request.Document} was successfully deleted."
                };
            }

            return new Response
            {
                IsSuccess = false,
                Message = $"User with id:{request.Document} not found in database."
            };

        }

        public override async Task<Response> EditUser(UserModel request, ServerCallContext context)
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(u => u.DocumentId == request.DocumentId);

            if (user != null)
            {
                user.Name = request.Name;
                user.Age = request.Age;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = $"User with id:{request.DocumentId} was successfully updated.",
                    Results = user
                };
            }

            return new Response
            {
                IsSuccess = false,
                Message = $"User with id:{request.DocumentId} not found in database."
            };

        }


    }

}
