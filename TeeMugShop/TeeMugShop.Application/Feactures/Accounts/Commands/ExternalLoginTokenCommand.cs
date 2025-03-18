using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeeMugShop.Application.Feactures.Accounts.Commands
{
    /// <summary>
    /// Representa os dados recebidos do frontend após login social.
    /// </summary>
    public class ExternalLoginTokenCommand : IRequest<ExternalLoginTokenResult>
    {
        public string Provider { get; set; } = string.Empty; // "Google" ou "Facebook"
        public string Token { get; set; } = string.Empty; // Token enviado pelo frontend
    }
}
