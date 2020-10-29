using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
//using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium.Support.UI;

namespace MarsFramework.Pages.Helper
{
    public class HelperCallingMethods
    {
        public HelperCallingMethods()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        //Click Start date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private  IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private  IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[@class='ui container']/div[@class='listing']/form[@class='ui form']/div[7]/div[2]/div[1]")]
        private IWebElement Days { get; set; }

        //Storing the starttime
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private  IWebElement StartTime { get; set; }

        //Click on StartTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[2]/input[1]")]
        private IWebElement StartTimeDropDown { get; set; }

        //Click on EndTime dropdown
        [FindsBy(How = How.XPath, Using = "//div[3]/div[3]/input[1]")]
        private IWebElement EndTimeDropDown { get; set; }

        //Click on Share Skill button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }



        public static void SelectingDropdown(IWebElement DropDownElement, string selectBy, string DropdownValue)
        {
            SelectElement DropDown = new SelectElement(DropDownElement);
            if (selectBy.ToLower() == "SelectByValue".ToLower())
            {
                DropDown.SelectByValue(DropdownValue);
            }
            else if(selectBy.ToLower() == "SelectByText".ToLower())
            {
                DropDown.SelectByText(DropdownValue);
            }
            else if(selectBy.ToLower() == "SelectByIndex".ToLower())
            {
                DropDown.SelectByIndex(int.Parse(DropdownValue));
            }
        }

        public static void SelectingRadiobutton(string locatorValue_listWebElement, string locatorValue_RadiobtnType, string DataFromExcel_RadiobtnName)

        {
            IList<IWebElement> Radiobuttons = GlobalDefinitions.driver.FindElements(By.XPath(locatorValue_listWebElement));
            
            for (int i = 0; i <= Radiobuttons.Count; i++)

            {

                String RadioButtonText = Radiobuttons[i].Text;
                bool bValue_IsDisplayed = Radiobuttons[i].Displayed;
                
                if (bValue_IsDisplayed == true && RadioButtonText == DataFromExcel_RadiobtnName)
                {

                    Radiobuttons[i].FindElement(By.Name(locatorValue_RadiobtnType)).Click();
                    return; 
                  
                }

               
            }

           
                
        }

    
        public void SelectingDateAndTime()
        {
            //Getting the Today's date
            string StartDate = DateTime.Today.ToString("dd/MM/yyyy"); 
            
            //Entering the Start Date
            StartDateDropDown.SendKeys(StartDate);

            //Setting the End date as today's date plus 14days
            string EndDate = DateTime.Today.AddDays(14).ToString("dd/MM/yyyy");
            
            //Entering the End Date
            EndDateDropDown.SendKeys(EndDate);
            string EndDateValueFromApp = DateTime.Parse(EndDateDropDown.GetAttribute("value")).ToString("dd/MM/yyyy");
           
            int countStartTime = GlobalDefinitions.driver.FindElements(By.XPath("//div[@class= 'four wide field']/input[@name = 'StartTime']")).Count;
            int countEndTime = GlobalDefinitions.driver.FindElements(By.XPath("//div[@class= 'four wide field']/input[@name = 'EndTime']")).Count;
            int countWeekdaysCheckbox = GlobalDefinitions.driver.FindElements(By.XPath("//div[@class='ui checkbox']/input[@name = 'Available']")).Count;
            
            // Loop for no. of days available,Start time and End time	
            try
            {

                for (int count = 2; count < countWeekdaysCheckbox; count++)
                {
                    IWebElement StartTime = GlobalDefinitions.driver.FindElement(By.XPath("//div[" + count + "]/div[2]/input"));
                    IWebElement EndTime = GlobalDefinitions.driver.FindElement(By.XPath("//div[" + count + "]/div[3]/input"));
                    IWebElement WeekDaysCheckbox = GlobalDefinitions.driver.FindElement(By.XPath("//div[" + count + "]/div[1]/div/input"));
                    string WeekDaysName = GlobalDefinitions.driver.FindElement(By.XPath("//div[" + count + "]/div[1]/div/label")).Text;

                    //Verifying if weekdays name is same as the weekdays data from excel
                    if (WeekDaysName == GlobalDefinitions.ExcelLib.ReadData(2, "Selectday"))
                    {
                        WeekDaysCheckbox.Click();
                        if (WeekDaysCheckbox.Selected == true)
                        {
                            Base.test.Log(LogStatus.Pass, WeekDaysName + " " + "checkbox is selected successfully");
                        }
                        else
                        {
                            Base.test.Log(LogStatus.Pass, WeekDaysName + " " + "checkbox is not selected successfully");
                        }
                        
                        //Entering the Start Time
                        StartTime.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime"));
                        string StartTimeValueFromApp = DateTime.Parse(StartTime.GetAttribute("value")).ToString("hh:mmtt");
                        StartTime.SendKeys(Keys.Tab);

                        //Entering the End Time
                        EndTime.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime"));
                        break;
                    } 
                }
              
            }
            catch(Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Exception found for Day and Time", e.Message);
            }
            

             

        }


    }
}










        
    



   