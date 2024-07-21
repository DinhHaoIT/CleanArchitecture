using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Contract.Authentication
{
    public record AutResultContract(
        Guid Id,
        string FirstName,
        string LastName,    
        string Email,
        string Token
    );
}
