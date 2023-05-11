using ArenaGestor.API;
using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccess;
using ArenaGestor.DataAccess.Managements;
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

        private SnacksService snacksService;
        private SnacksController snackController;

        private DbContext context;
        private SnacksManagement management;

        private Exception exception;

        private int? _responseCode;

        public SnackMaintenanceStepDefinitions()
        {
            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ArenaGestorContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            context = new ArenaGestorContext(options);

            management = new SnacksManagement(context);

            snacksService = new SnacksService(management);

            user = new User();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ArenaGestorAutoMapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            snackController = new SnacksController(snacksService, mapper);

            _snackOnDTB = new Snack()
            {
                Name = "Hot dog",
                Description = "estas son papas muy caras",
                Price = 500
            };
            _snackNotOnDB = new Snack()
            {
                Name = "chips",
                Description = "estas son papas muy caras",
                Price = 500
            };

            
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

            try
            {
                var result = snackController.PostSnack(insertSnackDto);
                var objectResult = result as OkObjectResult;
                _responseCode = objectResult.StatusCode;
            }catch(Exception ex)
            {
                exception = ex;
            }
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int p0)
        {
            _responseCode.Should().Be(p0);
        }

        [Then(@"the snack with name ""([^""]*)"" should be added to the snacks table")]
        public void ThenTheSnackWithNameShouldBeAddedToTheSnacksTable(string chips)
        {
            var exists = management.ExistsSnack(x => x.Name == chips);
            exists.Should().BeTrue();
        }

        [Given(@"there is already a snack with name ""([^""]*)"" in the snacks table")]
        public void GivenThereIsAlreadyASnackWithNameInTheSnacksTable(string chips)
        {
            _snackOnDTB.Name = chips;
            var exists = management.ExistsSnack(x => x.Name == chips);
            if (!exists)
            {
                context.Set<Snack>().Add(_snackOnDTB);
                context.SaveChanges();
            }
        }

        [Given(@"there is a snack with ID (.*) in the snacks table")]
        public void GivenThereIsASnackWithIDInTheSnacksTable(int p0)
        {
            var exists = management.ExistsSnack(x => x.Id == p0);
            if(!exists)
            {
                context.Set<Snack>().Add(_snackOnDTB);
                context.SaveChanges();
            }
        }

        [When(@"I delete the snack with ID (.*)")]
        public void WhenIDeleteTheSnackWithID(int p0)
        {
            try
            {
                var result = snackController.DeleteSnack(p0);
                var objectResult = result as OkResult;
                _responseCode = objectResult.StatusCode;
            }catch (Exception ex)
            {
                exception = ex;
            }
        }

        [Then(@"the snack with ID (.*) should no longer appear in the snacks table")]
        public void ThenTheSnackWithIDShouldNoLongerAppearInTheSnacksTable(int p0)
        {
            var exists = management.ExistsSnack(x => x.Id == p0);
            exists.Should().BeFalse();
        }

        [Given(@"there is no snack with ID (.*) in the snacks table")]
        public void GivenThereIsNoSnackWithIDInTheSnacksTable(int p0)
        {
            _snackNotOnDB.Id = p0;
            var exists = management.ExistsSnack(x => x.Id == p0);
            if (exists)
            {
                context.Set<Snack>().Remove(_snackNotOnDB);
                context.SaveChanges();
            }
        }

        [Then(@"ArgumentException is thrown")]
        public void ThenArgumentExceptionIsThrown()
        {
            exception.Should().BeOfType<ArgumentException>();
        }

        [Then(@"NullReferenceException is thrown")]
        public void ThenNullReferenceExceptionIsThrown()
        {
            exception.Should().BeOfType<NullReferenceException>();
        }


    }
}
