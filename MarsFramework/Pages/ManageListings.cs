using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
//using OpenQA.Selenium.Support.PageObjects;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Threading;
using RelevantCodes.ExtentReports;
using System;

namespace MarsFramework.Pages
{
    internal class ManageListings
    {
        public ManageListings()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        //Click on Manage Listings Link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingsLink { get; set; }

        //View the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='eye icon'])[1]")]
        private IWebElement view { get; set; }

        //Delete the listing
        [FindsBy(How = How.XPath, Using = "//table[1]/tbody[1]")]
        private IWebElement delete { get; set; }

        //Edit the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='outline write icon'])[1]")]
        private IWebElement edit { get; set; }

        //Click on Yes or No
        [FindsBy(How = How.XPath, Using = "//div[@class='actions']")]
        private IWebElement clickActionsButton { get; set; }

        //Get the Pagination button
        [FindsBy(How = How.XPath, Using = "//div[@class='ui buttons semantic-ui-react-button-pagination']/button")]
        private IList<IWebElement> paginationButtons { get; set; }

        //Get the table rows
        [FindsBy(How = How.XPath, Using = "//table[@class= 'ui striped table']/tbody/tr")]
        private IList<IWebElement> tableRows { get; set; }

        //Click Next button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'>')]")]
        private IWebElement nextButton { get; set; }

        //Click on Yes button of the delete popup
        [FindsBy(How = How.XPath, Using = "//button[@class='ui icon positive right labeled button'][contains(.,'Yes')]")]
        private IWebElement deletePopupYesButton { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }


        internal void ClickManageListingLink()
        {
            //Navigate to Manage Listing page
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "LinkText", "Manage Listings", 8);
            Thread.Sleep(2000);
            manageListingsLink.Click();
        }

        internal int SearchListings(string CategoryToSearch, string TitleToSearch, string DescriptionToSearch)
        {

            //Initialize the Record count to 0   
            int RecordFound = 0;
           
            //Loop for searching record through the pages
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);
                foreach (IWebElement listingRecord in tableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;

                    if (Category == CategoryToSearch && Title == TitleToSearch && Description == DescriptionToSearch)
                    {
                        RecordFound++;

                    }

                }
                // It will navigate to next page if the next button is enabled
                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }
            }
            // Returning the no. of matching record found 
            return RecordFound;

        }


        internal void DeleteShareSkill(string CategoryToFind, string TitleToFind, string DescriptionToFind)
        {
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "LinkText", "Manage Listings", 8);

            Thread.Sleep(3000);
            //Loop for searching the record through the pages
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                foreach (IWebElement listingRecord in tableRows)

                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;

                    //Comparing Category,title,description to find out corresponding delete button and deleting item
                    if (Category.ToLower() == CategoryToFind.ToLower() && Title.ToLower() == TitleToFind.ToLower() && Description.ToLower().Contains(TitleToFind.ToLower()))
                    {
                        int rowToBeDeleted = tableRows.IndexOf(listingRecord) + 1;
                        Thread.Sleep(3000);
                        listingRecord.FindElement(By.XPath("//tr[" + rowToBeDeleted + "]/td[8]/div/button[3]/i")).Click();

                        Thread.Sleep(3000);
                        //Clicking on "Yes" button from the delete popup 
                        deletePopupYesButton.Click();

                        return;
                    }
                }

                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }
                

            }

        }

        internal void ValidateDeletedShareSkill()
        {
            //Validating deleted share skill
            ManageListings manageListings = new ManageListings();
            int MatchingRecordFoundAfterAdding = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            try
            {
                if (MatchingRecordFoundAfterAdding < 1)
                {
                    Base.test.Log(LogStatus.Pass, "Share Skill deleted successfully");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Share Skill deletion is unsuccessful" + " " + "Screenshot Image " + GlobalDefinitions.SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "ShareSkillScreenshot"));
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed to delete share skill", e.Message);
                Base.test.Log(LogStatus.Fail, "Share Skill deletion is unsuccessful", e.Message);
            }
           
        }



        internal void EditShareSkill(string CategoryToSearch, string TitleToSearch, string DescriptionToSearch)
        {
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "LinkText", "Manage Listings", 8);

            int RecordFound = 0;
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//div[@id='listing-management-section']/div/div[1]/div[1]", 8);

            // GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath("//div[@id='listing-management-section']/div/div[1]/div[1]"), 5);
            Thread.Sleep(2000);

            // Searching for the added share skill record in the Manage Listing and validating is it available or not 
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Loop for searching the added share skill through the pages
                foreach (IWebElement listingRecord in tableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;

                    //Comparing Category,title,description to find out corresponding Edit button and Editing item 
                    if (Category == CategoryToSearch && Title == TitleToSearch && Description == DescriptionToSearch)
                    {
                        //Editing a matched record
                        int rowToBeEdited = tableRows.IndexOf(listingRecord) + 1;
                        listingRecord.FindElement(By.XPath("//tr[" + rowToBeEdited + "]/td[8]/div/button[2]/i")).Click();
                        Thread.Sleep(2000);

                        //log after editing
                        Base.test.Log(LogStatus.Pass, "Share skill with titlename" + " " + Title + " " + "edit button is clicked and navigated to Edit share skill page");
                        RecordFound++;

                        GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "EditShareSkill");

                        //Calling EnterShareSkill() for adding share skill data
                        ShareSkill shareSkill = new ShareSkill();
                        //shareSkill.EnterShareSkill();
                        shareSkill.EditShareSkillData();

                        Thread.Sleep(2000);
                        //Clicking Save button
                        Save.Click();

                        return;

                    }


                }

                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }

            }


        }


        internal void ValidateUpdatedShareSkill()
        {
            
            string CategoryValueFromExcel = GlobalDefinitions.ExcelLib.ReadData(4, "Category").ToLower();
            string TitleValueFromExcel = GlobalDefinitions.ExcelLib.ReadData(4, "Title").ToLower();
            string DescriptionValueFromExcel = GlobalDefinitions.ExcelLib.ReadData(4, "Description").Replace("ers", "...").ToLower();
            string[] ServiceTypeFromExcel = GlobalDefinitions.ExcelLib.ReadData(4, "ServiceType").Split(' ');
            string ServiceTypeValueAfterSplit = ServiceTypeFromExcel[0].ToLower();

            // Searching for the added share skill record in the Manage Listing and validating is it available or not 
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Loop for searching the added share skill through the pages
                foreach (IWebElement listingRecord in tableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text.ToLower();
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text.ToLower();
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text.ToLower();
                    string ServiceType = listingRecord.FindElement(By.XPath("td[5]")).Text.ToLower();
                    try
                    {
                        Assert.Multiple(() =>
                        {
                            Assert.That(Category, Is.EqualTo(CategoryValueFromExcel));
                            Assert.That(Title, Is.EqualTo(TitleValueFromExcel));
                            Assert.That(Description, Is.EqualTo(DescriptionValueFromExcel));
                            Assert.That(ServiceType, Is.EqualTo(ServiceTypeValueAfterSplit));

                        });
                        Base.test.Log(LogStatus.Pass, "Edited and saved a Share Skill Successfully");


                    }


                    catch (Exception e)
                    {
                        Assert.Fail("Failed to update the share skill", e.Message);
                        Base.test.Log(LogStatus.Fail, "Edit and updating a Share Skill is unsuccessful", e.Message);
                    }

                    return;

                }
                // It will navigate to next page if the next button is enabled
                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }

            }


        }
    }
}

