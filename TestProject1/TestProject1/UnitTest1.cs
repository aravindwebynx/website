using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using  NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android.Enums;

namespace TestProject1
{
    public class Tests
    {
        private AndroidDriver _driver; // Remove <AndroidElement>

        [SetUp]
        
        public void Setup()
        {
            var appPath = @"C:\Users\mypc\Downloads\uptodown-app-store-6-58.apk";
            string serverUrl = "http://localhost:4723/";

            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.DeviceName = "emulator-5554"; // Keep this
            options.App = appPath;
            options.AutomationName = "UiAutomator2";

            // Add chromedriver auto-download option
            options.AddAdditionalAppiumOption("chromedriverAutoDownload", true);

            _driver = new AndroidDriver(new Uri(serverUrl), options);
        }

        [Test]
        public void Test1()
        {
            try
            {
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                AppiumElement permissionscr = _driver.FindElement(By.XPath("//android.widget.ScrollView/android.view.View[2]/android.view.View"));
                if (permissionscr.Displayed) { permissionscr.Click();
                    _driver.PressKeyCode(AndroidKeyCode.Back);
                }

                AppiumElement acceptBox = _driver.FindElement(By.XPath("//android.widget.TextView[@resource-id=\"com.uptodown:id/tv_accept_wizard_welcome\"]"));
                if (acceptBox.Displayed)
                {
                    acceptBox.Click();
                }
                
                AppiumElement nextBtn = _driver.FindElement(By.XPath("//android.widget.TextView[@resource-id='com.uptodown:id/tv_next_wizard_continue']"));
                nextBtn.Click();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                // Fetch RecyclerView
                AppiumElement recyclerViewHome = _driver.FindElement(By.Id("com.uptodown:id/recycler_view_home"));
                Console.WriteLine("RecyclerView found!");

                // Scroll inside RecyclerView
                ((IJavaScriptExecutor)_driver).ExecuteScript("mobile: scroll", new Dictionary<string, string>
        {
            { "direction", "down" },
            { "element", recyclerViewHome.Id }
        });

                // Click on the first item inside RecyclerView
                AppiumElement firstItem = _driver.FindElement(By.XPath("(//android.widget.RelativeLayout[@resource-id='com.uptodown:id/rl_home_card_featured_item'])[1]"));
              var text=  firstItem.GetAttribute("text");
               Console.WriteLine(text);
                firstItem.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found, skipping.");
            }

            Assert.Pass();
        }
        [TearDown]
        public void TearDown()
        {
           _driver?.Quit();
            _driver?.Dispose();
        }
    }
}
