using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace JwtService
{
    public static class JwtHelper
    {
        /// <summary>
        /// 向request添加访问用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static RequestUser RequestUser(this HttpRequest request)
        {
            var authorization = request.Headers["Authorization"].ToString();
            var auth = authorization.Split(" ")[1];
            var authArray = auth.Split(".");
            var bts = Convert.FromBase64String(authArray[1] + "==");
            var str = Encoding.UTF8.GetString(bts);
            //解析Claims
            var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
            var reqUser = new RequestUser
            {
                UserId = dic["id"],
                UserName = dic["name"],
            };
            return reqUser;
        }



    }
}
