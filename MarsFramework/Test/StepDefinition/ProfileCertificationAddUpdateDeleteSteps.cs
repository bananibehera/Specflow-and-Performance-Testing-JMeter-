using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ProfileCertificationAddUpdateDeleteSteps
    {
        [Given(@"I navigate to certification tab")]
        public void GivenINavigateToCertificationTab()
        {
            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.NavigateToCertificationTab();
        }
        
        [When(@"I add a new certification detail")]
        public void WhenIAddANewCertificationDetail()
        {
            test = extent.StartTest("Add/Delete/Update Certification");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.AddCertification();
        }

        [Then(@"that certification details should be displayed on my listings")]
        public void ThenThatCertificationDetailsShouldBeDisplayedOnMyListings()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.ValidateAddedCertification();
        }

        [When(@"I delete an existing certification")]
        public void WhenIDeleteAnExistingCertification()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.DeleteCertification();
        }


        [Then(@"that certification details should not be displayed on my listings")]
        public void ThenThatCertificationDetailsShouldNotBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.ValidateDeletedCertification();
        }

        [When(@"I update an existing certification details")]
        public void WhenIUpdateAnExistingCertificationDetails()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.UpdateAddedCertification();
        }
                
        [Then(@"that updated certification details should be displayed on my listings")]
        public void ThenThatUpdatedCertificationDetailsShouldBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");

            ProfileCertification profileCertification = new ProfileCertification();
            profileCertification.ValidateUpdatedCertification();
        }
    }
}
