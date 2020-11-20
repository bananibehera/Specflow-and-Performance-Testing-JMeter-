using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ProfileSkillsAddUpdateDeleteSteps
    {
        [Given(@"I navigate to skill tab")]
        public void GivenINavigateToSkillTab()
        {
            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.NavigateToSkillsTab();
        }
        
        [When(@"I add a new skill")]
        public void WhenIAddANewSkill()
        {
            test = extent.StartTest("Skills Add/update/delete detail");
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.AddSkills();
        }

        [Then(@"that skill should be displayed on my listings")]
        public void ThenThatSkillShouldBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.ValidateAddedSkills();
        }

        [When(@"I delete an existing skill")]
        public void WhenIDeleteAnExistingSkill()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.DeleteSkill();
        }

        [Then(@"that skill should not be displayed on my listings")]
        public void ThenThatSkillShouldNotBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.ValidateDeletedSkill();
        }

        [When(@"I update an existing skill")]
        public void WhenIUpdateAnExistingSkill()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.UpdateAddedSkill();
        }
              
        [Then(@"that updated skill should be displayed on my listings")]
        public void ThenThatUpdatedSkillShouldBeDisplayedOnMyListings()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

            ProfileSkills profileSkills = new ProfileSkills();
            profileSkills.ValidateUpdatedSkill();
        }
    }
}
