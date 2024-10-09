using AutoMapper.Execution;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        //public string parameter { get; set; }

        AuthenticationFilter(object someValue) { 
        
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
          
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

           //var dum= context.ActionArguments.TryGetValue(parameter, out var value);


            var ser = "nacho";
        }
    }
}
