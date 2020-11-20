using MarsFramework.Global;
using NUnit.Framework;
using NUnit.VisualStudio.TestAdapter;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Threading;
//using OpenQA.Selenium.Support.PageObjects;

namespace MarsFramework.Pages
{
    class SignUp
    {
        public SignUp()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region  Initialize Web Elements 
        //Finding the Join 
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Join')]")]
        private IWebElement Join { get; set; }

        //Identify FirstName Textbox
        [FindsBy(How = How.Name, Using = "firstName")]
        private IWebElement FirstName { get; set; }

        //Identify LastName Textbox
        [FindsBy(How = How.Name, Using = "lastName")]
        private IWebElement LastName { get; set; }

        //Identify Email Textbox
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email { get; set; }

        //Identify Password Textbox
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password { get; set; }

        //Identify Confirm Password Textbox
        [FindsBy(How = How.Name, Using = "confirmPassword")]
        private IWebElement ConfirmPassword { get; set; }

        //Identify Term and Conditions Checkbox
        [FindsBy(How = How.Name, Using = "terms")]
        private IWebElement Checkbox { get; set; }

        //Identify join button
        [FindsBy(How = How.Id, Using = "submit-btn")]
        private IWebElement JoinBtn { get; set; }

        //Finding the Sign Link
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
        private IWebElement SignInLink { get; set; }

        // Finding the Email Field
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement EmailAfterSignUp { get; set; }

        //Finding the Password Field
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement PasswordAfterSignUp { get; set; }

        //Finding the Login Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        private IWebElement LoginBtnAfterSignUp { get; set; }

        //User welcome link
        [FindsBy(How = How.XPath, Using = "//div[@class ='ui compact menu']//span[@class = 'item ui dropdown link '][contains(text(),'Hi')]")]
        private IWebElement UserWelcomeLink { get; set; }
        #endregion

        internal void ClickJoinLink()
        {
            //Click on Join link in application login page
            Join.Click();
        }

        internal void ClickJoinButton()
        {
            //Click on join button to Sign Up
            JoinBtn.Click();
           
        }

        internal void register()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignUp");
            
            //Enter FirstName
            FirstName.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "FirstName"));

            //Enter LastName
            LastName.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "LastName"));

            //Enter Email
            Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Email"));

            //Enter Password
            Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));

            //Enter Password again to confirm
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "ConfirmPswd"));

            //Click on Checkbox
            Checkbox.Click();
                       
        }

        internal void ValidateSuccessfulRegistration()
        {
            try
            {
                //Sign In steps with registered account
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "RegistrationSignIn");
                string Username = GlobalDefinitions.ExcelLib.ReadData(2, "Username");
                string Pwd = GlobalDefinitions.ExcelLib.ReadData(2, "Password");
                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//a[contains(text(),'Sign')]", 7);
                Thread.Sleep(3000);
                SignInLink.Click();

                EmailAfterSignUp.SendKeys(Username);
                PasswordAfterSignUp.SendKeys(Pwd);
                LoginBtnAfterSignUp.Click();

                //string actualUrl = GlobalDefinitions.driver.Url;
                //string expectedUrl = "http://localhost:5000/Account/Profile";
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "SignUp");

                Thread.Sleep(2000);
                string FirstName = GlobalDefinitions.ExcelLib.ReadData(2, "FirstName");
                string actualUsernameLogin = UserWelcomeLink.Text.ToLower();
                string expectedUsernameLogin = "Hi".ToLower() + " " + FirstName.ToLower();

                Thread.Sleep(2000);
                //Assert.Multiple(() =>
                //{ 
                   // Assert.That(expectedUrl, Is.EqualTo(actualUrl));
                    Assert.That(expectedUsernameLogin, Is.EqualTo(actualUsernameLogin));
                    Thread.Sleep(2000);
                //});
                
                Base.test.Log(LogStatus.Pass, "Registration successful");
            }
             //Base.test.Log(LogStatus.Pass, "Registration successful");
            catch (Exception e)
            {
                Assert.Fail("Unsuccessful SignUp", e.Message);
                Base.test.Log(LogStatus.Fail, "Registration", e.Message);
            }
        }
    }
}
