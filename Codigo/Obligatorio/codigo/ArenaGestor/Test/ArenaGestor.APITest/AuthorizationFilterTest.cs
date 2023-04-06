using ArenaGestor.API.Filters;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class AuthorizationFilterTest
    {
        private AuthorizationFilter filter;
        private Mock<HttpContext> httpContextMock;
        private Mock<ISecurityService> securityServiceMock;

        private User nullUser;
        private User userOk;

        [TestInitialize]
        public void InitTest()
        {
            httpContextMock = new Mock<HttpContext>(MockBehavior.Strict);
            securityServiceMock = new Mock<ISecurityService>(MockBehavior.Strict);
            filter = new AuthorizationFilter(RoleCode.Administrador, RoleCode.Vendedor);
            nullUser = null;

            userOk = new User()
            {
                UserId = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>() {
                    new UserRole()
                    {
                        RoleId = RoleCode.Administrador
                    }
                }
            };
        }

        private AuthorizationFilterContext GetFilterContext(HttpContext httpContext)
        {
            ActionContext actionContext = new ActionContext(httpContext,
                new Microsoft.AspNetCore.Routing.RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor() { DisplayName = "token" },
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary());

            return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
        }

        [TestMethod]
        public void OnAuthorizationOk()
        {
            httpContextMock.Setup(x => x.Request.Headers["token"]).Returns("123456");
            httpContextMock.Setup(x => x.RequestServices.GetService(typeof(ISecurityService))).Returns(securityServiceMock.Object);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(true);
            var AuthFilterContext = GetFilterContext(httpContextMock.Object);
            filter.OnAuthorization(AuthFilterContext);

            securityServiceMock.VerifyAll();
            httpContextMock.VerifyAll();
        }

        [TestMethod]
        public void TokenEmpty()
        {
            httpContextMock.Setup(x => x.Request.Headers["token"]).Returns("");
            httpContextMock.Setup(x => x.RequestServices.GetService(typeof(ISecurityService))).Returns(securityServiceMock.Object);
            var AuthFilterContext = GetFilterContext(httpContextMock.Object);
            filter.OnAuthorization(AuthFilterContext);

            Assert.AreEqual(StatusCodes.Status401Unauthorized, (AuthFilterContext.Result as ContentResult).StatusCode);
        }

        [TestMethod]
        public void TokenInvalid()
        {
            httpContextMock.Setup(x => x.Request.Headers["token"]).Returns("123456");
            httpContextMock.Setup(x => x.RequestServices.GetService(typeof(ISecurityService))).Returns(securityServiceMock.Object);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(nullUser);
            var AuthFilterContext = GetFilterContext(httpContextMock.Object);
            filter.OnAuthorization(AuthFilterContext);

            Assert.AreEqual(StatusCodes.Status403Forbidden, (AuthFilterContext.Result as ContentResult).StatusCode);
        }

        [TestMethod]
        public void TokenThrowsException()
        {
            httpContextMock.Setup(x => x.Request.Headers["token"]).Returns("123456");
            httpContextMock.Setup(x => x.RequestServices.GetService(typeof(ISecurityService))).Returns(securityServiceMock.Object);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Throws(new Exception());
            var AuthFilterContext = GetFilterContext(httpContextMock.Object);
            filter.OnAuthorization(AuthFilterContext);

            Assert.AreEqual(StatusCodes.Status403Forbidden, (AuthFilterContext.Result as ContentResult).StatusCode);
        }

        [TestMethod]
        public void UserNoPermissions()
        {
            httpContextMock.Setup(x => x.Request.Headers["token"]).Returns("123456");
            httpContextMock.Setup(x => x.RequestServices.GetService(typeof(ISecurityService))).Returns(securityServiceMock.Object);
            securityServiceMock.Setup(x => x.GetUserOfToken(It.IsAny<string>())).Returns(userOk);
            securityServiceMock.Setup(x => x.UserHaveRole(It.IsAny<RoleCode>(), It.IsAny<string>())).Returns(false);
            var AuthFilterContext = GetFilterContext(httpContextMock.Object);
            filter.OnAuthorization(AuthFilterContext);

            Assert.AreEqual(StatusCodes.Status403Forbidden, (AuthFilterContext.Result as ContentResult).StatusCode);
        }

    }
}
