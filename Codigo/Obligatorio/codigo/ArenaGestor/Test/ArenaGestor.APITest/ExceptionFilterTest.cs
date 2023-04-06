using ArenaGestor.API.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class ExceptionFilterTest
    {
        private ExceptionFilter filter;
        private ExceptionContext exceptionContext;

        [TestInitialize]
        public void InitTest()
        {
            IList<IFilterMetadata> filters = new List<IFilterMetadata>();
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };
            exceptionContext = new ExceptionContext(actionContext, filters);
            filter = new ExceptionFilter();
        }

        [TestMethod]
        public void ArgumentException()
        {
            exceptionContext.Exception = new ArgumentException();
            filter.OnException(exceptionContext);
            ContentResult result = (ContentResult)exceptionContext.Result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [TestMethod]
        public void InvalidOperationException()
        {
            exceptionContext.Exception = new InvalidOperationException();
            filter.OnException(exceptionContext);
            ContentResult result = (ContentResult)exceptionContext.Result;
            Assert.AreEqual(StatusCodes.Status400BadRequest, result.StatusCode);

        }

        [TestMethod]
        public void NullReferenceException()
        {
            exceptionContext.Exception = new NullReferenceException();
            filter.OnException(exceptionContext);
            ContentResult result = (ContentResult)exceptionContext.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [TestMethod]
        public void InternalException()
        {
            exceptionContext.Exception = new Exception();
            filter.OnException(exceptionContext);
            ContentResult result = (ContentResult)exceptionContext.Result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }
    }
}
