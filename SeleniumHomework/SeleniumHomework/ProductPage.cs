using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Globalization;

namespace SeleniumHomework
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class ProductPage<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver driver;

        [SetUp]
        public void Start()
        {
            Environment.SetEnvironmentVariable("webdriver.gecko.driver", "C:/Tools/geckodriver.exe");
            this.driver = new TWebDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void ProductPageTest()
        {
            driver.Url = "http://localhost/litecart/";

            var product = driver.FindElement(By.CssSelector("div[id=box-campaigns] li.product"));
            
            var expectedHeader = product.FindElement(By.CssSelector("div.name")).Text;
            var expectedOldPrice = product.FindElement(By.CssSelector("s.regular-price"));
            var expectedOldPriceText = expectedOldPrice.Text;
            var expectedOldPriceColor = expectedOldPrice.GetCssValue("color");
            var expectedOldPriceStyle = expectedOldPrice.GetCssValue("text-decoration");
            var expectedOldPriceSize = expectedOldPrice.GetCssValue("font-size");
            var expectedCampaignPrice = product.FindElement(By.CssSelector("strong.campaign-price"));
            var expectedCampaignPriceColor = expectedCampaignPrice.GetCssValue("color");
            var expectedCapmaignPriceStyle = int.Parse(expectedCampaignPrice.GetCssValue("font-weight"));
            var expectedCampaignPriceSize = expectedCampaignPrice.GetCssValue("font-size");

            RGB(expectedOldPriceColor, out int rExpectedOld, out int gExpectedOld, out int bExpectedOld);
            RGB(expectedCampaignPriceColor, out _, out int gExpectedCampaign, out int bExpectedCampaign);
            PriceSize(expectedOldPriceSize, out float sizeExpectedOld);
            PriceSize(expectedCampaignPriceSize, out float sizeExpectedCampaign);

            Assert.That(new[] { rExpectedOld, gExpectedOld, bExpectedOld }, Is.All.EqualTo(rExpectedOld));
            Assert.That(new[] { gExpectedCampaign, bExpectedCampaign }, Is.All.EqualTo(0));
            StringAssert.Contains("line-through", expectedOldPriceStyle);
            Assert.IsTrue(expectedCapmaignPriceStyle >= 700);
            Assert.IsTrue(sizeExpectedOld < sizeExpectedCampaign);
            
            driver.FindElement(By.CssSelector("div[id=box-campaigns] li.product a.link")).SendKeys(Keys.Enter);
            
            var actualHeader = driver.FindElement(By.CssSelector("h1.title")).Text;
            
            var actualOldPrice = driver.FindElement(By.CssSelector("s.regular-price"));
            var actualOldPriceText = actualOldPrice.Text;
            var actualOldPriceColor = actualOldPrice.GetCssValue("color");
            var actualOldPriceStyle = actualOldPrice.GetCssValue("text-decoration");
            var actualOldPriceSize = actualOldPrice.GetCssValue("font-size");
            var actualCampaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price"));
            var actualCampaignPriceColor = actualCampaignPrice.GetCssValue("color");
            var actualCampaignPriceStyle = Int32.Parse(actualCampaignPrice.GetCssValue("font-weight"));
            var actualCampaignPriceSize = actualCampaignPrice.GetCssValue("font-size");
            
            RGB(actualOldPriceColor, out int rActualOld, out int gActualOld, out int bActualOld);
            RGB(actualCampaignPriceColor, out _, out int gActualCampaign, out int bActualCampaign);
            PriceSize(actualOldPriceSize, out float sizeActualOld);
            PriceSize(actualCampaignPriceSize, out float sizeActualCampaign);

            Assert.That(new[] { rActualOld, gActualOld, bActualOld }, Is.All.EqualTo(rActualOld));
            Assert.That(new[] { gActualCampaign, bActualCampaign }, Is.All.EqualTo(0));
            StringAssert.Contains("line-through", actualOldPriceStyle);
            Assert.IsTrue(actualCampaignPriceStyle >= 700);
            Assert.IsTrue(sizeActualOld < sizeActualCampaign);

            Assert.AreEqual(expectedHeader, actualHeader);
            Assert.AreEqual(expectedOldPriceText, actualOldPriceText);
        }

        private void RGB(string color, out int r, out int g, out int b)
        {
            color = color.Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Replace(" ", "");
            r = Int32.Parse(color.Split(",")[0]);
            g = Int32.Parse(color.Split(",")[1]);
            b = Int32.Parse(color.Split(",")[2]);
        }

        private void PriceSize(string sizeString, out float sizeFloat)
        {
            sizeFloat = float.Parse(sizeString.Remove(sizeString.Length - 2, 2),
                CultureInfo.InvariantCulture.NumberFormat);
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
