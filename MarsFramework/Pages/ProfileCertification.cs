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
    class ProfileCertifcation
    {
        //Clicking on Certification tab
        private static IWebElement certificationTab => GlobalDefinitions.driver.FindElement(By.XPath("//a[@class='item'][contains(.,'Certifications')]"));
        //Clicking on Add New Button
        private static IWebElement addNewButton_Certification => GlobalDefinitions.driver.FindElement(By.XPath("//th[contains(text(),'Certificate')]/..//div[text() = 'Add New']"));
        //Entering Certification name
        private static IWebElement CertificationName => GlobalDefinitions.driver.FindElement(By.Name("certificationName"));
        //Entering Certification from detail
        private static IWebElement CertificationFrom => GlobalDefinitions.driver.FindElement(By.Name("certificationFrom"));
        //Clicking on Add button
        private static IWebElement AddButton_Certificate => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@value,'Add')]"));
        //Selecting the Year
        private static IWebElement yearDropdown => GlobalDefinitions.driver.FindElement(By.Name("certificationYear"));
        //Clicking on the Update button
        private static IWebElement updateCertificateButton => GlobalDefinitions.driver.FindElement(By.XPath("//input[contains(@value,'Update')]"));
        //Retrieving the certification record list
        private IList<IWebElement> certificationRecords => GlobalDefinitions.driver.FindElements(By.XPath("//th[contains(text(),'Certificate')]/../../following-sibling::tbody"));
        //Certification table body
        private string certificationTableBody = "//th[contains(text(),'Certificate')]/../../following-sibling::tbody";
        //Reading number of certification detail count from the excel
        private int NumberOfCertificationDetailsToAdd => Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "NumberOfCertificationDetailsToAdd"));
        private int NumberOfCertificationDetailsFound = 0;
        //Retrieving the Certification name value to update from the excel 
        private string CertificationNameToUpdate => GlobalDefinitions.ExcelLib.ReadData(2, "CertificationNameToUpdate");
        //Retrieving the Year value to update from excel
        //private string yearToUpdate => GlobalDefinitions.ExcelLib.ReadData(2, "YearToUpdate");

        public void NavigateToCertificationPage()
        {
            // Clicking on the Education tab
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//a[@class='item'][contains(.,'Certifications')]", 3);
            certificationTab.Click();
        }
        public void AddCertification()
        {
            for (int i = 1; i <= NumberOfCertificationDetailsToAdd; i++)
            {
                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//th[contains(text(),'Certificate')]/..//div[text() = 'Add New']", 5);

                //Clicking the addNew button
                addNewButton_Certification.Click();

                //Entering the Certification name
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "certificationName", 5);

                CertificationName.SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "CertificationName"));

                //Entering the certification from detail
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "certificationFrom", 5);

                CertificationFrom.SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "CertifiedFrom"));

                //Selecting the year
                SelectElement elementYear_drpdwn = new SelectElement(yearDropdown);
                elementYear_drpdwn.SelectByText(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Year"));

                //Clicking add button 
                //GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//input[contains(@class,'ui teal button ')][contains(value,'Add')]",5);
                Thread.Sleep(3000);
                AddButton_Certificate.Click();
                string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Certification added");



            }

        }

        public void SearchAddedCertification()
        {
            //Searching the certification details
            NumberOfCertificationDetailsFound = 0;
            for (int i = 1; i <= NumberOfCertificationDetailsToAdd; i++)
            {
                for (int j = 1; j <= certificationRecords.Count; j++)
                {
                    String certificateNameValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                    String certificationFromValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                    String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                    if (certificateNameValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "CertificationName") && certificationFromValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "CertifiedFrom") && yearValue == GlobalDefinitions.ExcelLib.ReadData(i + 1, "Year"))
                    {
                        NumberOfCertificationDetailsFound++;
                        break;
                    }
                }
            }
        }
        public void ValidateAddedCertification()
        {
            //Searching the Certification details
            SearchAddedCertification();
            try
            {
                //Verifying the certification details is added in the list
                Assert.AreEqual(NumberOfCertificationDetailsToAdd, NumberOfCertificationDetailsFound);
                Base.test.Log(LogStatus.Pass, "Added Certification details found in the list");

            }

            catch (Exception e)
            {
                Assert.AreNotEqual(NumberOfCertificationDetailsToAdd, NumberOfCertificationDetailsFound);
                Base.test.Log(LogStatus.Fail, "Added Certification details is not found in the list", e.Message);
            }
        }

        public void UpdateAddedCertification()
        {
            //Reading from the excel about the certification details to update
            String CertificationNameToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "CertificationNameToUpdate");
           
            for (int j = 1; j <= certificationRecords.Count; j++)
            {
                String certificateNameValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String certificationFromValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                if (certificateNameValue == GlobalDefinitions.ExcelLib.ReadData(2, "CertificationName") && certificationFromValue == GlobalDefinitions.ExcelLib.ReadData(2, "CertifiedFrom") && yearValue == GlobalDefinitions.ExcelLib.ReadData(2, "Year"))
                {
                    GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "/tr[1]/td[4]/span[1]/i[1]")).Click();
                    Thread.Sleep(2000);

                    //Updating the Certification name
                    CertificationName.Clear();
                    CertificationName.SendKeys(CertificationNameToUpdate);

                    //Clicking add button 
                    updateCertificateButton.Click();
                }

            }

        }

        public void ValidateUpdatedCertification()
        {
            //Reading from the excel about the certification details to update
            String CertificationNameToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "CertificationNameToUpdate");

            for (int j = 1; j <= certificationRecords.Count; j++)
            {
                Thread.Sleep(2000);
                String certificateNameValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String certificationFromValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                try
                {
                    if (certificateNameValue == CertificationNameToUpdate)
                    {
                        Assert.True(true, certificateNameValue + " " + " certification updated successfully");
                        Base.test.Log(LogStatus.Pass, "Certification details updated successfully");
                        break;
                    }
                   
                }

                catch (Exception e)
                {
                    Assert.IsFalse(false, "Exception Thrown, Exception: " + " " + e.ToString());
                    Base.test.Log(LogStatus.Fail, "Certification details failed to update", e.Message);
                }
            }

        }

        public void DeleteCertification()
        {
            //Reading from the excel about the certification details to delete 
            String CertificationNameToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "CertificationNameToDelete");
            String CertifiedFromToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "CertifiedFromToDelete");
            //String yearToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "YearToDelete");

            for (int j = 1; j <= certificationRecords.Count; j++)
            {
                //String countryValue = GlobalDefinitions.driver.FindElement(By.XPath(educationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String certificateNameValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[1]")).Text;
                String certificationFromValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[2]")).Text;
                //String yearValue = GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "//tr//td[3]")).Text;
                try
                {
                    if (certificateNameValue == CertificationNameToDelete && certificationFromValue == CertifiedFromToDelete)
                    {
                        //Deleting the particular certication detail
                        GlobalDefinitions.driver.FindElement(By.XPath(certificationTableBody + "[" + j + "]" + "/tr[1]/td[4]/span[2]/i[1]")).Click();
                        Thread.Sleep(3000);
                        GenericWait.ElementIsVisible(GlobalDefinitions.driver, "ClassName", "ns-box-inner", 7);

                        String DeleteAlertPopupText = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner")).Text;
                        Assert.IsTrue(DeleteAlertPopupText.Contains("deleted"), CertificationNameToDelete + " " + "has been deleted from the list");
                        Base.test.Log(LogStatus.Pass, "Certification details deleted from the list");
                        break;
                    }
                  
                }
                catch (Exception e)
                {
                    Assert.IsFalse(false, "Exception Thrown, Exception: " + " " + e.ToString());

                    Base.test.Log(LogStatus.Fail, "Unable to delete the education details from the list", e.Message);
                }
            }

        }

        public void ValidateDeletedCertification()
        {
            //Searching through the list of certification details available under the certification tab 
            SearchAddedCertification();
            try
            {
                //Verifying the deleted certification details is not available and deleted from the list
                Assert.AreEqual(NumberOfCertificationDetailsToAdd - 1, NumberOfCertificationDetailsFound, "Certification deletion successful");
                Base.test.Log(LogStatus.Pass, "Certification details deleted from the list");

            }

            catch (Exception e)
            {
                Assert.AreNotEqual(NumberOfCertificationDetailsToAdd - 1, NumberOfCertificationDetailsFound, "Certification deletion unsuccesful");

                Base.test.Log(LogStatus.Fail, "Certification deleted failed to delete from the list", e.Message);
            }

        }
    }



}



