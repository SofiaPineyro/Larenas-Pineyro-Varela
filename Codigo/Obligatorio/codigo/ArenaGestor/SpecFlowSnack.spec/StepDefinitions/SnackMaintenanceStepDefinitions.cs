using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccess;
using ArenaGestor.Domain;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace SpecFlowSnack.spec.StepDefinitions
{
    [Binding]
    public class SnackMaintenanceStepDefinitions
    {
        private User user;
        private readonly Snack _snackOnDTB;
        private readonly Snack _snackNotOnDB;

        private Mock<ISnacksService> mock;
        private Mock<IMapper> mockMapper;
        private SnacksController snackController;

        private DbContext context;
        private SnacksManagement management;

        private int? _responseCode;

        public SnackMaintenanceStepDefinitions()
        {
            mock = new Mock<ISnacksService>(MockBehavior.Strict);
            mockMapper = new Mock<IMapper>(MockBehavior.Strict);

            user = new User();

            snackController = new SnacksController(mock.Object, mockMapper.Object);

            _snackOnDTB = new Snack()
            {
                Name = "chips",
                Description = "estas son papas muy caras",
                Price = 500
            };
            _snackNotOnDB = new Snack()
            {
                Name = "chips",
                Description = "estas son papas muy caras",
                Price = 500
            };

            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ArenaGestorContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            context = new ArenaGestorContext(options);
            
            management = new SnacksManagement(context);
        }

        [Given(@"I am an administrator")]
        public void GivenIAmAnAdministrator()
        {
            user = new User()
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

        [When(@"I add a new snack with name ""([^""]*)"", description ""([^""]*)"", and price (.*)")]
        public void WhenIAddANewSnackWithNameDescriptionAndPrice(string chips, string p1, int p2)
        {
            var insertSnackDto = new InsertSnackDto()
            {
                Name = chips,
                Description = p1,
                Price = p2
            };
            var resultSnackDto = new SnackResultSnackDto()
            {
                Id = 1,
                Name = chips,
                Description = p1,
                Price = p2
            };
            var snack = new Snack()
            {
                Id = 1,
                Name = chips,
                Description = p1,
                Price = p2
            };

            mock.Setup(x => x.InsertSnack(snack)).Returns(snack);
            mockMapper.Setup(x => x.Map<Snack>(insertSnackDto)).Returns(snack);
            mockMapper.Setup(x => x.Map<SnackResultSnackDto>(snack)).Returns(resultSnackDto);

            var result = snackController.PostSnack(insertSnackDto);
            var objectResult = result as ObjectResult;
            _responseCode = objectResult.StatusCode;
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int p0)
        {
            _responseCode.Should().Be(p0);
        }

        [Then(@"the snack with name ""([^""]*)"" should be added to the snacks table")]
        public void ThenTheSnackWithNameShouldBeAddedToTheSnacksTable(string chips)
        {
            var exists = management.Exists(x => x.Name == chips);
            exists.Should().BeTrue();
        }

        [Given(@"there is already a snack with name ""([^""]*)"" in the snacks table")]
        public void GivenThereIsAlreadyASnackWithNameInTheSnacksTable(string chips)
        {
            var exists = management.Exists(x => x.Name == chips);
            if (!exists)
            {
                context.Set<Snack>().Add(_snackOnDTB);
                context.SaveChanges();
            }
        }

        [Given(@"there is a snack with ID (.*) in the snacks table")]
        public void GivenThereIsASnackWithIDInTheSnacksTable(int p0)
        {
            var exists = management.Exists(x => x.Id == p0);
            if(!exists)
            {
                context.Set<Snack>().Add(_snackOnDTB);
                context.SaveChanges();
            }
        }

        [When(@"I delete the snack with ID (.*)")]
        public void WhenIDeleteTheSnackWithID(int p0)
        {
            mock.Setup(x => x.DeleteSnack(p0));

            var result = snackController.DeleteSnack(p0);
            var objectResult = result as ObjectResult;
            _responseCode = objectResult.StatusCode;
        }

        [Then(@"the snack with ID (.*) should no longer appear in the snacks table")]
        public void ThenTheSnackWithIDShouldNoLongerAppearInTheSnacksTable(int p0)
        {
            var exists = management.Exists(x => x.Id == p0);
            exists.Should().BeFalse();
        }

        [Given(@"there is no snack with ID (.*) in the snacks table")]
        public void GivenThereIsNoSnackWithIDInTheSnacksTable(int p0)
        {
            _snackNotOnDB.Id = p0;
            var exists = management.Exists(x => x.Id == p0);
            if (exists)
            {
                context.Set<Snack>().Remove(_snackNotOnDB);
                context.SaveChanges();
            }
        }
    }
}
