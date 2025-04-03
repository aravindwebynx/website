using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System;

namespace TestProject2
{
    public class Tests
    {
        private AndroidDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Setup()
        {
            var appPath = @"C:\Users\mypc\Downloads\aptoide-latest.apk";
            string serverUrl = "http://localhost:4723/";

            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.DeviceName = "emulator-5554";
            options.App = appPath;
            options.AutomationName = "UiAutomator2";
            options.AddAdditionalAppiumOption("chromedriverAutoDownload", true);

            _driver = new AndroidDriver(new Uri(serverUrl), options);
        
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Quit();
            Dispose();
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        [Test]
        public void AptoideAppTest()
        {
            AppiumElement a=_driver.FindElement(By.XPath("//android.widget.Button[@resource-id='android:id/button1']"));    
            if (a != null)
            {
                a.Click();
            }
         
            // Click OK button

            // Click Skip button
            _wait.Until(d => d.FindElement(By.XPath("//android.widget.Button[@resource-id='cm.aptoide.pt:id/skip_button']"))).Click();

            // Click Search icon
            _wait.Until(d => d.FindElement(By.XPath("(//android.widget.ImageView[@resource-id='cm.aptoide.pt:id/icon'])[3]"))).Click();

            // Enter text in Search bar and press Enter
            var searchBar = _wait.Until(d => d.FindElement(By.XPath("//android.widget.AutoCompleteTextView[@resource-id='cm.aptoide.pt:id/search_src_text']")));
            searchBar.SendKeys("bank");
            searchBar.SendKeys(Keys.Enter); // If this fails, try searchBar.SendKeys("\n");

            // Click on Piggy Bank app
            _wait.Until(d => d.FindElement(By.XPath("//android.widget.TextView[@resource-id='cm.aptoide.pt:id/app_name' and @text='Piggy Bank']"))).Click();
        }
    }
}
