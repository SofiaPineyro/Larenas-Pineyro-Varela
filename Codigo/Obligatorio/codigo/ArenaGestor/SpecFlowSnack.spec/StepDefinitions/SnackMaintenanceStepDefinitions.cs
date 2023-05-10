using System;
using TechTalk.SpecFlow;

namespace SpecFlowSnack.spec.StepDefinitions
{
    [Binding]
    public class SnackMaintenanceStepDefinitions
    {
        [Given(@"I am an administrator")]
        public void GivenIAmAnAdministrator()
        {
            throw new PendingStepException();
        }

        [When(@"I add a new snack with name ""([^""]*)"", description ""([^""]*)"", and price (.*)")]
        public void WhenIAddANewSnackWithNameDescriptionAndPrice(string chips, string p1, int p2)
        {
            throw new PendingStepException();
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the snack with name ""([^""]*)"" should be added to the snack list")]
        public void ThenTheSnackWithNameShouldBeAddedToTheSnackList(string chips)
        {
            throw new PendingStepException();
        }

        [Given(@"there is already a snack with name ""([^""]*)"" in the snack list")]
        public void GivenThereIsAlreadyASnackWithNameInTheSnackList(string chips)
        {
            throw new PendingStepException();
        }

        [Given(@"there is a snack with ID (.*) in the snack list")]
        public void GivenThereIsASnackWithIDInTheSnackList(int p0)
        {
            throw new PendingStepException();
        }

        [When(@"I delete the snack with ID (.*)")]
        public void WhenIDeleteTheSnackWithID(int p0)
        {
            throw new PendingStepException();
        }

        [Then(@"the snack with ID (.*) should no longer appear in the snack list")]
        public void ThenTheSnackWithIDShouldNoLongerAppearInTheSnackList(int p0)
        {
            throw new PendingStepException();
        }

        [Given(@"there is no snack with ID (.*) in the snack list")]
        public void GivenThereIsNoSnackWithIDInTheSnackList(int p0)
        {
            throw new PendingStepException();
        }
    }
}
