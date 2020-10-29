using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsFramework.Global;
using NUnit.Framework;
using System.Threading;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium.Interactions;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    class SearchSkill
    {
        public SearchSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        #region Intializing the web element

        [FindsBy(How = How.XPath, Using = "//input[contains(@placeholder,'Search skills')][1]")]
        private IWebElement SearchSkillsTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='active item']")]
        private IWebElement AllCategory { get; set; }

        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='twelve wide column']/div[@class='ui grid']/../div[@class = 'ui grid']/descendant::div[@class='ui stackable three cards']/div[@class='ui card']")]
        private IList<IWebElement> ResultList { get; set; }

        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='twelve wide column']/div[@class='ui grid']/../div[@class = 'ui grid']")]
        private IWebElement ResultPageMessage { get; set; }

        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='ui link list']/a[contains(@class,'item category')]")]
        private IList<IWebElement> Categories { get; set; }
       
        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='ui link list']/a[contains(@class,'item subcategory')]")]
        private IList<IWebElement> SubCategories { get; set; }

        [FindsBy(How = How.ClassName, Using = "search-results")]
        private IWebElement skillResult { get; set; }

        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='four wide column']/descendant::input[@placeholder='Search user']")]
        private IWebElement FilterSearchUser{ get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='results transition visible']/div[@class ='result']")]
        private IList<IWebElement> UserList { get; set; }

        // Filter web elements
        [FindsBy(How = How.XPath, Using = "//section[@class='search-results']/descendant::div[@class='four wide column']/descendant::input[@placeholder='Search skills']")]
        private IWebElement FilterSearchSkillsTextbox { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='ui button'][contains(.,'Online')]")]
        private IWebElement online { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='ui button'][contains(.,'Onsite')]")]
        private IWebElement onsite { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='ui button'][contains(.,'ShowAll')]")]
        private IWebElement showAll { get; set; }
        #endregion

        //Entering the skill in search skill textbox and searching
        public void EnterSkillIntoSearchBox(string SearchTerm)
        {
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//input[contains(@placeholder,'Search skills')][1]", 5);
            SearchSkillsTextbox.SendKeys(SearchTerm + "\n");

        }

         public void EnterSkillIntoFilterSearchBox(string SearchTerm)
         {
             Thread.Sleep(2000);
             GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//section[@class='search-results']/descendant::div[@class='four wide column']/descendant::input[@placeholder='Search skills']", 5);
             FilterSearchSkillsTextbox.SendKeys(SearchTerm + "\n");

         }

        //Getting the total count of the All Categories
        public string GetAllCategoryTotal()
        {
            GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//a[@class='active item']", 6);

            Thread.Sleep(2000);
            string totalCategoryCount = null;

            //Check AllCategory is active and the total are changing
            if (AllCategory.Displayed && AllCategory.Enabled)
            {
                var span = AllCategory.FindElement(By.XPath(".//span"));
                totalCategoryCount = span.Text;
            }
            return totalCategoryCount;

        }

        //Verify AllCategory WebElement is active and Skills are displayed if available
        public int TotalSkillsDisplayed()
        {
            int total = 0;
            if (skillResult.Displayed && skillResult.Enabled)
            {
                total = Int32.Parse(GetAllCategoryTotal());

            }
            else
            {
                Assert.Fail("No skill is displayed");
            }
            return total;

        }

        /* 
         * Verfiy Skills are displayed based on the search text into search box
         * Checking with first Page, as the most relevant result should be displayed on first page of the search result page
         */
        public void VerifySearchedSkill(string skill)
        {
            Thread.Sleep(2000);
            int total = Int32.Parse(GetAllCategoryTotal());
            int countResultFound = 0;
            if (total != 0)
            {
                if (ResultList.Count != 0)
                {
                    //check result contains searched skill
                    foreach (var item in ResultList)
                    {
                        string serviceName = item.FindElement(By.XPath("//div/a[@class='service-info']/p")).Text.ToLower();
                        
                        if (serviceName.Contains(skill.ToLower()))
                        {
                            countResultFound++;
                        }
                    }
                    Assert.GreaterOrEqual(countResultFound, 1, "Skills are displayed based on the search text");
                    Base.test.Log(LogStatus.Pass, "Skills are displayed based on the search text");
                }

            }
            else if (total == 0 && ResultPageMessage.Displayed)
            {
                Assert.AreEqual(ResultPageMessage.Text, "No results found, please select a new category!");
                Base.test.Log(LogStatus.Pass, "Skills are displayed based on the search text with no result");
            }
            else
            {
                Assert.Fail(skill + " is not found!");
                Base.test.Log(LogStatus.Pass, "Skills are failed to displayed based on the search text");
            }
        }

        public void ClickOnACategory(string Category)
        {

           //Searching through the all the available categories and clicking on a required category
            foreach (var categoryElement in Categories)
            {
                string categoryText = categoryElement.Text;
                string[] categorySubstring = categoryText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string categoryName = categorySubstring[0];
                Thread.Sleep(2000);
                if (categoryName.ToLower() == Category.ToLower())
                {
                    Thread.Sleep(4000);
                    categoryElement.Click();
                    Thread.Sleep(2000);
                    string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Category successfully clicked");
                    break;
                }
            }
        }

        public void ClickOnASubCategory(string SubCategory)
        {
            //Searching through the all the available subcategories and clicking on a required subcategory

            foreach (var subCategoryElement in SubCategories)
            {
                string subCategoryText = subCategoryElement.Text;
                string[] subCategorySubstring = subCategoryText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                string subCategoryName = subCategorySubstring[0];
                Thread.Sleep(2000);
                if (subCategoryName.ToLower() == SubCategory.ToLower())
                {
                    Thread.Sleep(2000);
                    subCategoryElement.Click();
                    Thread.Sleep(3000);
                    string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Subcategory successfully clicked");
                    break;
                }
            }   
        }

        public void VerifyCategorySubCategorySearch(string Category, int SubIndex)
        {
            bool activeCategory = false;
            bool activeSubCategory = false;

            //Verifying the category and subcategory selection
            if (skillResult.Displayed && skillResult.Enabled)
                if (skillResult.Displayed && skillResult.Enabled && Categories.Count != 0)
                {
                    var expectedURL = "http://localhost:5000/Home/Search?cat=" + Category.Replace("&", string.Empty).Replace(" ", "");
                    var currentURL = GlobalDefinitions.driver.Url;
                    var expectedSubCategoryURL = "http://localhost:5000/Home/Search?cat=" + Category.Replace("&", string.Empty).Replace(" ", "") + "&subcat=" + SubIndex;
                    foreach (var categoryElement in Categories)
                    {
                       
                        var cat = categoryElement.Text;
                        string[] categorySubstring = cat.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        string categoryName = categorySubstring[0];
                       
                        if (categoryElement.GetAttribute("class").Contains("active"))
                            if (categoryElement.GetAttribute("class").StartsWith("active") && categoryElement.GetAttribute("class").EndsWith("category"))
                            {

                                activeCategory = true;
                                break;
                            }
                    }

                    foreach (var subCategoryElement in SubCategories)
                    {
                        if (subCategoryElement.GetAttribute("class").StartsWith("active") && subCategoryElement.GetAttribute("class").EndsWith("subcategory"))
                        {
                            activeSubCategory = true;
                            break;
                        }
                    }

                    int resultStatus = ResultList.Count;
                    if (resultStatus > 0)
                    {
                        Thread.Sleep(2000);
                        Assert.Multiple(() =>
                        {
                            Assert.AreEqual(expectedSubCategoryURL, currentURL);
                            Assert.AreEqual(true, activeCategory);
                            Assert.AreEqual(true, activeSubCategory);
                            Assert.Greater(ResultList.Count, 0);
                            
                        });
                        
                        Base.test.Log(LogStatus.Pass, "Category and subcategory search successful");

                    }

                    else if (resultStatus == 0)
                    {
                        Thread.Sleep(2000);
                        var c = ResultPageMessage.Text;
                        Assert.AreEqual(expectedURL, currentURL);
                        Assert.AreEqual(true, activeCategory);
                        Assert.AreEqual(true, activeSubCategory);
                        Assert.AreEqual(ResultPageMessage.Text, "No results found, please select a new category!");
                        Base.test.Log(LogStatus.Pass, "Category search successful with no result");
                    }

                    else
                    {
                        Assert.Fail("Category and results not found");
                        Base.test.Log(LogStatus.Fail, "Category and results not found");
                    }


                
                    

                }

        }

        //Check the user is present into the list and Click on the user
        public void SearchUserFromSearchUserTextBox(string name)
        {
            if (FilterSearchUser.Displayed)
            {
                //Entering the user name
                FilterSearchUser.SendKeys(name);

                // Get the users and click on the particular user
                GenericWait.ElementIsVisible(GlobalDefinitions.driver, "XPath", "//div[@class='results transition visible']/div[@class ='result']", 5);
                
                if (UserList.Count != 0)
                {
                    foreach (var user in UserList)
                    {
                        string userName = user.FindElement(By.XPath("//div[@class='results transition visible']/div[@class ='result']//div/span")).Text;
                        if (userName.ToLower().Equals(name.ToLower()))
                        {
                            Thread.Sleep(5000);
                            Actions actions = new Actions(GlobalDefinitions.driver);
                            actions.MoveToElement(user).Build().Perform();
                            user.Click();
                            Thread.Sleep(2000);
                            Base.test.Log(LogStatus.Pass, "User found and clicked successfully");
                            string img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Service listed by user");
                            break;
                        }
                    }
                }
                else if(UserList.Count == 0)
                {
                    Console.WriteLine("User not found");
                    Base.test.Log(LogStatus.Fail, "User not found");
                }
            }
        }

        // Verify Result is listed as per user
        public void VerifyUserServicesListed(string name)
        {
            try
            {
                int total = Int32.Parse(GetAllCategoryTotal());
                int countUserSkillFound = 0;
                if (total >= 1)
                {
                    if (ResultList.Count != 0)
                    {
                        //check result contains searched skill
                        foreach (var item in ResultList)
                        {
                            string sellerName = item.FindElement(By.XPath("//div/a[@class='seller-info']")).Text.ToLower();
                            if (sellerName.Equals(name.ToLower()))
                            {
                                countUserSkillFound++;
                            }
                        }
                        Assert.Multiple(() =>
                        {
                            Assert.GreaterOrEqual(countUserSkillFound, ResultList.Count);

                        });
                        Base.test.Log(LogStatus.Pass, "Result found based on the username");
                    }
                }
                else
                {
                    Assert.Fail(name + " is not registered!");
                    Base.test.Log(LogStatus.Fail, name + " is not registered!");
                }

            }
            catch(Exception e)
            {
                Base.test.Log(LogStatus.Fail, "User not found in the list exception ", e.Message);
            }
        }



        //Click on Filters: Online , Offline and ShowAll
        public int ClickOnFilters(string filter)
        {
            int total;
            switch (filter)
            {
                case "Online":
                    online.Click();
                    total = Int32.Parse(GetAllCategoryTotal());
                    break;
                case "Onsite":
                    onsite.Click();
                    total = Int32.Parse(GetAllCategoryTotal());
                    break;
                default: //ShowAll
                    showAll.Click();
                    total = Int32.Parse(GetAllCategoryTotal());
                    break;
            }
            return total;

        }

        // Verify Result with Filter option Online , Offline and ShowAll
        public void VerifyResultwithFilter(int totalSkills, int refineSkills, string filter)
        {
            var expectedURL = "http://localhost:5000/Home/Search?";
            var currentURL = GlobalDefinitions.driver.Url;
            int current = Int32.Parse(GetAllCategoryTotal());
            if (filter == "Online" || filter == "Onsite")
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(expectedURL, currentURL);
                    Assert.GreaterOrEqual(ResultList.Count,0);
                    Assert.AreEqual(current, refineSkills);
                    Assert.Less(current, totalSkills);
                    Base.test.Log(LogStatus.Pass, "Filter result shown successfully based on filter option  " + filter);
                });
            }
            else if (filter == "ShowAll")
            {
                Assert.Multiple(() =>
                {
                    Assert.AreEqual(current, refineSkills);
                    Assert.LessOrEqual(current, totalSkills);
                    Assert.GreaterOrEqual(ResultList.Count, 0);
                    Base.test.Log(LogStatus.Pass, "Filter result shown successfully based on filter option  " + filter);

                });
            }
            else
            {
                Assert.Fail("Result is not successfully refined!");
                Base.test.Log(LogStatus.Pass, "Filter failed to show the result based on filter option  " + filter);
                
            }
        }



    }
}

