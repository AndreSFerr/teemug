using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeMugShop.Application.Feactures.Accounts.Commands
{
    public class ExternalLoginTokenResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; } 
        public Guid UserId { get; set; }
        public string? Email { get; set; }
    }
}
