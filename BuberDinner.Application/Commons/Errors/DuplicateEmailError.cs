using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Commons.Errors
{
    public record struct DuplicateEmailError() : IError
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string Message => "Email is exists already";
    }
}
