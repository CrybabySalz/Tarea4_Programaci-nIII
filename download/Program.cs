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
            // Navegar a la página de descarga de League of Legends
            driver.Navigate().GoToUrl("https://signup.leagueoflegends.com/es-es/signup/redownload?page_referrer=index");

            // Esperar a que la página cargue completamente
            System.Threading.Thread.Sleep(5000); // Esperar 5 segundos

            // Hacer clic en el enlace de descarga
            IWebElement downloadButton = driver.FindElement(By.ClassName("download-button"));
            downloadButton.Click();

            // Esperar un tiempo para que inicie la descarga
            System.Threading.Thread.Sleep(15000); // Esperar 5 segundos

            // Verificar si se descargó el archivo
            bool fileDownloaded = CheckFileDownloaded();
            if (fileDownloaded)
            {
                Console.WriteLine("Descarga de archivo exitosa.");
            }
            else
            {
                Console.WriteLine("Error: No se pudo descargar el archivo.");
            }

            // Generar captura de pantalla y guardarla en la carpeta
            string screenshotPath = Path.Combine(directoryPath, "download_screenshot.png");
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(screenshotPath);
            Console.WriteLine($"Captura de pantalla de la descarga guardada en: {screenshotPath}");

            // Generar el reporte HTML con los resultados
            GenerateHTMLReport(directoryPath, "Descarga de archivo", "Descarga de archivo desde la página de League of Legends", fileDownloaded ? "Éxito" : "Error");

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Cerrar el navegador al finalizar la ejecución
            driver.Quit();
        }
    }

    static bool CheckFileDownloaded()
    {
        // Verificar si el archivo se descargó correctamente en la carpeta de descargas
        string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
        string[] files = Directory.GetFiles(downloadsPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            if (file.EndsWith(".exe"))
            {
                return true;
            }
        }
        return false;
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
