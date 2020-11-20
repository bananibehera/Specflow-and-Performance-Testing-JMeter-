using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;


namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class CreateShareSkillSteps
    {
        [When(@"I click on the Share skill button")]
        public void WhenIClickOnTheShareSkillButton()
        {
            ShareSkill shareSkill = new ShareSkill();
            shareSkill.ClickShareSkillButton();
        }

        [Then(@"I should successfully navigate to service listing form page")]
        public void ThenIShouldSuccessfullyNavigateToServiceListingFormPage()
        {
            test = extent.StartTest("Add Share Skills Details");
            ShareSkill shareSkill = new ShareSkill();
            shareSkill.ValidateNavigateToServiceListingPage();
        }

        [When(@"I enter all the valid data for the service")]
        public void WhenIEnterAllTheValidDataForTheService()
        {
            
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathAddShareSkill, "ShareSkill");

            ShareSkill shareSkill = new ShareSkill();
            shareSkill.EnterShareSkill();
        }

        [When(@"I click on the save button")]
        public void WhenIClickOnTheSaveButton()
        {
            ShareSkill shareSkill = new ShareSkill();
            shareSkill.ClickOnSaveButton();
        }

        [Then(@"I should see the service successfully displayed in the manage listings page")]
        public void ThenIShouldSeeTheServiceSuccessfullyDisplayedInTheManageListingsPage()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "ShareSkill");

            ShareSkill shareSkill = new ShareSkill();
            shareSkill.ValidatingAddedShareSkill();
        }


    }
}
