using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class SearchSkillSteps
    {
        [When(@"I search a skill")]
        public void WhenISearchASkill()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");
            test = extent.StartTest("Search Skill");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.EnterSkillIntoSearchBox(GlobalDefinitions.ExcelLib.ReadData(2, "SearchTerm"));
            searchSkill.ClickOnACategory(GlobalDefinitions.ExcelLib.ReadData(2, "Category"));
            searchSkill.ClickOnASubCategory(GlobalDefinitions.ExcelLib.ReadData(2, "Subcategory"));
        }

        [Then(@"that skill related result should displayed in category and subcategory")]
        public void ThenThatSkillRelatedResultShouldDisplayedInCategoryAndSubcategory()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");
           
            SearchSkill searchSkill = new SearchSkill();
            searchSkill.VerifyCategorySubCategorySearch(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "SubIndex")));

        }

        [When(@"I search a skill based on the registered user")]
        public void WhenISearchASkillBasedOnTheRegisteredUser()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");
            test = extent.StartTest("Filter Search Skill by user");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.EnterSkillIntoSearchBox(GlobalDefinitions.ExcelLib.ReadData(2, "SearchTerm"));
            searchSkill.SearchUserFromSearchUserTextBox(GlobalDefinitions.ExcelLib.ReadData(2, "User"));
        }
        
       
        
        [Then(@"I should be able to see the skills result listed by that user")]
        public void ThenIShouldBeAbleToSeeTheSkillsResultListedByThatUser()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.VerifyUserServicesListed(GlobalDefinitions.ExcelLib.ReadData(2, "User"));
        }

        [When(@"I search a skill using refine search textbox")]
        public void WhenISearchASkillUsingRefineSearchTextbox()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");
            test = extent.StartTest("Filter Search Skill");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.EnterSkillIntoSearchBox(GlobalDefinitions.ExcelLib.ReadData(2, "SearchTerm"));
            searchSkill.EnterSkillIntoFilterSearchBox(GlobalDefinitions.ExcelLib.ReadData(2, "FilterSearchTerm"));
        }


        [When(@"I Click on any Filter option")]
        public void WhenIClickOnAnyFilterOption()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.ClickOnFilters(GlobalDefinitions.ExcelLib.ReadData(2, "FilterOption"));

        }

        [Then(@"I should be able the see the result displayed based on the filter option")]
        public void ThenIShouldBeAbleTheSeeTheResultDisplayedBasedOnTheFilterOption()
        {
            //Populating excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(ExcelPathSearchSkill, "SearchSkill");

            SearchSkill searchSkill = new SearchSkill();
            searchSkill.VerifyResultwithFilter(Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "TotalSkill")), Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "RefineSkill")), GlobalDefinitions.ExcelLib.ReadData(2, "FilterOption"));
        }



    }
}
