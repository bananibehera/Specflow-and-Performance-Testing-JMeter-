using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;
using NUnit.Framework;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ManageListingSteps
    {
        [Given(@"I navigate to Manage Listing page")]
        public void GivenINavigateToManageListingPage()
        {
            ManageListings manageListings = new ManageListings();
            manageListings.ClickManageListingLink();
        }

       
        [When(@"I edit the existing shared skill service details")]
        public void WhenIEditTheExistingSharedSkillServiceDetails()
        {
            test = extent.StartTest("Edit Share skills Details");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathAddShareSkill, "ShareSkill");

            ManageListings manageListings = new ManageListings();
            manageListings.EditShareSkill(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

        }

        [Then(@"that service should be edited and updated in the list")]
        public void ThenThatServiceShouldBeEditedAndUpdatedInTheList()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathAddShareSkill, "EditShareSkill");

            ManageListings manageListings = new ManageListings();
            manageListings.ValidateUpdatedShareSkill();
        }

        
        [When(@"I delete the existing shared skill service details")]
        public void WhenIDeleteTheExistingSharedSkillServiceDetails()
        {

            test = extent.StartTest("Delete Share skills Details");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageShareSkill, "ManageListings");

            ManageListings manageListings = new ManageListings();
            manageListings.DeleteShareSkill(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

        }

        [Then(@"that service should not be displayed in the list")]
        public void ThenThatServiceShouldNotBeDisplayedInTheList()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageShareSkill, "ManageListings");

            ManageListings manageListings = new ManageListings();
            manageListings.ValidateDeletedShareSkill();

        }


    }
}
