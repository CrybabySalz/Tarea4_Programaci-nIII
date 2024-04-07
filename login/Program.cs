using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string driverPath = @"C:\Drivers\chromedriver.exe";

        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        IWebDriver driver = new ChromeDriver(driverPath, options);

        try
        {
            driver.Navigate().GoToUrl("https://www.leagueoflegends.com/es-es/");
            System.Threading.Thread.Sleep(5000);

            IWebElement loginButton = driver.FindElement(By.ClassName("_2I66LI-wCuX47s2um7O7kh"));
            loginButton.Click();

            System.Threading.Thread.Sleep(5000);

            IWebElement usernameInput = driver.FindElement(By.CssSelector("input[name='username']"));
            usernameInput.SendKeys("TheUnforgiven614");

            IWebElement passwordInput = driver.FindElement(By.CssSelector("input[name='password']"));
            passwordInput.SendKeys("Dragonmetal11*crsd");

            IWebElement submitButton = driver.FindElement(By.CssSelector("button[data-riotbar-track='account.login.submit']"));
            submitButton.Click();

            System.Threading.Thread.Sleep(10000);

            if (driver.Url.Contains("account.leagueoflegends.com"))
            {
                Console.WriteLine("Inicio de sesión exitoso.");
            }
            else
            {
                Console.WriteLine("Inicio de sesión fallido.");
            }

            string screenshotPath = Path.Combine(directoryPath, "login_screenshot.png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);
            Console.WriteLine($"Captura de pantalla guardada en: {screenshotPath}");
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine("Error: Elemento no encontrado. Detalles: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
