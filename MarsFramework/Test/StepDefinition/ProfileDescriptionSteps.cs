using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;


namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ProfileDescriptionSteps
    {
        [When(@"I add a short summary/description")]
        public void WhenIAddAShortSummaryDescription()
        {
            test = extent.StartTest("Add profile description");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Profile Description");

            ProfileDescription profileDescription = new ProfileDescription();
            profileDescription.AddDescription();
        }
        
        [Then(@"I should see the short summary/description should be displayed on my profile")]
        public void ThenIShouldSeeTheShortSummaryDescriptionShouldBeDisplayedOnMyProfile()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Profile Description");

            ProfileDescription profileDescription = new ProfileDescription();
            profileDescription.ValidateDescription();
        }
    }
}
