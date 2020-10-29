using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsFramework.Global;
using MarsFramework.Pages.Helper;
using System.Threading;

namespace MarsFramework.Pages
{
    class ProfileDetailAvailability
    {
        public ProfileDetailAvailability()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region Initializing web elements
        //Selecting Availability dropdown
        [FindsBy(How = How.Name, Using = "availabiltyType")]
        private IWebElement AvailabilityType { get; set; }

        [FindsBy(How = How.XPath, Using = "//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement AvailabilityTypeEditButton { get; set; }

        //Selecting Availability hour dropdown
        [FindsBy(How = How.Name, Using = "availabiltyHour")]
        private IWebElement AvailabilityHour { get; set; }

        [FindsBy(How = How.XPath, Using = "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement AvailabilityHourEditButton { get; set; }

        //Selecting Availability Target dropdown
        [FindsBy(How = How.Name, Using = "availabiltyTarget")]
        private IWebElement AvailabilityTarget { get; set; }

        [FindsBy(How = How.XPath, Using = "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement AvailabilityTargetEditButton { get; set; }
        #endregion


        public void SelectAvailabilityType()
        {

            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']", 5);
            Thread.Sleep(2000);
            string AvailabilityTypeValue = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Type");
            if (AvailabilityTypeValue == "Full Time")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityTypeEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityType, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availabilty Type"));
             
                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
            else if (AvailabilityTypeValue == "Part Time")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']", 5);
                AvailabilityTypeEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityType, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Type"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
        }
        public void ValidateAvailabilityType()
        {
            //retrieve the expected Availability Type value
            string expectedAvailabilityType = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Type");

            //retrieve the Actual Availability Type value
            string actualAvailabilityType = GlobalDefinitions.driver.FindElement(By.XPath("//strong[text()='Availability']/../..//div[@class='right floated content']")).Text;

            //Validate the selected Availability Type
            GlobalDefinitions.TextDataFieldValidation("Availability Type",expectedAvailabilityType, actualAvailabilityType);

        }

        public void SelectAvailabilityHour()
        {
            //GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']", 5);

            string AvailabilityHourValue = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Hour");
            if (AvailabilityHourValue == "Less than 30hours a week")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityHourEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityHour, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Hour"));
               

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
            else if (AvailabilityHourValue == "More than 30hours a week")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityHourEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityHour, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Hour"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }

            else if (AvailabilityHourValue == "As needed")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityHourEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityHour, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availabilty Hour"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
        }

        //Validate selected Availability Hour 
        public void ValidateAvailabilityHours()
        {

            //Retrieve the expected Availability Hour value
            string expectedAvailabilityHours = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Hour");

            //Retrieve the Actual Availability Hour value
            string actualAvailabilityHours = GlobalDefinitions.driver.FindElement(By.XPath("//strong[text()='Hours']/../..//div[@class='right floated content']/span")).Text;

            //Validate the selected Availability hour
            GlobalDefinitions.TextDataFieldValidation("Availability Hour", expectedAvailabilityHours, actualAvailabilityHours);

        }


        public void SelectAvailabilityTarget()
        {
            //GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']", 5);

            string AvailabilityTargetValue = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Target");
            if (AvailabilityTargetValue == "Less than $500 month")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityTargetEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityTarget, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Target"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
            else if (AvailabilityTargetValue == "More than $1000 per month")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityTargetEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityTarget, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Target"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }

            else if (AvailabilityTargetValue == "Between $500 and $1000 per month")
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']", 5);

                AvailabilityTargetEditButton.Click();
                HelperCallingMethods.SelectingDropdown(AvailabilityTarget, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Availability Target"));

                //Validate message
                GlobalDefinitions.MessageValidation("Availability updated");
                Thread.Sleep(2000);
            }
        }

        //Validate the selected Availability Target 
        public void ValidateAvailabilityTarget()
        {
            //Retrieve the expected Availability Target
            string expectedAvailabilityTarget = GlobalDefinitions.ExcelLib.ReadData(2, "Availability Target");

            //Retrieve the Actual Availability Target
            string actualAvailabilityTarget = GlobalDefinitions.driver.FindElement(By.XPath("//strong[text()='Earn Target']/../..//div[@class='right floated content']/span")).Text;

            //Validate the selected Availability Target
            GlobalDefinitions.TextDataFieldValidation("Availability Target",expectedAvailabilityTarget, actualAvailabilityTarget);



        }

    }
}
