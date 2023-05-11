
using ArenaGestor.BusinessHelpers;
using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using System.Collections.Generic;
using System;
using System.Linq;
using ArenaGestor.DataAccessInterface;

namespace ArenaGestor.Business
{
    public class SnacksService : ISnacksService
    {
        private readonly ISnacksManagement snackManagement;

        public SnacksService(ISnacksManagement snacksManagement)
        {
            this.snackManagement = snacksManagement;
        }

        public IEnumerable<Snack> GetSnacks()
        {
            return snackManagement.GetSnacks(x => x.Id == x.Id);
        }

        public Snack InsertSnack(Snack snack)
        {
            ValidSnack(snack);

            var snacks = snackManagement.ExistsSnack(x => x.Name == snack.Name);
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

            var snackToDelete = snackManagement.GetSnacks(x => x.Id == snackId);
            if (snackToDelete != null && !snackToDelete.Any())
            {
                throw new NullReferenceException($"The snack with identifier: {snackId} doesn't exists.");
            }
            snackManagement.DeleteSnack(snackToDelete.First());
            snackManagement.Save();
        }

        private static void ValidSnack(Snack snack)
        {
            if (snack == null || snack.Name == null || snack.Description == null || snack.Price == 0 || snack.Name == "" || snack.Description == "")
            {
                throw new ArgumentException("You must send a complete snack");
            }
        }

        public Snack BuySnack(Snack snackBuy)
        {
            if (snackBuy == null)
            {
                throw new ArgumentException("Invalid data in purchase");
            }

            Snack snack = snackManagement.GetSnacks(x => x.Id == snackBuy.Id).FirstOrDefault();

            if (snack == null)
            {
                throw new NullReferenceException("This snack does not exist");
            }
            else
            {
                return snack;
            }
        }
    }
}
