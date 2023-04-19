using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Moxxii.mobile.Services.adminApi
{
    public class moxiApiFactory
    {
        private readonly IMoxiApiAuth _moOauth;

        public moxiApiFactory(IMoxiApiAuth moOauth)
        {
            _moOauth = moOauth;
        }

    }
}
