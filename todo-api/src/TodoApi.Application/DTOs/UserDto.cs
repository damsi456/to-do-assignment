using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Application.DTOs
{
    // For response
    public class UserDto
    {

        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
    public class CreateUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}