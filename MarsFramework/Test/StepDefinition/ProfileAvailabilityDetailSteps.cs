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
    public class ProfileAccountDetailSteps
    {
        [Order(1)]
        [When(@"I select my availability option")]
        public void WhenISelectMyAvailabilityOption()
        {
            test = extent.StartTest("Profile Availability Detail");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.SelectAvailabilityType();
        }

        [Order(1)]
        [Then(@"I should see the selected availability details displayed on my profile")]
        public void ThenIShouldSeeTheSelectedAvailabilityDetailsDisplayedOnMyProfile()
        {   //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.ValidateAvailabilityType();
        }

        [Order(2)]
        [When(@"I select my hours option")]
        public void WhenISelectMyHoursOption()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.SelectAvailabilityHour();
        }

        [Then(@"I should see the selected hours details displayed on my profile")]
        public void ThenIShouldSeeTheSelectedHoursDetailsDisplayedOnMyProfile()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.ValidateAvailabilityHours();
        }

        [Order(3)]
        [When(@"I select my Earn Target option")]
        public void WhenISelectMyEarnTargetOption()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.SelectAvailabilityTarget();
        }

        [Then(@"I should see the selected Earn Target details displayed on my profile")]
        public void ThenIShouldSeeTheSelectedEarnTargetDetailsDisplayedOnMyProfile()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");

            ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
            profileDetailAvailability.ValidateAvailabilityTarget();
        }


    }
}
