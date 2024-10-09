using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sicf_BusinessHandlers.BusinessHandlers.Filter;

namespace sicf_BusinessHandlers.Filters
{
    public class MyAttribute : TypeFilterAttribute
    {
        public MyAttribute(params string[] ids) : base(typeof(MyAttributeImpl))
        {
            Arguments = new object[] { ids };
        }

        private class MyAttributeImpl : IActionFilter
        {
            private readonly string[] _ids;
            private IFilterService filterService;

            public MyAttributeImpl( IFilterService filterService, string[] ids)
            {
                _ids = ids;
                this.filterService = filterService;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
              
                var token = Context.GetToken(context.HttpContext);
                var filtro = filterService.ValidarPermiso(token.usuario , _ids.First());
                var salida = filtro.Result;

                if (!salida) {


                    context.Result = new BadRequestObjectResult("Token invalido");
                    return;
                }
              
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
