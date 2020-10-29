using Excel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
//using MarsFramework.Pages.Helper;
using MarsFramework.Pages;

namespace MarsFramework.Global
{
    class GlobalDefinitions
    {
        //Initialise the browser
        public static IWebDriver driver { get; set; }

        #region WaitforElement 

        public static void wait(int time)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(time);

        }

        [Obsolete]
        public static IWebElement WaitForElement(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(ExpectedConditions.ElementIsVisible(by)));
        }
        #endregion


        #region Excel 
        public class ExcelLib
        {
            static List<Datacollection> dataCol = new List<Datacollection>();

            public class Datacollection
            {
                public int rowNumber { get; set; }
                public string colName { get; set; }
                public string colValue { get; set; }
            }


            public static void ClearData()
            {
                dataCol.Clear();
            }


            private static DataTable ExcelToDataTable(string fileName, string SheetName)
            {
                // Open file and return as Stream
                using (System.IO.FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;

                        //Return as dataset
                        DataSet result = excelReader.AsDataSet();
                        //Get all the tables
                        DataTableCollection table = result.Tables;

                        // store it in data table
                        DataTable resultTable = table[SheetName];

                        //excelReader.Dispose();
                        //excelReader.Close();
                        // return
                        return resultTable;
                    }
                }
            }

            public static string ReadData(int rowNumber, string columnName)
            {
                try
                {
                    //Retriving Data using LINQ to reduce much of iterations

                    rowNumber = rowNumber - 1;
                    string data = (from colData in dataCol
                                   where colData.colName == columnName && colData.rowNumber == rowNumber
                                   select colData.colValue).SingleOrDefault();

                    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;


                    return data.ToString();
                }

                catch (Exception e)
                {
                    //Added by Kumar
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                    return null;
                }
            }

            public static void PopulateInCollection(string fileName, string SheetName)
            {
                ExcelLib.ClearData();
                DataTable table = ExcelToDataTable(fileName, SheetName);

                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = table.Columns[col].ColumnName,
                            colValue = table.Rows[row - 1][col].ToString()
                        };


                        //Add all the details for each row
                        dataCol.Add(dtTable);

                    }
                }

            }
        }

        #endregion

        #region screenshots
        public class SaveScreenShotClass
        {
            public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
            {
                var folderLocation = (Base.ScreenshotPath);

                if (!System.IO.Directory.Exists(folderLocation))
                {
                    System.IO.Directory.CreateDirectory(folderLocation);
                }

                var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = new StringBuilder(folderLocation);

                fileName.Append(ScreenShotFileName);
                fileName.Append(DateTime.Now.ToString("_dd-mm-yyyy_mss"));
                //fileName.Append(DateTime.Now.ToString("dd-mm-yyyym_ss"));
                fileName.Append(".jpeg");
                screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Jpeg);
                return fileName.ToString();
            }
        }
        #endregion

        //Validation for text fields
        public static void TextDataFieldValidation(String textFieldName, String expectedValue, String actualValue)
        {
            try
            {
                if (expectedValue.ToLower() == actualValue.ToLower())
                {
                    Base.test.Log(LogStatus.Pass, textFieldName + " is entered and displayed successfully");
                    Assert.IsTrue(true);
                }
                else
                    Base.test.Log(LogStatus.Fail, textFieldName + " is not entered and displayed successfully" + " "+ "Screenshot Image" + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "ShareSkillScreenshot"));
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Exception found For " + textFieldName, e.Message);
            }

        }

        #region Validation for dropdown
        public static void DropDownDataValidation(String dropDownFieldName, IWebElement dropDownElement, String expectedValue)
        {
            try
            {
                SelectElement dropDown = new SelectElement(dropDownElement);

                if (dropDown.SelectedOption.Text.ToLower() == expectedValue.ToLower())
                {
                    Base.test.Log(LogStatus.Pass, dropDownFieldName + " is selected successfully");
                    Assert.IsTrue(true);
                }

                else
                    Base.test.Log(LogStatus.Fail, dropDownFieldName + " Selection unsuccessful"+ " "+ "Screenshot Image " + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "DropdownScreenshot"));
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Exception found For " + dropDownElement, e.Message);
            }

        }
        #endregion

        //Validating radiobutton
        public static void RadiobuttonValidation(string radiobuttonFieldName, string locatorValue_listWebElement, string locatorValue_RadiobtnType, string expectedValue)

        {

            IList<IWebElement> Radiobuttons = GlobalDefinitions.driver.FindElements(By.XPath(locatorValue_listWebElement));

            try
            {
                for (int i = 0; i <= Radiobuttons.Count; i++)

                {

                    if (Radiobuttons[i].FindElement(By.Name(locatorValue_RadiobtnType)).Selected)
                    {
                        if (Radiobuttons[i].Text == expectedValue)
                        {
                            Base.test.Log(LogStatus.Pass, radiobuttonFieldName + " " + "radiobutton is selected successfully");
                            Assert.IsTrue(true);
                            break;
                        }

                        else
                        {
                            Base.test.Log(LogStatus.Fail, radiobuttonFieldName + " " + "radiobutton selection is unsuccessful" + " " + "Screenshot Image " + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "ShareSkillScreenshot"));
                        }

                    }
                    //break;
                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Exception found For " + radiobuttonFieldName, e.Message);
            }
            

        }

        //Message validation add/update/delete
        public static void MessageValidation(String ExpectedMessage)
        {
            try
            {
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "ClassName", "ns-box-inner", 2);
                IWebElement Message = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner"));
                String text = Message.Text;
                Assert.IsTrue(text.Contains(ExpectedMessage));
                
            }
            catch
            {
                Assert.Fail(ExpectedMessage+  "Failed");
            }
        }

    }
}
