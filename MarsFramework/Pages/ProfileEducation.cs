using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    class ProfileEducation
    {
        #region Initializing web elements
        //Clicking on Education tab
        private static IWebElement educationTab => GlobalDefinitions.driver.FindElement(By.XPath("//a[@class='item'][contains(.,'Education')]"));
        //Clicking on Add New Button
        private static IWebElement addNewButton_Education => GlobalDefinitions.driver.FindElement(By.XPath("//th[contains(text(),'Degree')]/..//div[text() = 'Add New']"));  
        //Entering College/University name
        private static IWebElement collegeNameTextbox => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@placeholder,'College/University Name')]"));
        //Entering Degree
        private static IWebElement degreeTextbox => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@placeholder,'Degree')]"));
        //Clicking on Add button
        private static IWebElement addButton_Education => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@class,'ui teal button ')]"));
        //Selecting the Country
        private static IWebElement countryDropdown => GlobalDefinitions.driver.FindElement(By.XPath("//select[contains(@name,'country')]"));
        //Selecting the Title
        private static IWebElement titleDropdown => GlobalDefinitions.driver.FindElement(By.XPath("//select[contains(@name,'title')]"));
        //Selecting the Year
        private static IWebElement yearDropdown => GlobalDefinitions.driver.FindElement(By.XPath("//select[contains(@name,'yearOfGraduation')]"));
        //Clicking the edit button
        private static IWebElement editEducationButton => GlobalDefinitions.driver.FindElement(By.XPath("(//i[contains(@class,'outline write icon')])[5]"));
        //Clicking on the Update button
        private static IWebElement updateEducationButton => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@value,'Update')]"));
        //Retrieving the educations record list
        private IList<IWebElement> educationRecords => GlobalDefinitions.driver.FindElements(By.XPath("//th[contains(text(),'Degree')]/../../following-sibling::tbody"));
        //Education table body
        private string educationTableBody = "//th[contains(text(),'Degree')]/../../following-sibling::tbody";
        //Reading number of education detail count from the excel
        private int NumberOfEducationDetailsToAdd => Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "NumberOfEducationDetailsToAdd"));
        private int NumberOfEducationDetailsFound = 0;
        //Retrieving the Title value to update from the excel 
        private string titleToUpdate => GlobalDefinitions.ExcelLib.ReadData(2, "TitleToUpdate");
        //Retrieving the Year value to update from excel
        private string yearToUpdate => GlobalDefinitions.ExcelLib.ReadData(2, "YearToUpdate");
        #endregion

        public void NavigateToEducationPage()
        {
            // Clicking on the Education tab
            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//a[@class='item'][contains(.,'Education')]", 3);
            educationTab.Click();
        }
        public void AddEducation()
        {
            for (int i = 1; i <= NumberOfEducationDetailsToAdd; i++)
            {
                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//th[contains(text(),'Degree')]/..//div[text() = 'Add New']", 5);

                //Clicking the addNew button
                addNewButton_Education.Click();

                //Entering the College/University name
                collegeNameTextbox.SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "College/University Name"));

                //Entering the degree
                degreeTextbox.SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Degree"));

                //Selecting the country
                SelectElement elementcountry_drpdwn = new SelectElement(countryDropdown);
                elementcountry_drpdwn.SelectByText(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Country"));


                //Selecting the Title
                SelectElement elementtitle_drpdwn = new SelectElement(titleDropdown);
                elementtitle_drpdwn.SelectByText(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Title"));

                //Selecting the year
                SelectElement elementyear_drpdwn = new SelectElement(yearDropdown);
                elementyear_drpdwn.SelectByText(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Year"));

                //Clicking add button 
                addButton_Education.Click();
                string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Education added");


            }

        }

        public void SearchAddedEducation()
        {
            //Searching the education details
            NumberOfEducationDetailsFound = 0;
            for (int i = 1; i <= NumberOfEducationDetailsToAdd; i++)
            {
                for (int j = 1; j <= educationRecords.Count; j++)
                {
                    String countryValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                    String universityValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                    String titleValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                    String degreeValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[4]")).Text;
                    String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[5]")).Text;
                    if (countryValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "Country") && universityValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "College/University Name") && titleValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "Title") && degreeValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "Degree") && yearValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "Year"))
                    {
                        NumberOfEducationDetailsFound++;
                        break;
                    }
                }
            }
        }
        public void ValidateAddedEducation()
        {
            //Searching the Education details
            SearchAddedEducation();
            try
            {
                //Verifying the education details is added in the list
                Assert.AreEqual(NumberOfEducationDetailsToAdd, NumberOfEducationDetailsFound);
                Base.test.Log(LogStatus.Pass, "Added Education details found in the list");

            }

            catch (Exception e)
            {
                Assert.AreNotEqual(NumberOfEducationDetailsToAdd, NumberOfEducationDetailsFound);
                Base.test.Log(LogStatus.Fail, "Added Education details is not found in the list", e.Message);
            }
        }

        public void UpdateAddedEducation()
        {
            //Reading from the excel about the education details to update
            String degreeToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "DegreeToUpdate");
            String titleToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "TitleToUpdate");
            String yearToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "YearToUpdate");

            for (int i = 1; i <= educationRecords.Count; i++)
            {
                String countryValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + i + "]" + "//tr//td[1]")).Text;
                String titleValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + i + "]" + "//tr//td[3]")).Text;
                String degreeValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + i + "]" + "//tr//td[4]")).Text;
                String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + i + "]" + "//tr//td[5]")).Text;
                
                if (countryValue == GlobalDefinitions.ExcelLib.ReadData(3, "Country") && degreeValue == GlobalDefinitions.ExcelLib.ReadData(3, "Degree") && titleValue == GlobalDefinitions.ExcelLib.ReadData(3, "Title") && yearValue == GlobalDefinitions.ExcelLib.ReadData(3, "Year"))
                {
                    GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + i + "]" + "/tr/td[6]/span[1]/i")).Click();
                    Thread.Sleep(2000);

                    //Updating the degree
                    degreeTextbox.Clear();
                    degreeTextbox.SendKeys(degreeToUpdate);

                    //Updating the Title
                    SelectElement elementtitle_drpdwn = new SelectElement(titleDropdown);
                    elementtitle_drpdwn.SelectByText(titleToUpdate);

                    //Updating the year
                    SelectElement elementyear_drpdwn = new SelectElement(yearDropdown);
                    elementyear_drpdwn.SelectByText(yearToUpdate);

                    //Clicking add button 
                    updateEducationButton.Click();

                    string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Education Updated");

                }

            }

        }

        public void ValidateUpdatedEducation()
        {
            //Reading from the excel about the education details to delete 
            String degreeToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "DegreeToUpdate");
            String titleToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "TitleToUpdate");
            String yearToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "YearToUpdate");

            for (int j = 1; j <= educationRecords.Count; j++)
            {
                //String countryValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                Thread.Sleep(2000);
                String titleValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                String degreeValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[4]")).Text;
                String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[5]")).Text;
                try
                {
                    if (titleValue == titleToUpdate && yearValue == yearToUpdate && degreeValue == degreeToUpdate)
                    {
                        Assert.IsTrue(true);
                        Base.test.Log(LogStatus.Pass, "The education details updated successfully");
                        
                        break;
                    }
                   
                }

                catch (Exception e)
                {
                    Assert.Fail("Education details failed to update", e.Message);
                    Base.test.Log(LogStatus.Fail, "Education details failed to update", e.Message);
                }
            }

        }

        public void DeleteEducation()
        {
                //Reading from the excel about the education details to delete 
                String countryToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "CountryToDelete");
                String titleToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "TitleToDelete");
                String yearToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "YearToDelete");


              for (int j = 1; j <= educationRecords.Count; j++)
              {
                   //Searching the education details to delete
                   String countryValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                   String titleValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                   String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[5]")).Text;
                   try
                   {
                        if (countryValue == countryToDelete && titleValue == titleToDelete && yearValue == yearToDelete)
                        {
                            //Deleting the particular education detail
                            GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "/tr/td[6]/span[2]/i")).Click();
                            Thread.Sleep(3000);    
                            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "ClassName", "ns-box-inner", 7);
                            
                            String DeleteAlertPopupText = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner")).Text;
                            Assert.IsTrue(DeleteAlertPopupText.Contains("removed"));
                            Base.test.Log(LogStatus.Pass, "Education details deleted from the list");
                            string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Education details deleted");
                            break;
                        }
                        
                   }
                   catch (Exception e)
                   {
                        Assert.Fail("Education details failed to delete", e.Message);
                        Base.test.Log(LogStatus.Fail, "Unable to delete the education details from the list", e.Message);
                   }
              }
            
        }

        public void ValidateDeletedEducation()
        {
            //Searching through the list of education details available under the education tab 
            SearchAddedEducation();
            try
            {
                //Verifying the deleted education details is not available and deleted from the list
                Assert.AreEqual(NumberOfEducationDetailsToAdd - 1, NumberOfEducationDetailsFound);
                Base.test.Log(LogStatus.Pass, "Education details deleted from the list");

            }

            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Education deleted failed to delete from the list", e.Message);
            }

        }
    }


    
}
