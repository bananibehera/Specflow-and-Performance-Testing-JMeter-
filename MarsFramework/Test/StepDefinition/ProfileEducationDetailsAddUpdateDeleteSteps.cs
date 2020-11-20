using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ProfileEducationDetailsAddUpdateDeleteSteps
    {
        [Given(@"I navigate to profile details page")]
        public void GivenINavigateToProfileDetailsPage()
        {
            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.NavigateToProfileTab();
        }


        [Given(@"I navigate to Education tab")]
        public void GivenINavigateToEducationTab()
        {
            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.NavigateToEducationPage();
        }

        [When(@"I add a new education detail")]
        public void WhenIAddANewEducationDetail()
        {

            test = extent.StartTest("Education details");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.AddEducation();
        }

        [Then(@"that education details should be displayed on my listings")]
        public void ThenThatEducationDetailsShouldBeDisplayedOnMyListings()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.ValidateAddedEducation();
        }

        [When(@"I delete an existing education")]
        public void WhenIDeleteAnExistingEducation()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.DeleteEducation();
        }

        [Then(@"that education details should not be displayed on my listings")]
        public void ThenThatEducationDetailsShouldNotBeDisplayedOnMyListings()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.ValidateDeletedEducation();
        }

        [When(@"I update an existing education details")]
        public void WhenIUpdateAnExistingEducationDetails()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.UpdateAddedEducation();
        }

        [Then(@"that updated education details should be displayed on my listings")]
        public void ThenThatUpdatedEducationDetailsShouldBeDisplayedOnMyListings()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");

            ProfileEducation profileEducation = new ProfileEducation();
            profileEducation.ValidateUpdatedEducation();
        }




    }
}
