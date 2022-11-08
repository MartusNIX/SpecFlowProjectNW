using NUnit.Framework;
using SpecFlowProjectNW.Context;
using SpecFlowProjectNW.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using SpecFlowProjectNW.Constants;

namespace SpecFlowProjectNW.StepDefinitions
{
    [Binding]
    public sealed class NWStepDefinitions
    {
        private readonly NorthwindContext nwContext;
        private readonly ScenarioContext scenarioContext;
        private int amountBeforeAction;
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
            scenarioContext.Add(StepConstants.Amount, categories.Count);
        }

        [Then(@"the user sees data in the table")]
        public void ThenTheUserSeesDataInTheTable()
        {
            amount = scenarioContext.Get<int>(StepConstants.Amount);
            var isAmountNotNull = amount != 0;
            Assert.IsTrue(isAmountNotNull, "\n Table don't contain data");
        }

        [When(@"the user adds new data in table")]
        public void WhenTheUserAddsNewDataInTable(Table table)
        {
            var categories = nwContext.Categories.ToList();
            scenarioContext.Add(StepConstants.AmountBeforeAction, categories.Count);

            var categoryToAdd = table.CreateSet<Category>().ToList();
            scenarioContext.Add(StepConstants.Amount, categoryToAdd.Count);
            nwContext.Categories.AddRange(categoryToAdd);
            nwContext.SaveChanges();

            var categoriesAfterAction = nwContext.Categories.ToList();
            scenarioContext.Add(StepConstants.AmountAfterAction, categoriesAfterAction.Count);
        }

        [Then(@"data is presented in table")]
        public void ThenDataIsPresentedInTable()
        {
            amountBeforeAction = scenarioContext.Get<int>(StepConstants.AmountBeforeAction);
            amount = scenarioContext.Get<int>(StepConstants.Amount);
            amountAfterAction = scenarioContext.Get<int>(StepConstants.AmountAfterAction);
            Assert.AreEqual(amountBeforeAction + amount, amountAfterAction, "\n Table don't contain added data");
        }
    }
}