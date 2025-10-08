using Config.ConfigReaders;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Config
{
    public class Tests
    {
        string env;

        [SetUp]
        public void Setup()
        {
            env = Environment.GetEnvironmentVariable("ENVIRONMENT");
        }

        [Test]
        public void Test1()
        {
            var user1 = CredentialsConfigReader.ReadConfig("Users.json", "Retailer");
            Assert.AreEqual("dbfhbdhd", user1.Password, "Password is incorrect!");
        }

        [Test]
        public void Test2()
        {
            EndPointsConfigReader.Init("EndPoints.json");
            var user = EndPointsConfigReader.Get("AdminApiUsername");
            var pass = EndPointsConfigReader.Get("AdminApiPassword");
            var url = EndPointsConfigReader.Get("AdminApiUrl");
            Assert.IsNotEmpty(user, "User is empty!");
        }

        [Test]
        public void Test3Web()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.automationtesting.in/my-account/");
            driver.Manage().Window.Maximize();
            var state = driver.FindElement(By.Id("reg_email")).Displayed;
            Assert.IsTrue(state, "Element is not present!");
            if(env == "dev")
            {
                Assert.IsFalse(state, "Element is present!");
            }
            driver.Quit();
        }
    }
}