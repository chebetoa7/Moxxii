using Moxxii.mobile.Models.Body;
using Moxxii.mobile.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services.Login
{
    public interface ILoginRepository
    {
        Task<Responsemoxxii> Login(loginModel item, string tabla);
    }
}
