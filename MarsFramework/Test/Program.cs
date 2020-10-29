using MarsFramework.Config;
using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
using System;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium;

namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class User : Global.Base
        {


            [Test, Order(11)]
            public void AddShareSkill()
            {

                test = extent.StartTest("Add Share Skills Details");
                //Populating excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "ShareSkill");
                
                //Calling Add share skill method
                ShareSkill shareSkill = new ShareSkill();
                shareSkill.AddShareSkill();
              
            }

            [Test, Order(12)]
            public void EditShareSkill()
            {

                test = extent.StartTest("Edit Share skills Details");
                //Populating excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "ShareSkill");
                
                //Calling Edit share skill method
                ManageListings manageListings = new ManageListings();
                manageListings.EditShareSkill(GlobalDefinitions.ExcelLib.ReadData(2, "Category"),GlobalDefinitions.ExcelLib.ReadData(2,"Title"),GlobalDefinitions.ExcelLib.ReadData(2,"Description"));
                
               
            }
            [Test, Order(13)]
            public void TestDeleteShareSkill()
            {

                test = extent.StartTest("Delete Share skills Details");
                //Populating excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageShareSkill, "ManageListings");
                
                //Calling Delete share skill method
                ManageListings manageListings = new ManageListings();
                manageListings.DeleteShareSkill(GlobalDefinitions.ExcelLib.ReadData(2,"Category"), GlobalDefinitions.ExcelLib.ReadData(2,"Title"),GlobalDefinitions.ExcelLib.ReadData(2,"Description"));
                
            }

            [Test, Order(2)]
            public void TestProfileAddDescription()
            {
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Profile Description");
                test = extent.StartTest("Add profile description");
                Profile profile = new Profile();
                profile.AddDescription();
                profile.ValidateDescription();

            }

            [Test, Order(3)]
            public void TestChangePassword()
            {
                test = extent.StartTest("Change Password");
                //GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ChangePassword");
                SignIn signIn = new SignIn();
                signIn.ChangePassword();
                signIn.ValidateChangedPassword();

            }

            [Test, Order(1)]
            public void TestProfileDetailAvailability()
            {
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathAddShareSkill, "Availability");
                test = extent.StartTest("Profile Availability Detail");
                ProfileDetailAvailability profileDetailAvailability = new ProfileDetailAvailability();
                profileDetailAvailability.SelectAvailabilityType();
                profileDetailAvailability.ValidateAvailabilityType();
                profileDetailAvailability.SelectAvailabilityHour();
                profileDetailAvailability.ValidateAvailabilityHours();
                profileDetailAvailability.SelectAvailabilityTarget();
                profileDetailAvailability.ValidateAvailabilityTarget();
            }

            [Test, Order(4)]
            public void TestProfileLanguage()
            {
                test = extent.StartTest("Add/Update/Delete Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");
                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.AddLanguage();
                profileLanguage.ValidateAddedLanguage();
                profileLanguage.DeleteLanguage();
                profileLanguage.ValidateDeletedLanguage();
                profileLanguage.UpdateAddedLanguage();
                profileLanguage.ValidateUpdatedLanguage();
                
            }
           /* [Test]
            public void ValidateAddLang()
            {
                test = extent.StartTest("Validate Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");

                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.ValidateAddedLanguage();
            }*/

            

            /*[Test]
            public void TestUpdateLanguage()
            {
                test = extent.StartTest("Update Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");

                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.UpdateAddedLanguage();
            }*/

            /*[Test]
            public void TestValidateUpdatedLanguage()
            {
                test = extent.StartTest("Update Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");

                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.ValidateUpdatedLanguage();
            }*/

           /* [Test]
            public void TestDeleteLanguage()
            {
                test = extent.StartTest("Delete Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");
                
                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.DeleteLanguage();
            }*/

           /* [Test]
            public void TestValidateDeletedLanguage()
            {
                test = extent.StartTest("Validate Deleted Language");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Language");

                ProfileLanguage profileLanguage = new ProfileLanguage();
                profileLanguage.ValidateDeletedLanguage();
            }*/

            [Test, Order(5)]
            public void TestProfileSkillDetail()
            {
                test = extent.StartTest("Skills Add/update/delete detail");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");
                ProfileSkills profileSkill = new ProfileSkills();
                profileSkill.NavigateToSkillsTab();
                profileSkill.AddSkills();
                profileSkill.ValidateAddedSkills();
                profileSkill.DeleteSkill();
                profileSkill.ValidateDeletedSkill();
                profileSkill.UpdateAddedSkill();
                profileSkill.ValidateUpdatedSkill();
             
            }

            [Test, Order(6)]
            public void TestProfileEducation()
            {
                test = extent.StartTest("Add/Update/Delete education details");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                ProfileEducation profileEducation = new ProfileEducation();
                profileEducation.NavigateToEducationPage();
                profileEducation.AddEducation();
                profileEducation.ValidateAddedEducation();
                profileEducation.DeleteEducation();
                profileEducation.ValidateDeletedEducation();
                profileEducation.UpdateAddedEducation();
                profileEducation.ValidateUpdatedEducation();
            }

            /* [Test]
             public void TestValidateAddedSkills()
             {
                 test = extent.StartTest("Validate added skills");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

                 ProfileSkills profileSkill = new ProfileSkills();
                 profileSkill.NavigateToSkillsTab();
                 profileSkill.ValidateAddedSkills();
             }
             [Test]
             public void TestUpdateAddedSkills()
             {
                 test = extent.StartTest("Update added skills");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

                 ProfileSkills profileSkill = new ProfileSkills();
                 profileSkill.NavigateToSkillsTab();
                 profileSkill.UpdateAddedSkill();
             }

             [Test]
             public void TestValidateUpdatedAddedSkills()
             {
                 test = extent.StartTest("Validate Updated added skills");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

                 ProfileSkills profileSkill = new ProfileSkills();
                 profileSkill.NavigateToSkillsTab();
                 profileSkill.ValidateUpdatedSkill();
             }

             [Test]
             public void TestDeleteAddedSkills()
             {
                 test = extent.StartTest("Delete and Validate added skills");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Skill");

                 ProfileSkills profileSkill = new ProfileSkills();
                 profileSkill.NavigateToSkillsTab();
                 profileSkill.DeleteSkill();
                 profileSkill.ValidateDeletedSkill();
             }*
             [Test]
             public void AddEducation()
             {
                 test = extent.StartTest("Add education");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                 ProfileEducation profileEducation = new ProfileEducation();
                 profileEducation.NavigateToEducationPage();
                 profileEducation.AddEducation();
                 profileEducation.ValidateAddedEducation();
                 profileEducation.DeleteEducation();
                 profileEducation.ValidateDeletedEducation();
                 profileEducation.UpdateAddedEducation();
                 profileEducation.ValidateUpdatedEducation();
             }
 /*
             [Test]
             public void TestValidateAddedEducation()
             {
                 test = extent.StartTest("Validate added education");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                 ProfileEducation profileEducation = new ProfileEducation();
                 profileEducation.NavigateToEducationPage();
                 profileEducation.ValidateAddedEducation();
             }
             [Test]
             public void TestDeleteEducation()
             {
                 test = extent.StartTest("Delete education");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                 ProfileEducation profileEducation = new ProfileEducation();
                 profileEducation.NavigateToEducationPage();
                 profileEducation.DeleteEducation();
             }

             [Test]
             public void TestUpdateEducation()
             {
                 test = extent.StartTest("Update education");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                 ProfileEducation profileEducation = new ProfileEducation();
                 profileEducation.NavigateToEducationPage();
                 profileEducation.UpdateAddedEducation();
                 profileEducation.ValidateUpdatedEducation();
             }
             [Test]
             public void TestValidateUpdateEducation()
             {
                 test = extent.StartTest("validate education");
                 GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Education");
                 ProfileEducation profileEducation = new ProfileEducation();
                 profileEducation.NavigateToEducationPage();
                 //profileEducation.UpdateAddedEducation();
                 profileEducation.ValidateUpdatedEducation();
             }
 */
            [Test, Order(7)]
            public void TestCertification()
            {
                test = extent.StartTest("validate Certification");
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfileDetail, "Certification");
                ProfileCertifcation profileCertifcation = new ProfileCertifcation();
                profileCertifcation.NavigateToCertificationPage();
                profileCertifcation.AddCertification();
                profileCertifcation.ValidateAddedCertification();
                profileCertifcation.DeleteCertification();
                profileCertifcation.ValidateDeletedCertification();
                profileCertifcation.UpdateAddedCertification();
                profileCertifcation.ValidateUpdatedCertification();
                
            }
            [Test, Order(8)]
            public void TestSearchSkill()
            {
                test = extent.StartTest("Search Skill");
                SearchSkill searchSkill = new SearchSkill();
                searchSkill.EnterSkillIntoSearchBox("selenium");
                searchSkill.ClickOnACategory("Programming & Tech");
                searchSkill.ClickOnASubCategory("QA");
                searchSkill.VerifyCategorySubCategorySearch("Programming & Tech",4);
            }

            [Test, Order(9)]
            public void TestFilterSearchSkill()
            {
                test = extent.StartTest("Filter Search Skill");
                SearchSkill searchSkill = new SearchSkill();
                searchSkill.EnterSkillIntoSearchBox("selenium");
                searchSkill.EnterSkillIntoFilterSearchBox("java");
                searchSkill.ClickOnFilters("Onsite");
                searchSkill.VerifyResultwithFilter(498, 15 ,"Onsite");
            }


            [Test, Order(10)]
            public void TestFilterSearchSkillByUser()
            {
                test = extent.StartTest("Filter Search Skill by user");
                SearchSkill searchSkill = new SearchSkill();
                searchSkill.EnterSkillIntoSearchBox("selenium");
                searchSkill.SearchUserFromSearchUserTextBox("priyanka singh");
                searchSkill.VerifyUserServicesListed("priyanka singh");
            }


        }
    }
}