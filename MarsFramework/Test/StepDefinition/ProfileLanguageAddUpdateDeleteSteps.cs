using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;


namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ProfileLanguageAddUpdateDeleteSteps
    {
        [Given(@"I navigate to language tab under profile page")]
        public void GivenINavigateToLanguageTabUnderProfilePage()
        {
            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.NavigateToLanguageTab();
        }

        [When(@"I add a new language")]
        public void WhenIAddANewLanguage()
        {
            test = extent.StartTest("Add/Update/Delete Language");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.AddLanguage();
        }

        [Then(@"that language should be displayed on my listings")]
        public void ThenThatLanguageShouldBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.ValidateAddedLanguage();


        }

        [When(@"I delete an existing language")]
        public void WhenIDeleteAnExistingLanguage()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.DeleteLanguage();
        }
        [Then(@"that language should not be displayed on my listings")]
        public void ThenThatLanguageShouldNotBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.ValidateDeletedLanguage();
        }

        [When(@"I update an existing language")]
        public void WhenIUpdateAnExistingLanguage()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.UpdateAddedLanguage();
        }

        [Then(@"that updated language should be displayed on my listings")]
        public void ThenThatUpdatedLanguageShouldBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathProfileDetail, "Language");

            ProfileLanguage profileLanguage = new ProfileLanguage();
            profileLanguage.ValidateUpdatedLanguage();
        }

    }
   
}
