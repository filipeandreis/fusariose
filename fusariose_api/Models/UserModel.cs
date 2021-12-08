using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fusariose_api.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
