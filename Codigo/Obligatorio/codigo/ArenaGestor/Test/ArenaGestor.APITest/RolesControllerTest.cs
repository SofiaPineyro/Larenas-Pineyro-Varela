using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Roles;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ArenaGestor.APITest
{
    [TestClass]
    public class RolesControllerTest
    {
        private Mock<IRolesService> mock;
        private Mock<IMapper> mockMapper;
        private RolesController api;
        private IEnumerable<Role> rolesUserOk;
        private IEnumerable<RoleArtist> rolesArtistOk;
        private IEnumerable<RolesResultDto> resultRolesDto;
        private IEnumerable<RolesArtistResultDto> rolesArtistResultDto;

        [TestInitialize]
        public void InitTest()
        {
            mock = new Mock<IRolesService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            api = new RolesController(mock.Object, mockMapper.Object);
            rolesUserOk = new List<Role>() { new Role() { RoleId = RoleCode.Administrador, Name = RoleCode.Administrador.ToString() } };
            rolesArtistOk = new List<RoleArtist>() { new RoleArtist() { RoleArtistId = RoleArtistCode.Cantante, Name = RoleArtistCode.Cantante.ToString() } };

            resultRolesDto = new List<RolesResultDto>()
            {
                new RolesResultDto()
                    {
                        RoleId = 1,
                        Name = RoleCode.Administrador.ToString()
                    }
            };

            rolesArtistResultDto = new List<RolesArtistResultDto>()
            {
                new RolesArtistResultDto()
                    {
                        RoleArtistId = 1,
                        Name = RoleArtistCode.Cantante.ToString()
                    }
            };
        }

        [TestMethod]
        public void GetUserRolesOkTest()
        {
            mock.Setup(x => x.GetUserRoles()).Returns(rolesUserOk);
            mockMapper.Setup(x => x.Map<IEnumerable<RolesResultDto>>(rolesUserOk)).Returns(resultRolesDto);

            var result = api.GetUserRoles();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }

        [TestMethod]
        public void GetArtistRolesOkTest()
        {
            mock.Setup(x => x.GetArtistRoles()).Returns(rolesArtistOk);
            mockMapper.Setup(x => x.Map<IEnumerable<RolesArtistResultDto>>(rolesArtistOk)).Returns(rolesArtistResultDto);

            var result = api.GetArtistRoles();
            var objectResult = result as ObjectResult;
            var statusCode = objectResult.StatusCode;

            mock.VerifyAll();
            Assert.AreEqual(StatusCodes.Status200OK, statusCode);
        }
    }
}
