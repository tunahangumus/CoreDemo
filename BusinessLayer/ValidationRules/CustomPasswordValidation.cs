using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CustomPasswordValidation : IdentityErrorDescriber
    {
        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = "PasswordMismatch", Description = "Şu anki şifre doğru değil" };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = "Şifre en az 6 karakter olmalı" };
        }
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = "PasswordRequiresDigit", Description = "Şifrede en az 1 tane sayı olmalı" };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError { Code = "PasswordRequiresUpper", Description = "Şifrede en az bir büyük harf olmalı" };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError { Code = "PasswordRequiresLıwer", Description = "Şifrede en az bir küçük harf olmalı" };
        }
    }
}
