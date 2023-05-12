using System;
using ArenaGestor.API;
using ArenaGestor.API.Controllers;
using ArenaGestor.APIContracts.Snack;
using ArenaGestor.APIContracts.Ticket;
using ArenaGestor.Business;
using ArenaGestor.BusinessInterface;
using ArenaGestor.DataAccess;
using ArenaGestor.DataAccess.Managements;
using ArenaGestor.DataAccessInterface;
using ArenaGestor.Domain;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TechTalk.SpecFlow;

namespace SpecFlowSnack.spec.StepDefinitions
{
    [Binding]
    public class SnackPurchaseStepDefinitions
    {
        private User user;

        private readonly Snack snack1;
        private readonly Snack snack2;
        private BuySnackDto? buySnackDto;

        private ISnacksService snacksService;
        private SnacksController snackController;

        private DbContext context;
        private ISnacksManagement management;

        private Exception? exception;
        private int? _responseCode;

        public SnackPurchaseStepDefinitions()
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

            snack1 = new Snack()
            {
                Name = "Hot dog",
                Description = "Panchos al pan",
                Price = 500
            };
            snack2 = new Snack()
            {
                Name = "chips",
                Description = "estas son papas muy caras",
                Price = 500
            };

            context.Add(snack1);
            context.Add(snack2);
            context.SaveChanges();
        }

        [Given(@"I am an spectator")]
        public void GivenIAmAnSpectator()
        {
            user = new User()
            {
                UserId = 2,
                Name = "Test",
                Surname = "User",
                Email = "test@user.com",
                Password = "testuser123",
                Roles = new List<UserRole>()
                {
                    new UserRole()
                    {
                        RoleId = RoleCode.Espectador,
                    }
                }
            };
        }

        [Given(@"there are snacks available")]
        public void GivenThereAreSnacksAvailable()
        {
            var snacks = management.GetSnacks(x => x.Id == x.Id).AsEnumerable();
            snacks.Should().HaveCountGreaterThan(0);
        }

        [Given(@"I select one snack with id (.*) and quantity (.*)")]
        public void GivenISelectOneSnackWithIdAndQuantity(int id, int quantity)
        {
            buySnackDto = new BuySnackDto()
            {
                Id = id,
                Quantity = quantity,
            };
        }

        [When(@"I confirm the purchase")]
        public void WhenIConfirmThePurchase()
        {
            var result = snackController.PostSnacks(buySnackDto);
            var objectResult = result as OkObjectResult;
            _responseCode = objectResult.StatusCode;
        }

        [Then(@"the purchase is completed")]
        public void ThenThePurchaseIsCompleted()
        {
            _responseCode.Should().Be(200);
        }
    }
}
