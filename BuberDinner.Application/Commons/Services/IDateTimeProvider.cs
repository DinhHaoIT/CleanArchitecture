using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Commons.Services
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get;}
    }
}
