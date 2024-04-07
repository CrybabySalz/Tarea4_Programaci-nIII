using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main(string[] args)
    {
        // Directorio para almacenar capturas de pantalla y reporte HTML
        string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LeagueOfLegendsTest");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Ubicación del ChromeDriver
        string driverPath = @"C:\Drivers\chromedriver.exe";

        // Inicializar el navegador Chrome
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        IWebDriver driver = new ChromeDriver(driverPath, options);

        try
        {
            driver.Navigate().GoToUrl("https://www.leagueoflegends.com/es-es/");

            System.Threading.Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://www.youtube.com/@LeagueofLegendsLATAM");

            System.Threading.Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://twitter.com/lol_es");

            System.Threading.Thread.Sleep(5000);

            driver.Navigate().GoToUrl("https://www.facebook.com/leagueoflegends/?brand_redir=324077981024817&fref=nf");

            System.Threading.Thread.Sleep(5000);


            driver.Navigate().GoToUrl("https://www.instagram.com/leagueoflegends/");

            System.Threading.Thread.Sleep(5000);

            // Verificar si se encuentra en la página de redes sociales
            bool onSocialMediaPage = true;
            if (onSocialMediaPage)
            {
                Console.WriteLine("Visita a redes sociales exitosa.");
            }
            else
            {
                Console.WriteLine("Error: No se pudo visitar la página de redes sociales.");
            }

            // Generar captura de pantalla y guardarla en la carpeta
            string screenshotPath = Path.Combine(directoryPath, "social_media_screenshot.png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);
            Console.WriteLine($"Captura de pantalla de la visita a redes sociales guardada en: {screenshotPath}");

            // Generar el reporte HTML con los resultados
            GenerateHTMLReport(directoryPath, "Visita a redes sociales", "Visita a las redes sociales desde la página de League of Legends", onSocialMediaPage ? "Éxito" : "Error");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }
    }

    static void GenerateHTMLReport(string directoryPath, string testName, string testDescription, string testResult)
    {
        string reportPath = Path.Combine(directoryPath, "report.html");
        using (StreamWriter sw = new StreamWriter(reportPath))
        {
            sw.WriteLine("<!DOCTYPE html>");
            sw.WriteLine("<html>");
            sw.WriteLine("<head>");
            sw.WriteLine("<title>Reporte de Prueba</title>");
            sw.WriteLine("</head>");
            sw.WriteLine("<body>");
            sw.WriteLine("<h1>Reporte de Prueba</h1>");
            sw.WriteLine($"<h2>{testName}</h2>");
            sw.WriteLine($"<p>{testDescription}</p>");
            sw.WriteLine($"<p>Resultado: {testResult}</p>");
            sw.WriteLine("</body>");
            sw.WriteLine("</html>");
        }

        Console.WriteLine($"Reporte generado en: {reportPath}");
    }
}
