using MarsFramework.Global;
using MarsFramework.Pages.Helper;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoIt;
using AutoItX3Lib;
using RelevantCodes.ExtentReports.Model;
using RelevantCodes.ExtentReports;
using System.IO;
using OpenQA.Selenium.Interactions;
using System.Threading;
using static MarsFramework.Global.GlobalDefinitions;




namespace MarsFramework.Pages
{
    internal class ShareSkill 
    {
        public ShareSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region Intializing web elements
        //Click on ShareSkill Button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }
       
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }
      
        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }
        private SelectElement ChooseCategory => new SelectElement(CategoryDropDown);

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        private IWebElement Tags { get; set; }

        //Select the Service type
        [FindsBy(How = How.XPath, Using = "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']")]
        private IList<IWebElement> ServiceTypeOptions { get; set; }

        //Select the Location Type
        [FindsBy(How = How.XPath, Using = "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IList<IWebElement> LocationTypeOption { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]")]
        private IWebElement Days { get; set; }

        //Storing the starttime
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTime { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        private IWebElement EndTime { get; set; }

        //Click on StartTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTimeDropDown { get; set; }

        //Click on EndTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        private IWebElement EndTimeDropDown { get; set; }

        //Click on Skill Trade option
        [FindsBy(How = How.XPath, Using = "//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement SkillTradeOption { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        private IWebElement SkillExchange { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //Uploading the file
        [FindsBy(How = How.XPath, Using = "//i[@class='huge plus circle icon padding-25']")]
        private IWebElement WorkSamples { get; set; }

        //Click on Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IWebElement ActiveOption { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }

        //Entered tag text
        [FindsBy(How = How.XPath, Using = "//*[text()='Tags']/../..//span[@Class='ReactTags__tag']")]
        private IWebElement TagReact { get; set; }

        //Entered Skill exchange text
        [FindsBy(How = How.XPath, Using = "//*[text()='Skill-Exchange']/../..//span[@Class='ReactTags__tag']")]
        private IWebElement SkillExchangeTag { get; set; }

        //Mouse hover on WorkSample plus icon and the tooltiptext
        [FindsBy(How = How.ClassName, Using = "tooltiptext")]
        private IWebElement WorkSampleFileName { get; set; }
        
        //Click on Manage Listing Page
        [FindsBy(How = How.LinkText, Using = "//a[contains(text(),'Manage Listings')]")]
        private IWebElement ManageListingLink { get; set; }

        //Get the Pagination buttons
        [FindsBy(How = How.XPath, Using = "//div[@class='ui buttons semantic-ui-react-button-pagination']/button")]
        private IList<IWebElement> paginationButtons { get; set; }

        //Get the table rows
        [FindsBy(How = How.XPath, Using = "//table[@class= 'ui striped table']/tbody/tr")]
        private IList<IWebElement> tableRows { get; set; }

        //Click Next button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'>')]")]
        private IWebElement nextButton { get; set; }

        //Click on managelisting link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingsLink { get; set; }
        #endregion

        internal void  ClickShareSkillButton()
        {

            //Wait for ShareSkill Button
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "LinkText", "Share Skill",6);

            //Click ShareSkill Button
            ShareSkillButton.Click();
        }
        
         
        internal void EnterShareSkill()
        {
           
            //Calling wait method
            GenericWait.ElementExists(GlobalDefinitions.driver, "Name", "title", 8);

            //Entering the "Title"
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));
            //Validating Title
            GlobalDefinitions.TextDataFieldValidation("Title", GlobalDefinitions.ExcelLib.ReadData(2, "Title"), Title.GetAttribute("value"));

            //Entering the "Description"
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            //Validating Description
            GlobalDefinitions.TextDataFieldValidation("Description", GlobalDefinitions.ExcelLib.ReadData(2, "Description"), Description.Text);
            
            //Selecting Category
            HelperCallingMethods.SelectingDropdown(CategoryDropDown, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Category"));
            //Validating Category selection
            GlobalDefinitions.DropDownDataValidation("Category", CategoryDropDown, GlobalDefinitions.ExcelLib.ReadData(2, "Category"));
             
            //Selecting Sub-Category
            HelperCallingMethods.SelectingDropdown(SubCategoryDropDown, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));
            //Validating Sub-Category selection
            GlobalDefinitions.DropDownDataValidation("SubCategory", SubCategoryDropDown, GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));
            
            //Entering Tag
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tags") + "\n");
            //GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath("//span[@class='ReactTags__tag']"), 5);
            GenericWait.ElementIsVisible(GlobalDefinitions.driver,"XPath", "//span[@class='ReactTags__tag']",6);
            string EnteredTagTxt = TagReact.Text;
            //Validating entered Tag
            GlobalDefinitions.TextDataFieldValidation("Tags", GlobalDefinitions.ExcelLib.ReadData(2, "Tags"), EnteredTagTxt.Remove(EnteredTagTxt.Length - 1, 1));          
            
            // Select the Service type radio button
            HelperCallingMethods.SelectingRadiobutton("//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']", "serviceType", GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"));
            //Validating Service Type Radiobutton selection
            GlobalDefinitions.RadiobuttonValidation("ServiceTypeRadioButtons", "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']", "serviceType", GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"));
            
            // Select the Location type radio button
            HelperCallingMethods.SelectingRadiobutton("//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']", "locationType", GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"));
            //Validating Location Type Radiobutton selection
            GlobalDefinitions.RadiobuttonValidation("LocationTypeRadioButtons", "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']", "locationType", GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"));
            
            //Selecting date and time
            HelperCallingMethods helperCallingMethods = new HelperCallingMethods();
            helperCallingMethods.SelectingDateAndTime();
            //Validating Start Date, End Date, Start Time,End Time
            GlobalDefinitions.TextDataFieldValidation("Start Date", DateTime.Today.ToString("dd/MM/yyyy"), DateTime.Parse(StartDateDropDown.GetAttribute("value")).ToString("dd/MM/yyyy"));
            GlobalDefinitions.TextDataFieldValidation("End Date", DateTime.Today.AddDays(14).ToString("dd/MM/yyyy"), DateTime.Parse(EndDateDropDown.GetAttribute("value")).ToString("dd/MM/yyyy"));
            GlobalDefinitions.TextDataFieldValidation("Start Time", GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"), DateTime.Parse(StartTime.GetAttribute("value")).ToString("hh:mmtt"));
            GlobalDefinitions.TextDataFieldValidation("End Time", GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"), DateTime.Parse(EndTime.GetAttribute("value")).ToString("hh:mmtt"));


            //Selecting SkillTrade or Credit radio button
            HelperCallingMethods.SelectingRadiobutton("//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']", "skillTrades", GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade"));
            string SkillTradeValue = GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade");

            //Enter Skill-Exchange or Credit
            if (SkillTradeValue == "Skill-exchange")
            {
                SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange") + "\n");
                //Validating entered Skill exchange value
                GlobalDefinitions.TextDataFieldValidation("SkillExchange", GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange"), SkillExchangeTag.Text.Remove(SkillExchangeTag.Text.Length - 1, 1));
            }
            else if(SkillTradeValue == "Credit") 
            {
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
                //Validating entered Credit value
                GlobalDefinitions.TextDataFieldValidation("Credit", GlobalDefinitions.ExcelLib.ReadData(2, "Credit"), CreditAmount.GetAttribute("value"));
            }

            //Uploading file for Work Sample
            WorkSamples.Click();
            AutoItX3 autoIt = new AutoItX3();
            autoIt.WinWait("Open", "File Upload", 1);
            autoIt.WinActivate("Open", "File Upload");
            autoIt.ControlFocus("Open", "File Upload", "[CLASS:Edit; INSTANCE:1]");
            autoIt.Sleep(1000);
            //autoIt.Send(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory()) + "\\FileUploadTest.txt"));
            autoIt.Send(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\FileUploadTest.txt"));
            autoIt.Sleep(1000);
            autoIt.Send("{ENTER}");
            autoIt.Sleep(2000);
            string[] FileSplitText = WorkSampleFileName.Text.Split('.');
            string FileSplitTextName = FileSplitText[0];
            
            //Validating uploaded worksample
            GlobalDefinitions.TextDataFieldValidation("WorkSamples", FileSplitTextName.ToString(), GlobalDefinitions.ExcelLib.ReadData(2, "FileName").ToString());

            //selecting Active radio button  
            HelperCallingMethods.SelectingRadiobutton("//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']", "isActive", GlobalDefinitions.ExcelLib.ReadData(2, "Active"));
            //Validating Active radio button selection
            GlobalDefinitions.RadiobuttonValidation("Active", "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']", "isActive", GlobalDefinitions.ExcelLib.ReadData(2, "Active"));
            
            //Clicking Save button
            Save.Click();

            string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Share skill added"); 
         
        }

        internal void AddShareSkill()
        {


            //Clicking on Share Skill Button
            ClickShareSkillButton();

            //Calling EnterShareSkill Method to enter Share Skill data
            EnterShareSkill();

        }

        //Validating added share skill
        internal void ValidatingAddedShareSkill()
        {
            Thread.Sleep(2000);
            ManageListings manageListings = new ManageListings();
            int MatchingRecordFoundAfterAdding = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            
            try
            {
                if (MatchingRecordFoundAfterAdding > 0)
                {
                    Base.test.Log(LogStatus.Pass, "Added a Share Skill Successfully");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Adding a Share Skill is unsuccessful" + " " + "Screenshot Image " + GlobalDefinitions.SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "ShareSkillScreenshot"));
                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Adding a Share Skill is unsuccessful", e.Message);
            }
        }

        internal void EditShareSkillData()
        {
            //Calling wait method
            GenericWait.ElementExists(GlobalDefinitions.driver, "Name", "title", 8);

            //Clearing the "Title"
            Title.Clear();

            //Entering the updated "Title"
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(4, "Title"));
            //Validating Title
            GlobalDefinitions.TextDataFieldValidation("Title", GlobalDefinitions.ExcelLib.ReadData(2, "Title"), Title.GetAttribute("value"));

            //Clearing the "Description"
            Description.Clear();
          
            //Entering the updated "Description"
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(4, "Description"));
            //Validating Description
            GlobalDefinitions.TextDataFieldValidation("Description", GlobalDefinitions.ExcelLib.ReadData(4, "Description"), Description.Text);

            // Select the Service type radio button
            HelperCallingMethods.SelectingRadiobutton("//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']", "serviceType", GlobalDefinitions.ExcelLib.ReadData(4, "ServiceType"));
            //Validating Service Type Radiobutton selection
            GlobalDefinitions.RadiobuttonValidation("ServiceTypeRadioButtons", "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']", "serviceType", GlobalDefinitions.ExcelLib.ReadData(4, "ServiceType"));

            // Select the Location type radio button
            HelperCallingMethods.SelectingRadiobutton("//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']", "locationType", GlobalDefinitions.ExcelLib.ReadData(4, "LocationType"));
            //Validating Location Type Radiobutton selection
            GlobalDefinitions.RadiobuttonValidation("LocationTypeRadioButtons", "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']", "locationType", GlobalDefinitions.ExcelLib.ReadData(4, "LocationType"));


        }



    }


}











       

