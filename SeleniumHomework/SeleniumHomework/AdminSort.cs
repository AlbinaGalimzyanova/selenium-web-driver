using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace SeleniumHomework
{
    public class AdminSort
    {
        private IWebDriver driver;

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void SortCountriesTest()
        {
            Login("admin", "admin");

            driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";

            var countries = driver.FindElements(By.XPath("//tr[@class='row']/td[5]/a"));
            var countryNames = new List<string>();

            foreach (IWebElement country in countries)
            {
                countryNames.Add(country.Text);
            }

            var countryNamesSorted = new List<string>(countryNames);
            countryNamesSorted.Sort();

            CollectionAssert.AreEqual(countryNamesSorted, countryNames);
            
            for (int i = 1; i <= countries.Count; i++)
            {
                if (!driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[6]")).Text.Equals("0"))
                {
                    driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[5]/a")).Click();
                    var geoZones = driver.FindElements(By.CssSelector("table.dataTable td:nth-of-type(3) input[type=hidden]"));
                    var geoZonesNames = new List<string>();

                    foreach (IWebElement geoZone in geoZones)
                    {
                        geoZonesNames.Add(geoZone.Text);
                    }

                    var geoZonesNamesSorted = new List<string>(geoZonesNames);
                    geoZonesNamesSorted.Sort();

                    CollectionAssert.AreEqual(geoZonesNamesSorted, geoZonesNames);

                    driver.Navigate().Back();
                }
            }
        }

        [Test]
        public void SortGeoZonesTest()
        {
            Login("admin", "admin");

            driver.Url = "http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones";

            var countries = driver.FindElements(By.XPath("//tr[@class='row']/td[3]/a"));

            for (int i = 1; i <= countries.Count; i++)
            {
                driver.FindElement(By.XPath("//tr[@class='row'][" + i + "]/td[3]/a")).Click();

                var geoZones = driver.FindElements(By.CssSelector("select[name*=zones]:not([class*=hidden])"));
                var geoZonesNames = new List<string>();

                foreach (IWebElement geoZone in geoZones)
                {
                    geoZonesNames.Add(geoZone.Text);
                }

                var geoZonesNamesSorted = new List<string>(geoZonesNames);
                geoZonesNamesSorted.Sort();

                CollectionAssert.AreEqual(geoZonesNamesSorted, geoZonesNames);

                driver.Navigate().Back();
            }
        }

        private void Login(string username, string password)
        {
            driver.Url = "http://localhost/litecart/admin/";
            driver.FindElement(By.CssSelector("input[name=username]")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(password);
            driver.FindElement(By.CssSelector("input[name=remember_me]")).Click();
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
        }

        [TearDown]
        public void Stop()
        {
            driver.Quit();
        }
    }
}
