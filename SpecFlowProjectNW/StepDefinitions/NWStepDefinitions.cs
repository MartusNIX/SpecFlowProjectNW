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
        public NWStepDefinitions()
        {
            nwContext = new NorthwindContext();
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
        }

        [Then(@"the user sees data in the table")]
        public void ThenTheUserSeesDataInTheTable()
        {
            Assert.IsTrue(true);
        }

    }
}