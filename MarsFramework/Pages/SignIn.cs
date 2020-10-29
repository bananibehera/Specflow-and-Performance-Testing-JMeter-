using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;
using MarsFramework.Pages.Helper;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    class SignIn
    {
        public SignIn()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region  Initialize Web Elements 
        //Finding the Sign Link
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
        private IWebElement SignInLink { get; set; }

        // Finding the Email Field
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email { get; set; }

        //Finding the Password Field
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password { get; set; }

        //Finding the Login Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        private IWebElement LoginBtn { get; set; }

        [FindsBy(How = How.LinkText, Using = "Change Password")]
        private IWebElement ChangePasswordLink { get; set; }

        [FindsBy(How = How.Name, Using = "oldPassword")]
        private IWebElement CurrentPasswordTextbox { get; set; }

        [FindsBy(How = How.Name, Using = "newPassword")]
        private IWebElement NewPasswordTextbox { get; set; }

        [FindsBy(How = How.Name, Using = "confirmPassword")]
        private IWebElement ConfirmPasswordTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='button'][contains(.,'Save')]")]
        private IWebElement SaveButton { get; set; }

        //Finding the Sign Out Button
        [FindsBy(How = How.XPath, Using = "//button[text()='Sign Out']")]
        private IWebElement SignOutBtn { get; set; }

        //User welcome link
        [FindsBy(How = How.XPath, Using = "//div[@class ='ui compact menu']//span[@class = 'item ui dropdown link '][contains(text(),'Hi')]")]
        private IWebElement UserWelcomeLink { get; set; }


        #endregion

        internal void LoginSteps()
        {
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignIn");
            String BaseUrl = GlobalDefinitions.ExcelLib.ReadData(3, "Url");
            String Username = GlobalDefinitions.ExcelLib.ReadData(3, "Username");
            String Pwd = GlobalDefinitions.ExcelLib.ReadData(3, "Password");

            GlobalDefinitions.driver.Navigate().GoToUrl(BaseUrl);

            SignInLink.Click();
            Email.SendKeys(Username);
            Password.SendKeys(Pwd);
            LoginBtn.Click();

        }

        internal void SignOutSteps()
        {
            SignOutBtn.Click();

        }

        //
        internal void ChangePassword()
        {
            //Retrieving new password, confirm password and current passowrd value from excel
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ChangePassword");
            String CurrentPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "Password");
            String NewPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "NewPassword");
            String ConfirmPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "ConfirmPassword");

            //Moving curser to Userwelcom link
            Actions actions = new Actions(GlobalDefinitions.driver);
            Thread.Sleep(4000);
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Xpath", "//span[contains(@class,'item ui dropdown link')]", 3);
            actions.MoveToElement(UserWelcomeLink).Build().Perform();

            //Clicking on the change password link
            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "LinkText", "Change Password", 3);
            Thread.Sleep(2000);
            ChangePasswordLink.Click();

            //Entering the current password
            CurrentPasswordTextbox.SendKeys(CurrentPasswordValue);

            //Entering the new password
            NewPasswordTextbox.SendKeys(NewPasswordValue);

            //Entering the confirm password
            ConfirmPasswordTextbox.SendKeys(ConfirmPasswordValue);

            //clicking on the save button
            SaveButton.Click();

            string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Change Password");

            //Validating message
            GlobalDefinitions.MessageValidation("Password Changed Successfully");

        }

        //Validate password has been changed 
        public void ValidateChangedPassword()
        {
            try
            {

                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignIn");
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//button[text()='Sign Out']", 6);
                //Click on the signout button
                SignOutBtn.Click();

                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//a[contains(text(),'Sign')]", 6);
                //Clicking on SignIn link
                SignInLink.Click();

                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "email", 6);
                //Enter the username
                Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(3, "Username"));

                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "password", 6);
                //Enter the new password
                Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(3, "New Password"));

                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//button[contains(text(),'Login')]", 6);
                //Click on the Login button
                LoginBtn.Click();

                //Validating user succesfully login after with the changed password 
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//div[@class ='ui compact menu']//span[@class = 'item ui dropdown link '][contains(text(),'Hi')]", 6);

                if (UserWelcomeLink.Displayed)
                {
                    Assert.IsTrue(true);
                    Base.test.Log(LogStatus.Pass, "User new password has been changed successfully");

                }
                else
                {
                    Assert.IsTrue(false, "User new password has failed to update");
                    Base.test.Log(LogStatus.Fail, "User new password has failed to update");

                }
            }
            catch(Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Exception found For Change Password", e.Message);
            }

            /*Resetting the password*/
            //Retreiving passwords values from the excel
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ChangePassword");
            String CurrentPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "NewPassword");
            String NewPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "Password");
            String ConfirmPasswordValue = GlobalDefinitions.ExcelLib.ReadData(2, "Password");
            
            //Clicking on User welcome name link
            UserWelcomeLink.Click();
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "LinkText", "Change Password", 5);
            
            //Clicking on change password link
            ChangePasswordLink.Click();

            //Entering the current password
            CurrentPasswordTextbox.SendKeys(CurrentPasswordValue);

            //Entering the new password password
            NewPasswordTextbox.SendKeys(NewPasswordValue);

            //Entering the confirm password
            ConfirmPasswordTextbox.SendKeys(ConfirmPasswordValue);

            //Clicking on the save button
            SaveButton.Click();

        }
    }
}