using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WebDriverTasks
{
    class LoginPage
    {
        private string _url;
        private IWebDriver _driver;

        public LoginPage(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public void Login(string login, string pw)
        {
            _driver.Navigate().GoToUrl(_url);

            Console.WriteLine(login);

            // 2) Implicit wait
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            // 3) Thread sleep
            Thread.Sleep(5000); // This is Implicit wait because the code will be fulfilled only once when DOM is loading.

            // Make the screenshot
            Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
            ss.SaveAsFile(System.IO.Path.Combine("C:/Users/lenamavricheva/source/repos/Task 2/WebDriverTasks/assets", "tut_by.png"), ScreenshotImageFormat.Png);

            IWebElement searchEnterButton = _driver.FindElement(By.CssSelector("a.enter"));
            searchEnterButton.Click();

            IWebElement searchLoginField = _driver.FindElement(By.CssSelector("input[type='text']"));
            searchLoginField.SendKeys(login);

            IWebElement searchPwField = _driver.FindElement(By.CssSelector("input[type='password']"));
            searchPwField.SendKeys(pw);

            IWebElement searchApplyButton = _driver.FindElement(By.CssSelector("input.button.m-green.auth__enter"));
            searchApplyButton.Click();
        }


        public Email GetEmailList()
        {
            // 4) Explicit wait
            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            IWebElement findProfileName = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("span.uname")));
            string profileName = findProfileName.Text;

            Email userName = new Email(profileName);

            return userName;
        }
    }


    class MultiSelect
    {
        private string _url;
        private IWebDriver _driver;

        public MultiSelect(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }
        public void SelectedOptions(string state1, string state2, string state3) {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IList<IWebElement> elements = _driver.FindElements(By.CssSelector("select[name='States'] > option:nth-child(3n+1)"));
            foreach (IWebElement e in elements)
            {
                e.Click();
                if (e.Selected)
                {
                    Assert.IsTrue(e.Text == state1 || e.Text == state2 || e.Text == state3);
                    Console.WriteLine("Value of the option item is selected: " + e.Selected + " " + e.Text);
                }
            }
        }
    }

    class TestAlerts {
        private string _url;
        private IWebDriver _driver;

        public TestAlerts(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public void ConfirmBox()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchConfirmButton = _driver.FindElement(By.CssSelector("button[onclick='myConfirmFunction()']"));
            searchConfirmButton.Click();

            var confirm = _driver.SwitchTo().Alert();
            confirm.Accept();

            IWebElement clickResult = _driver.FindElement(By.Id("confirm-demo"));
            Console.WriteLine(clickResult.Text);

            if (clickResult.Text == "You pressed OK!") Console.WriteLine("Confirm test successful");
        }

        public void PromptBox()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchPromptButton = _driver.FindElement(By.CssSelector("button[onclick='myPromptFunction()']"));
            searchPromptButton.Click();

            var prompt = _driver.SwitchTo().Alert();
            prompt.SendKeys("This is a test prompt message");
            prompt.Accept();

            IWebElement clickResult = _driver.FindElement(By.CssSelector("#prompt-demo"));
            Console.WriteLine(clickResult.Text);

            if (clickResult.Text == "You have entered 'This is a test prompt message' !") Console.WriteLine("Prompt test successful");
        }

        public void AlertBox()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchAlertButton = _driver.FindElement(By.CssSelector("button[onclick='myAlertFunction()']"));
            searchAlertButton.Click();

            var expectedAlertText = "I am an alert box!";
            var alert = _driver.SwitchTo().Alert();
            Assert.AreEqual(expectedAlertText, alert.Text);
            if (alert.Text == expectedAlertText) Console.WriteLine("Alert test successful");
            alert.Accept();
        }
    }

    class UserWait
    {
        private string _url;
        private IWebDriver _driver;

        public UserWait(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public void UserWaiter()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchNewUserButton = _driver.FindElement(By.CssSelector("button#save"));
            searchNewUserButton.Click();

            WebDriverWait wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            IWebElement foto = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("img[src*='randomuser']")));
            bool image = foto.Displayed && foto.Enabled;
            if (image) Console.WriteLine("Test successful");
        }
    }

    class Downloader
    {
        private string _url;
        private IWebDriver _driver;

        public Downloader(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public void downloadingPercentage()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IWebElement searchDownloadButton = _driver.FindElement(By.CssSelector("button#cricle-btn"));
            searchDownloadButton.Click();

            Thread.Sleep(10300);
            IWebElement percent = _driver.FindElement(By.CssSelector("div.percenttext"));
            string percentage = percent.Text;
            string value = percentage.Remove(percentage.Length - 1, 1);
            if (Int32.Parse(value) >= 50) {
                _driver.Navigate().Refresh();
                Console.WriteLine(Int32.Parse(value));
                Console.WriteLine("Test successful");
            }
        }
    }

    class Sorting {
        private string _url;
        private IWebDriver _driver;

        public Sorting(string url, IWebDriver driver)
        {
            _url = url;
            _driver = driver;
        }

        public void selectionOfResults()
        {
            _driver.Navigate().GoToUrl(_url);
            _driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);

            IList<IWebElement> elements = _driver.FindElements(By.CssSelector("span > a.paginate_button"));
            int value = elements.Count;


            for (int i = 1; i <= value; i++)
            {
                if (_driver.FindElement(By.CssSelector("a#example_next")).Enabled == true)
                {
                    IWebElement table = _driver.FindElement(By.Id("example"));

                    var columns = table.FindElements(By.TagName("th"));
                    var rows = table.FindElements(By.TagName("tr"));

                    int rowIndex = 1;
                    List<object> _tableDataColections2 = new List<object>();

                    foreach (var row in rows)
                    {
                        int colIndex = 0;
                        var colDatas = row.FindElements(By.TagName("td"));
                        List<TableDataColection> _tableDataColections = new List<TableDataColection>();

                        foreach (var colValue in colDatas)
                        {
                            _tableDataColections.Add(new TableDataColection
                            {
                                ColumnName = columns[colIndex].Text,
                                ColumnValue = colValue.Text
                            });
                            colIndex++;
                        }

                        if (rows.IndexOf(row) == rows.Count - 1)
                        {
                            IWebElement searchPaginateButtonElem = _driver.FindElement(By.CssSelector("a#example_next"));
                            searchPaginateButtonElem.Click();
                        }

                        _tableDataColections2.Add(_tableDataColections);

                        rowIndex++;
                    }

                    var data = from rowData in _tableDataColections2 select rowData;

                    
                    foreach (var tableData in data)
                    {
                        /* if (tableData.ColumnName == "Age")
                        {
                            Console.WriteLine(tableData.ColumnName + " " + tableData.Age);
                        }
                        else if (tableData.ColumnName == "Salary")
                        {
                            Console.WriteLine(tableData.ColumnName + " " + tableData.Salary);
                        }
                        else
                        {
                            Console.WriteLine(tableData.ColumnName + " " + tableData.ColumnValue);
                        } */

                    }

                }
            }

            /* IList<IWebElement> elements = _driver.FindElements(By.CssSelector("tbody > tr >td:nth-child(-n+3)"));
            foreach (IWebElement e in elements)
            {
                string kek = e.Text;
                Console.WriteLine(kek);
            } */

        }
    }
}
