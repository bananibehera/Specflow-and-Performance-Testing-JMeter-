using System;
using TechTalk.SpecFlow;
using MarsFramework.Pages;
using MarsFramework.Global;
using RelevantCodes.ExtentReports;
using static MarsFramework.Global.Base;

namespace MarsFramework.Test.StepDefinition
{
    [Binding]
    public class ChangePasswordSteps
    {
        [Given(@"I navigate to change password page")]
        public void GivenINavigateToChangePasswordPage()
        {
            SignIn signIn = new SignIn();
            signIn.NavigateToChangePasswordPage();
        }

        [When(@"I enter my updated password information detail and save it")]
        public void WhenIEnterMyUpdatedPasswordInformationDetailAndSaveIt()
        {
            test = extent.StartTest("Change Password");
            SignIn signIn = new SignIn();
            signIn.ChangePassword();
        }


        [When(@"I save the information")]
        public void WhenISaveTheInformation()
        {
            SignIn signIn = new SignIn();
            signIn.SaveUpdatedPasswordInfo();
        }
        
        [When(@"I click on Signout button")]
        public void WhenIClickOnSignoutButton()
        {
            SignIn signIn = new SignIn();
            signIn.SignOutSteps();
        }
        
        [Then(@"I should be able to login again with my updated password successfully")]
        public void ThenIShouldBeAbleToLoginAgainWithMyUpdatedPasswordSuccessfully()
        {
            SignIn signIn = new SignIn();
            signIn.ValidateChangedPassword();
        }
    }
}
