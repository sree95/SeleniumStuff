using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumChallenges
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://mozart-uat.iso.com/";
            driver.FindElement(By.Id("username")).SendKeys("MCcustomeradmin1");
            driver.FindElement(By.Id("password")).SendKeys("Verisk@123");
            driver.FindElement(By.Id("signIn")).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//li[@ng-if='HomePageIconsSearch']/a")).Click();
            driver.FindElement(By.XPath("//*[@id='lobsCombo']/input")).Click();

            IJavaScriptExecutor jsExe = ((IJavaScriptExecutor)driver);
            Thread.Sleep(5000);
            var ulElement = driver.FindElement(By.XPath("//*[@id='lobsCombo']/ul"));
            var height = jsExe.ExecuteScript("return arguments[0].scrollHeight;", ulElement);

            var scrollHeight = Convert.ToInt32(height);


            HashSet<IWebElement> elements = new HashSet<IWebElement>();
            var scroll = 0;
            while (scroll < scrollHeight)
            {
                var elets = driver.FindElements(By.XPath("//li[@ng-repeat='item in $vs_collection']"));

                foreach (var item in elets)
                {
                    elements.Add(item);
                }


                scroll += 100;

                jsExe.ExecuteScript("arguments[0].scrollTop = arguments[1];", ulElement, scroll);
            }

            Console.WriteLine(elements.Count);
        }
    }
}
