﻿using Moxxii.mobile.Models.Body;
using Moxxii.mobile.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Helper.Login
{
    public partial class HelperLogin
    {
        public async Task<bool> Login(string _usuario, string _password)
        {
            try
            {

                var lodata = new loginModel
                {
                    usuario = _usuario,
                    password = _password
                };

                var apiManager = RestService.For<IMoxxiiApi>(MoxxiiApi.BaseUrl);
                var tokent1 = apiManager.GetTokenUser(lodata);
                var result = tokent1.Result;

                if (result.success == true)
                {

                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
