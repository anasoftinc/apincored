using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;
using System.Text;

namespace ApiNCoreDApplication1.Entity
{
    /// <summary>
    /// A user attached to an account
    /// </summary>
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsAdminRole { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }   //stored encrypted
        [Computed]
        public string DecryptedPassword
        {
            get { return Decrypt(Password); }
            set { Password = Encrypt(value); }
        }
        public int AccountId { get; set; }


        public virtual Account Account { get; set; }

        private string Decrypt(string cipherText)
        {
            return EntityHelper.Decrypt(cipherText);
        }
        private string Encrypt(string clearText)
        {
            return EntityHelper.Encrypt(clearText);
        }
    }
}
