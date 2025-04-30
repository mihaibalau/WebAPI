using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.IRepository;
using Data;
using Domain;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Repository
{
    /// <summary>
    /// Repository class for managing user-related database operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<List<User>> GetAllUsersAsync()
        {
            var userEntities = await dbContext.Users.ToListAsync();

            return userEntities.Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                Password = u.Password,
                Mail = u.Mail,
                Role = u.Role,
                Name = u.Name,
                BirthDate = u.BirthDate,
                CNP = u.CNP,
                Address = u.Address,
                PhoneNumber = u.PhoneNumber,
                RegistrationDate = u.RegistrationDate
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<User> GetUserByIdAsync(int id)
        {
            var userEntity = await dbContext.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            return new User
            {
                UserId = userEntity.UserId,
                Username = userEntity.Username,
                Password = userEntity.Password,
                Mail = userEntity.Mail,
                Role = userEntity.Role,
                Name = userEntity.Name,
                BirthDate = userEntity.BirthDate,
                CNP = userEntity.CNP,
                Address = userEntity.Address,
                PhoneNumber = userEntity.PhoneNumber,
                RegistrationDate = userEntity.RegistrationDate
            };
        }

        /// <inheritdoc/>
        public async Task AddUserAsync(User user)
        {
            var userEntity = new UserEntity
            {
                Username = user.Username,
                Password = user.Password,
                Mail = user.Mail,
                Role = user.Role,
                Name = user.Name,
                BirthDate = user.BirthDate,
                CNP = user.CNP,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                RegistrationDate = user.RegistrationDate
            };

            dbContext.Users.Add(userEntity);
            await dbContext.SaveChangesAsync();

            user.UserId = userEntity.UserId;
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            var userEntity = await dbContext.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            dbContext.Users.Remove(userEntity);
            await dbContext.SaveChangesAsync();
        }
    }
}
