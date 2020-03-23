using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CustomUserManagerRepository.Model
{
    public class LoginResult
    {
        public bool PasswordMatch { get; set; }
    }
}
