using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Contract.Authentication
{
    public record LoginRequestContract(
        string Email,
        string Password
    );
    
}
