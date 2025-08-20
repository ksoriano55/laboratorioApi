using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Laboratorios.Core.Entities;

namespace Laboratorios.Core.Entities
{
    public partial class User
    {
        //public User()
        //{
        //    UserCustomer = new HashSet<User_Customer>();
        //}
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string? userSAP { get; set; } = string.Empty;
        public string? passwordSAP { get; set; } = string.Empty;
        //public virtual ICollection<User_Customer> UserCustomer { get; set; }
    }
}
