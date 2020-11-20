using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    class ProfileSkills
    {

        public ProfileSkills()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region Initializing web elements

        //Navigating to Profile detail page
        [FindsBy(How = How.LinkText, Using = "Profile")]
        private IWebElement ProfileTab { get; set; }

        //Navigating to Skills tab
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Skills')]")]
        private IWebElement SkillsTab { get; set; }

        //Add new skill button
        [FindsBy(How = How.XPath, Using = "//th[contains(text(),'Skill')]/..//div[text()='Add New']")]
        private IWebElement addNewSkillsButton { get; set; }

        //Add new Skill
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Add Skill']")]
        private IWebElement addNewSkillsText { get; set; }

        //Selecting Skill level
        [FindsBy(How = How.Name, Using = "level")]
        private IWebElement SkillsLevelDropdown { get; set; }

        //Clicking Add button
        [FindsBy(How = How.XPath, Using = "//input[contains(@class,'ui teal button ')][contains(@value,'Add')]")]
        private IWebElement addButtonSkills { get; set; }

        //Clicking Update button 
        [FindsBy(How = How.XPath, Using = "//input[contains(@value,'Update')]")]
        private IWebElement updateButtonSkills { get; set; }

        private int NumberOfSkillsFound = 0;

        //Number of record list
        [FindsBy(How = How.XPath, Using = "//th[contains(text(),'Skill')]/../../following-sibling::tbody")]
        private IList<IWebElement> SkillRecords { get; set; }

        //Skill body xpath
        private string SkillsTableBody => "//th[contains(text(),'Skill')]/../../following-sibling::tbody";

        //Number of skills to add
        int NumberOfSkillsToAdd => Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "NumberOfSkillsToAdd"));
        #endregion


        public void NavigateToProfileTab()
        {
            // Clicking on the profile tab
            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "LinkText", "Profile", 5);
            ProfileTab.Click();
        }

        public void NavigateToSkillsTab()
        {
            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//a[contains(text(),'Skills')]", 2);
            SkillsTab.Click();

        }

        public void AddSkills()
        {

            for (int i = 1; i <= NumberOfSkillsToAdd; i++)

            {
                //Reading skills data from excel sheet
                String skillsData = GlobalDefinitions.ExcelLib.ReadData(i + 1, "Skill");

                //Clicking the Add New button
                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//th[contains(text(),'Skill')]/..//div[text()='Add New']", 4);
                addNewSkillsButton.Click();
                Thread.Sleep(2000);

                //Entering the skills data into the skill textbox
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//input[@placeholder='Add Skill']", 2);
                addNewSkillsText.SendKeys(skillsData);

                //Selecting the skills level
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "level", 2);
                SelectElement chooseLanguageLevel = new SelectElement(SkillsLevelDropdown);
                var skillLevelData = GlobalDefinitions.ExcelLib.ReadData(i + 1, "SkillLevel");
                chooseLanguageLevel.SelectByValue(skillLevelData);

                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//input[contains(@class,'ui teal button ')][contains(@value,'Add')]", 4);
                //Clicking Add button
                addButtonSkills.Click();
                string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Skills added");
            }
        }

        public void SearchSkill()
        {
            //Searching skill
            NumberOfSkillsFound = 0;
            for (int i = 1; i <= NumberOfSkillsToAdd; i++)
            {

                for (int j = 1; j <= SkillRecords.Count; j++)
                {
                    String SkillValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                    String SkillLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                    if (SkillValue == (GlobalDefinitions.ExcelLib.ReadData(i + 1, "Skill")) && SkillLevelValue == (GlobalDefinitions.ExcelLib.ReadData(i + 1, "SkillLevel")))
                    {
                        NumberOfSkillsFound++;
                        break;
                    }
                }
            }
        }

        public void ValidateAddedSkills()
        {
            //Searching for the added skills in the available skill list 
            SearchSkill();
            try
            {
                //Verifying all the added skills available in the skills list
                Assert.AreEqual(NumberOfSkillsToAdd, NumberOfSkillsFound );
                Base.test.Log(LogStatus.Pass, "The skill added found in the list");

            }

            catch (Exception e)
            {
                Assert.Fail("Skill added not found",e.Message);
                Base.test.Log(LogStatus.Fail, "The skill added is not found in the list", e.Message);
            }

        }


        public void UpdateAddedSkill()
        {
            //Reading skill and updated skill level from the excel 
            String skillToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "SkillToUpdate");
            String skillLevelToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "SkillLevelToUpdate");

            //Searching through the skill list to find a skill need to be updated   
            for (int j = 1; j <= SkillRecords.Count; j++)
            {
                String SkillValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String SkillLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                if (SkillValue == (GlobalDefinitions.ExcelLib.ReadData(4, "Skill")) && SkillLevelValue == (GlobalDefinitions.ExcelLib.ReadData(4, "SkillLevel")))
                {

                    //Clicking edit button of the skill needed to update
                    GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", SkillsTableBody + "[" + j + "]" +"//tr[1]//td[3]//span[1]//i[1]", 4);
                    GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]"+ "//tr[1]//td[3]//span[1]//i[1]")).Click();
                    Thread.Sleep(2000);

                    //Selecting the updated skill level
                    SelectElement chooseLanguageLevel = new SelectElement(SkillsLevelDropdown);
                    chooseLanguageLevel.SelectByValue(skillLevelToUpdate);

                    //Clicking Update button
                    GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//input[contains(@value,'Update')]", 4);
                    updateButtonSkills.Click();
                    string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Skills Updated");

                    Base.test.Log(LogStatus.Pass, "Skills updated successfully");
                    break;
                }
            }
          
        }

        public void ValidateUpdatedSkill()
        {
            //Searching for the skill which was updated among the available skill list
            for (int j = 1; j <= SkillRecords.Count; j++)
            {
                String SkillValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String SkillLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                try
                {
                    //Verifying the skill is updated and found in the available skill list
                    if (SkillValue == (GlobalDefinitions.ExcelLib.ReadData(2, "SkillToUpdate")) && SkillLevelValue == (GlobalDefinitions.ExcelLib.ReadData(2, "SkillLevelToUpdate")))
                    {
                        Assert.IsTrue(true);
                        Base.test.Log(LogStatus.Pass, "The skill updated found in the list");
                        break;
                    }

                }

                catch (Exception e)
                {
                    Assert.IsTrue(false, e.Message);
                    Base.test.Log(LogStatus.Fail, "The skill updated is not found in the list", e.Message);
                }
            }

        }


        public void DeleteSkill()
        {

            String SkillToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "SkillToDelete");
            String SkillLevelToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "SkillLevelToDelete");

            //Searching for the skill to delete from the skill list
            for (int j = 1; j <= SkillRecords.Count; j++)
            {
                String SkillValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String SkillLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                try
                {
                    //Deleting a particular skill if found from the available skill list
                    if (SkillValue == SkillToDelete && SkillLevelValue == SkillLevelToDelete)
                    {
                        GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", SkillsTableBody + "[" + j + "]" + "/tr[1]/td[3]/span[2]/i[1]", 5);
                        GlobalDefinitions.driver.FindElement(By.XPath(SkillsTableBody + "[" + j + "]" + "/tr[1]/td[3]/span[2]/i[1]")).Click();
                        //Thread.Sleep(2000);
                        GenericWait.ElementIsVisible(GlobalDefinitions.driver, "ClassName", "ns-box-inner", 4);
                        String DeleteAlertPopupText = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner")).Text;
                       
                        Assert.IsTrue(DeleteAlertPopupText.Contains("deleted"));
                        Base.test.Log(LogStatus.Pass, "The skill deleted from the list");
                        string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Skills Deleted");

                        break;
                    }

                }
                catch (Exception e)
                {
                    Assert.Fail("Failed to delete the skill", e.Message);
                    Base.test.Log(LogStatus.Fail, "The skill is not deleted from the list", e.Message);
                }
            }
        }

        public void ValidateDeletedSkill()
        {
            //Searching through the list of skill available under the skill tab 
            SearchSkill();
            try
            {
                //Verifying the deleted skill is not available and deleted from the list
                Assert.AreEqual(NumberOfSkillsToAdd - 1, NumberOfSkillsFound);
                Base.test.Log(LogStatus.Pass, "Skill deleted from the list");

            }

            catch (Exception e)
            {
                Assert.AreNotEqual(NumberOfSkillsToAdd - 1, NumberOfSkillsFound,"Skills failed to delete from the list ");
                Base.test.Log(LogStatus.Fail, "Skills failed to delete from the list", e.Message);
            }

        }

    }

}

