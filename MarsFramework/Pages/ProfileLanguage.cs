using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    public class ProfileLanguage
    {
       
            public ProfileLanguage()
            {
                PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
            }

        #region Intializing web elements

            //Navigating to Language tab
            [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Languages')]")]
            private IWebElement LanguageTab { get; set; }

            //Add new language button
            [FindsBy(How = How.XPath, Using = "(//div[@class='ui teal button '][contains(.,'Add New')])[1]")]
            private IWebElement addNewLanguageButton { get; set; }
            
            //Add new langauge
            [FindsBy(How = How.XPath, Using = "//input[@placeholder='Add Language']")]
            private IWebElement addNewLanguageText { get; set; }
            
            //Selecting Language level
            [FindsBy(How = How.Name, Using = "level")]
            private IWebElement LanguageLevelDropdown { get; set; }
            
            //Clicking Add button
            [FindsBy(How = How.XPath, Using = "//input[@class='ui teal button'][contains(@value, 'Add')]")]
            private IWebElement addButtonLangauge { get; set; }
            
            //Clicking Update button 
            [FindsBy(How = How.XPath, Using = "//input[contains(@value,'Update')]")]
            private IWebElement updateButtonLanguage { get; set; }

            private int NumberOfLanguagesFound = 0;
            
            //Clicking Delete language button
            [FindsBy(How = How.XPath, Using = " (//i[contains(@class,'remove icon')])[1]")]
            private IWebElement deleteLanguageButton { get; set; }
            
            //Number of record list
            [FindsBy(How = How.XPath, Using = "//th[contains(text(),'Language')]/../../following-sibling::tbody")]
            private IList<IWebElement> LanguageRecords { get; set; }
            
            //Language body xpath
            private string LanguageBody => "//th[contains(text(),'Language')]/../../following-sibling::tbody";
            
            //Number of language to add
            int NumberOfLanguagesToAdd => Int32.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "NumberOfLanguageToAdd"));
        #endregion

        public void NavigateToLanguageTab()
        {
            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//a[contains(text(),'Skills')]", 2);
            LanguageTab.Click();

        }

        public void AddLanguage()
            {

                for (int i = 1; i <= NumberOfLanguagesToAdd; i++)

                {
                    //Reading language data from excel sheet
                    String languageData = GlobalDefinitions.ExcelLib.ReadData(i + 1, "Language");

                    Thread.Sleep(2000);
                    GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "(//div[@class='ui teal button '][contains(.,'Add New')])[1]", 6);
                    //Clicking on Add new button
                    addNewLanguageButton.Click();

                    //Entering the language data into the language textbox
                    //Thread.Sleep(2000);
                    GenericWait.ElementExists(GlobalDefinitions.driver, "XPath", "//input[@placeholder='Add Language']", 6);
                    addNewLanguageText.SendKeys(languageData);

                    //Selecting the language level
                    GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "level", 6);
                    SelectElement chooseLanguageLevel = new SelectElement(LanguageLevelDropdown);
                    var languageLevelData = GlobalDefinitions.ExcelLib.ReadData(i + 1, "LanguageLevel");
                    chooseLanguageLevel.SelectByValue(languageLevelData);

                    //Clicking Add button
                    //Thread.Sleep(4000);
                    GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//input[@class='ui teal button'][contains(@value, 'Add')]", 6);
                    addButtonLangauge.Click();
                    string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Add Language");
                }
            }

            public void SearchLanguage()
            {
               //Searching language 
               NumberOfLanguagesFound = 0;
                 for (int i = 1; i <= NumberOfLanguagesToAdd; i++)
                 {

                     for (int j = 1; j <= LanguageRecords.Count; j++)
                     {
                        String LanguageValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[1]")).Text;
                        String LanguageLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[2]")).Text;
                        if (LanguageValue == (GlobalDefinitions.ExcelLib.ReadData(i + 1, "Language")) && LanguageLevelValue == (GlobalDefinitions.ExcelLib.ReadData( i + 1, "LanguageLevel")))
                        {
                            NumberOfLanguagesFound++;
                            break;
                        }
                     }
                 }
            }

            public void ValidateAddedLanguage()
            {
                //Searching for the added language in the available langauge list 
                SearchLanguage();
                try
                {
                    //Verifying all the added language available in the language list
                    Assert.AreEqual(NumberOfLanguagesToAdd, NumberOfLanguagesFound);
                    Base.test.Log(LogStatus.Pass, "The language added found in the list");

                }

                catch (Exception e)
                {
                    Assert.AreNotEqual(NumberOfLanguagesToAdd, NumberOfLanguagesFound, "The language added is not found in the list");
                    Base.test.Log(LogStatus.Fail, "The language added is not found in the list", e.Message);
                }

            }


            public void UpdateAddedLanguage()
            {
                //for (int i = 1; i <= NumberOfLanguagesToAdd; i++)

                //{
                    //Reading language and updated language level from the excel 
                    String languageToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "LanguageToUpdate");
                    String languageLevelToUpdate = GlobalDefinitions.ExcelLib.ReadData(2, "LanguageLevelToUpdate");

                    //Searching through the Language list to find a language need to be updated   
                    for (int j = 1; j <= LanguageRecords.Count; j++)
                    {
                        String LanguageValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[1]")).Text;
                        String LanguageLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[2]")).Text;
                        if (LanguageValue == (GlobalDefinitions.ExcelLib.ReadData(3, "Language")) && LanguageLevelValue == (GlobalDefinitions.ExcelLib.ReadData(3, "LanguageLevel")))
                        {
                             GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//tbody" + "[" + j + "]//tr[1]//td[3]//span[1]//i[1]", 3);
                             
                             //Clicking edit button of the language needed to update
                             GlobalDefinitions.driver.FindElement(By.XPath("//tbody" + "[" + j + "]//tr[1]//td[3]//span[1]//i[1]")).Click();

                             GenericWait.ElementIsVisible(GlobalDefinitions.driver, "Name", "level", 6);
                            
                            //Selecting the updated language level
                            SelectElement chooseLanguageLevel = new SelectElement(LanguageLevelDropdown);
                            chooseLanguageLevel.SelectByValue(languageLevelToUpdate);

                            GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", "//input[contains(@value,'Update')]", 6);
                           
                            //Clicking Update button
                            updateButtonLanguage.Click();
                            string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Update Language");
                            break;
                        }
                    }
               
            }

            public void ValidateUpdatedLanguage()
            {  
                    //Searching for the language which was updated among the available language list
                    for (int j = 1; j <= LanguageRecords.Count; j++)
                    {
                        String LanguageValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[1]")).Text;
                        String LanguageLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[2]")).Text;
                        try
                        {
                            //Verifying the language is updated and found in the available available language list
                            if (LanguageValue == (GlobalDefinitions.ExcelLib.ReadData(2, "LanguageToUpdate")) && LanguageLevelValue == (GlobalDefinitions.ExcelLib.ReadData(2, "LanguageLevelToUpdate")))
                            {
                             Base.test.Log(LogStatus.Pass, "The language updated found in the list");
                             break;  
                            }
                                      
                        }
                     
                        catch (Exception e)
                        {
                            Base.test.Log(LogStatus.Fail, "The language updated is not found in the list", e.Message);
                        }
                    }
        
            }
        

            public void DeleteLanguage()
            {

                  String languageToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "LanguageToDelete");
                  String languageLevelToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "LanguageLevelToDelete");

                    //Searching for the language to delete from the language list
                    for (int j = 1; j <= LanguageRecords.Count; j++)
                    {
                        String LanguageValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[1]")).Text;
                        String LanguageLevelValue = GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "//tr//td[2]")).Text;
                        try
                        {
                            //Deleting a particular language if found from the available language list
                            if (LanguageValue == languageToDelete && LanguageLevelValue == languageLevelToDelete)
                            {
                                GenericWait.ElementIsClickable(GlobalDefinitions.driver, "XPath", LanguageBody + "[" + j + "]" + " / tr[1] / td[3] / span[2] / i[1]", 3);
                                GlobalDefinitions.driver.FindElement(By.XPath(LanguageBody + "[" + j + "]" + "/tr[1]/td[3]/span[2]/i[1]")).Click();
                                string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Delete Language");
                                String DeleteAlertPopupText = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner")).Text;
                                Assert.IsTrue(DeleteAlertPopupText.Contains("deleted"));
                                Base.test.Log(LogStatus.Pass, "The language deleted from the list");
                                break;
                            }
                          
                        }
                        catch (Exception e)
                        {
                            Base.test.Log(LogStatus.Fail, "The language is not deleted from the list", e.Message);
                        }
                    }
            }

            public void ValidateDeletedLanguage()
            {
                    //Searching through the list of languages available under the language tab 
                    SearchLanguage();
                    try
                    {
                       //Verifying the deleted language is not available and deleted from the list
                       Assert.AreEqual(NumberOfLanguagesToAdd - 1,NumberOfLanguagesFound,"Successfully language deleted");
                       Base.test.Log(LogStatus.Pass, "The language deleted from the list");

                    }

                    catch (Exception e)
                    {
                      Base.test.Log(LogStatus.Fail, "The language is not deleted from the list", e.Message);
                    }

            }

    }
}

 
