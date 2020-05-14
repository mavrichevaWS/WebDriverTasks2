using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebDriverTasks
{
    [TestClass]
    public class WebDriverTasks
    {
        IWebDriver driver = new ChromeDriver(@"C:\Users\lenamavricheva\source\repos\Task 2\WebDriverTasks\bin\Debug");

        private TestContext _testContext;

        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            driver.Manage().Timeouts().ImplicitWait = new TimeSpan(5000);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            driver.Close();
        }

        [TestMethod]
        [DeploymentItem("d:\\Homework\\Task 2\\data.xls")]
        [DataSource("MyExcelDataSource")]
        public void NewEmailTest()
        {
            string isItAuthorized = "Selenium Test";

            LoginPage loginPage = new LoginPage(UserConstantData.URL, driver);
            loginPage.Login((string)(_testContext.DataRow["param1"]), (string)(_testContext.DataRow["param2"]));

            Email userName = loginPage.GetEmailList();

            Assert.IsTrue(userName.Profile == isItAuthorized);
        }

        [TestMethod]
        public void MultiselectTest()
        {
            string state1 = "California";
            string state2 = "New York";
            string state3 = "Pennsylvania";
            MultiSelect multiselect = new MultiSelect(UserConstantData.URL2, driver);
            multiselect.SelectedOptions(state1, state2, state3);
        }

        [TestMethod]
        public void TestAlerts() {
           TestAlerts alerts = new TestAlerts(UserConstantData.URL3, driver);
           alerts.ConfirmBox();
           alerts.PromptBox();
           alerts.AlertBox();
        }

        [TestMethod]
        public void UserWait() {
            UserWait waiter = new UserWait(UserConstantData.URL4, driver);
            waiter.UserWaiter();
        }

        [TestMethod]
        public void Downloader()
        {
            Downloader download = new Downloader(UserConstantData.URL5, driver);
            download.downloadingPercentage();
        }

        [TestMethod]
        public void Sorting()
        {
            Sorting sortingTable = new Sorting(UserConstantData.URL6, driver);
            sortingTable.selectionOfResults();
        }
    }
}
