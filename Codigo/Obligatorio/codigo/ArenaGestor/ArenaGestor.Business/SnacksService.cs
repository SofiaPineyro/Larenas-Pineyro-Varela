
using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ArenaGestor.Business
{
    public class SnacksService
    {
        private readonly ISnacksManagement snackManagement;
        private readonly IUsersService usersService;

        public SnacksService(ISnacksManagement snacksManagement, IUsersService usersService)
        {
            this.snackManagement = snacksManagement;
            this.usersService = usersService;
        }

        public IEnumerable<Snack> GetSnacks()
        {
            return snackManagement.GetSnacks();
        }

        public Snack InsertSnack(Snack snack)
        {
            ValidSnack(snack);

            Snack snackCheckName = new Snack()
            {
                Name = snack.Name
            };

            var snacks = snackManagement.Exists(x => x.Name == snackCheckName);
            if (snacks)
            {
                throw new ArgumentException($"It already exists an snack with name: {snack.Name}");
            }

            snackManagement.InsertSnack(snack);
            snackManagement.Save();
            return snack;
        }


        public void DeleteSnack(int snackId)
        {
            CommonValidations.ValidId(snackId);

            var snackToDelete = snackManagement.Exists(x => x.Id == snackId);
            if (!snackToDelete)
            {
                throw new NullReferenceException($"The snack with identifier: {snackId} doesn't exists.");
            }
            snackManagement.DeleteSnack(snackToDelete);
            snackManagement.Save();
        }

        private static void ValidSnack(Snack snack)
        {
            if (snack == null || snack.Name == null || snack.Description == null || snack.Price == 0 || snack.Name == "" || snack.Description == "")
            {
                throw new ArgumentException("You must send a complete snack");
            }
        }


    }
}
