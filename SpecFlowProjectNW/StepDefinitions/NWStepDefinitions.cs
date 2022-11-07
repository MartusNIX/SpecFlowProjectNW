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
        public NWStepDefinitions(ScenarioContext scenarioContext)
        {
            nwContext = new NorthwindContext();
            this.scenarioContext = scenarioContext;
        }

        [When(@"the user chooses the table")]
        public void WhenTheUserChoosesTheTable()
        {
            var categories = nwContext.Categories.ToList();
            Console.WriteLine("Current data:");
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
            Assert.IsTrue(isAmountNotNull, "Table don't contain data");
        }

    }
}