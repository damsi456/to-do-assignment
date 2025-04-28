using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Domain.Entities;

namespace TodoApi.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            return new UserDto {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Auth0Id = user.Auth0Id
            };
        }

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var user = await _repo.GetByEmailAsync(email);
            if (user == null) return null;
            return new UserDto {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Auth0Id = user.Auth0Id
            };
        }

        public async Task<UserDto?> CreateAsync(CreateUserDto dto)
        {
            var existingUser = await _repo.GetByAuth0IdAsync(dto.Auth0Id);

            if (existingUser != null)
            {
                return null;
            }
            
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Auth0Id = dto.Auth0Id
            };

            await _repo.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Auth0Id = user.Auth0Id
            };
        }

        public async Task<bool> UpdateAsync(int id,  CreateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.Auth0Id = dto.Auth0Id;
            await _repo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}