using Gherkin;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SpecFlowProjectNW.Context;
using SpecFlowProjectNW.Models;

namespace SpecFlowProjectNW.StepDefinitions
{
    [Binding]
    public sealed class NWStepDefinitions
    {
        private readonly NorthwindContext nwContext;
        private readonly ScenarioContext scenarioContext;
        private int amount;
        private int amountAfterAction;
        public NWStepDefinitions(ScenarioContext scenarioContext)
        {
            nwContext = new NorthwindContext();
            this.scenarioContext = scenarioContext;
        }

        [When(@"the user chooses the table")]
        public void WhenTheUserChoosesTheTable()
        {
            var categories = nwContext.Categories.ToList();
            Console.WriteLine("\n Current data:");
            foreach (Category c in categories)
            {
                Console.WriteLine($"{c.CategoryId}.{c.CategoryName}.{c.Description}");
            }
            scenarioContext.Add("Amount", categories.Count);
        }

        [Then(@"the user sees data in the table")]
        public void ThenTheUserSeesDataInTheTable()
        {
            amount = scenarioContext.Get<int>("Amount");
            var isAmountNotNull = amount != 0;
            Assert.IsTrue(isAmountNotNull, "\n Table don't contain data");
        }

        [When(@"the user adds new data")]
        public void WhenTheUserAddsNewData()
        {
            var categories = nwContext.Categories.ToList();
            scenarioContext.Add("Amount", categories.Count);
            Category category1 = new Category
            {
                CategoryName = "Drinks",
                Description = "Water, Juice, Cidre"
            };

            nwContext.Categories.Add(category1);
            nwContext.SaveChanges();

            var categoriesAfterAction = nwContext.Categories.ToList();
            scenarioContext.Add("AmountAfterAction", categoriesAfterAction.Count);
        }

        [Then(@"data is presented in table")]
        public void ThenDataIsPresentedInTable()
        {
            amount = scenarioContext.Get<int>("Amount");
            amountAfterAction = scenarioContext.Get<int>("AmountAfterAction");
            Assert.AreEqual(amount + 1, amountAfterAction);
        }

    }
}