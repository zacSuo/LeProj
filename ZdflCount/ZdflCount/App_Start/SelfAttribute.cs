﻿using System;
using System.Web.Mvc; 

namespace ZdflCount.App_Start
{
    /// <summary>
    /// 用户登录验证
    /// </summary>
    public class UserLoginAuthenticationAttribute : ActionFilterAttribute    
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Account/Login");
            }
        }  
    }
}