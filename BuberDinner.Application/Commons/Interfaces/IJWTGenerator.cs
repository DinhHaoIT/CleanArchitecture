using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Commons.Interfaces
{
    public interface IJWTGenerator
    {
        string GenerateToken(Guid Uid,string FirstName, string LastName);
    }
}
