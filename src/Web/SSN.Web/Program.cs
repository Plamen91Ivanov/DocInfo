namespace SSN.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public static class Program
    {
        public static void Main(string[] args)
        {
         //   Headless();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });

        public static void Headless()
        {
            ChromeOptions head = new ChromeOptions();
            head.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(head);

            // IWebDriver driver = webdriver.PhantomJS()

            driver.Navigate().GoToUrl("http://www.komini.eu/");

            IWebElement content1 = driver.FindElement(By.ClassName("contentheading"));
            var isthiswork1 = content1.Text; 
        }
    }

}
