using MarsFramework.Config;
using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using TechTalk.SpecFlow;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    [Binding]
    class Base
    {
        #region To access Path from resource file

        public static int Browser = Int32.Parse(MarsResource.Browser);
        public static String ExcelPath = MarsResource.ExcelPath_Login;
        public static String ExcelPathAddShareSkill = MarsResource.ExcelPath_AddSkills;
        public static string ExcelPathManageShareSkill = MarsResource.ExcelPath_ManageSkills;
        public static string ExcelPathSearchSkill = MarsResource.ExcelPath_SearchSkill;
        public static string ExcelPathProfileDetail = MarsResource.ExcelPath_ProfileDetail;
        public static string ScreenshotPath = MarsResource.ScreenShotPath;
        public static string ReportPath = MarsResource.ReportPath;

        #endregion

        #region reports
        public static ExtentTest test;
        public static ExtentReports extent;
        #endregion

        #region setup and tear down
        //[OneTimeSetUp]
        //[BeforeScenario]
        [BeforeTestRun]
        public static void Inititalize()
        {

            // advisasble to read this documentation before proceeding http://extentreports.relevantcodes.com/net/
            switch (Browser)
            {

                case 1:
                    GlobalDefinitions.driver = new FirefoxDriver();
                    break;
                case 2:
                    GlobalDefinitions.driver = new ChromeDriver();
                    GlobalDefinitions.driver.Manage().Window.Maximize();
                    break;

            }

            #region Initialise Reports

            extent = new ExtentReports(ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(MarsResource.ReportXMLPath);

            #endregion

            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                loginobj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.register();
            }

        }


        //[OneTimeTearDown]
        // [AfterScenario]
        [AfterTestRun]
        public static void TearDown()
        {
            // Screenshot
            
           // String img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "ShareSkillScreenshot");
            extent.EndTest(test);

            // calling Flush writes everything to the log file (Reports)
            extent.Flush();

            // Close the driver :)            
            GlobalDefinitions.driver.Close();
            GlobalDefinitions.driver.Quit();
        }
        #endregion

    }
}