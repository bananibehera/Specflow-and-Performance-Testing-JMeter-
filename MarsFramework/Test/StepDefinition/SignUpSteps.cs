using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;


namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class SignUpSteps
    {
        [Given(@"I click on Logout button")]
        public void GivenIClickOnLogoutButton()
        {
            SignIn signIn = new SignIn();
            signIn.SignOutSteps();

        }

        [Given(@"I click on Join link in SignIn page")]
        public void GivenIClickOnJoinLinkInSignInPage()
        {
            SignUp signUp = new SignUp();
            signUp.ClickJoinLink();

        }

        [Given(@"I entered the valid data for firstname, lastname, email and password")]
        public void GivenIEnteredTheValidDataForFirstnameLastnameEmailAndPassword()
        {
            SignUp signUp = new SignUp();
            test = extent.StartTest("Signup");
            signUp.register();

        }

        [When(@"I Click on Join button")]
        public void WhenIClickOnJoinButton()
        {
            SignUp signUp = new SignUp();
            signUp.ClickJoinButton();

        }

        [Then(@"I should be successfully signup")]
        public void ThenIShouldBeSuccessfullySignup()
        {
            SignUp signUp = new SignUp();
            signUp.ValidateSuccessfulRegistration();

        }
    }
}
