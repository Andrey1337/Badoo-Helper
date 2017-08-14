using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BadooHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://badoo.com/signin/?f=top");

            Login(driver);

            var counter = 0;
            try
            {
                while (true)
                {
                    driver.FindElement(By.XPath("//span[@class='b-link js-profile-header-vote']")).Click();
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    Thread.Sleep(100);
                    counter++;
                }
            }
            catch (Exception e)
            {
                //throw new Exception($"Dayly max count {counter}, Old exeption: {e}");
                Console.WriteLine("Dayly max count " + counter);
            }

        }

        public static void Login(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//input[@class='text-field__input js-signin-login']")).SendKeys(ConfigurationManager.AppSettings.Get("email"));
            driver.FindElement(By.XPath("//input[@class='text-field__input js-signin-password']")).SendKeys(ConfigurationManager.AppSettings.Get("pass"));
            driver.FindElement(By.XPath("//button[@class='btn btn--sm sign-form__submit']")).Click();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }
    }
}
