using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using ClassLibrary.Domain;
using Entity;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.IRepository;

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
        public async Task<List<User>> getAllUsersAsync()
        {
            var userEntities = await dbContext.Users.ToListAsync();

            return userEntities.Select(u => new User
            {
                userId = u.userId,
                username = u.username,
                password = u.password,
                mail = u.mail,
                role = u.role,
                name = u.name,
                birthDate = u.birthDate,
                cnp = u.cnp,
                address = u.address,
                phoneNumber = u.phoneNumber,
                registrationDate = u.registrationDate
            }).ToList();
        }

        /// <inheritdoc/>
        public async Task<User> getUserByIdAsync(int id)
        {
            var userEntity = await dbContext.Users.FindAsync(id);

            if (userEntity == null)
            {
                throw new Exception($"User with ID {id} not found.");
            }

            return new User
            {
                userId = userEntity.userId,
                username = userEntity.username,
                password = userEntity.password,
                mail = userEntity.mail,
                role = userEntity.role,
                name = userEntity.name,
                birthDate = userEntity.birthDate,
                cnp = userEntity.cnp,
                address = userEntity.address,
                phoneNumber = userEntity.phoneNumber,
                registrationDate = userEntity.registrationDate
            };
        }

        /// <inheritdoc/>
        public async Task addUserAsync(User user)
        {
            var userEntity = new UserEntity
            {
                username = user.username,
                password = user.password,
                mail = user.mail,
                role = user.role,
                name = user.name,
                birthDate = user.birthDate,
                cnp = user.cnp,
                address = user.address,
                phoneNumber = user.phoneNumber,
                registrationDate = user.registrationDate
            };

            dbContext.Users.Add(userEntity);
            await dbContext.SaveChangesAsync();

            user.userId = userEntity.userId;
        }

        /// <inheritdoc/>
        public async Task updateUserAsync(User user)
        {
            var userEntity = await dbContext.Users.FindAsync(user.userId);

            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {user.userId} not found.");
            }

            userEntity.name = user.name;
            userEntity.password = user.password;
            userEntity.address = user.address;
            userEntity.phoneNumber = user.phoneNumber;
            userEntity.birthDate = user.birthDate;

            await dbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task deleteUserAsync(int id)
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
