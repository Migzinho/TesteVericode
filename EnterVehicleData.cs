using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class EnterVehicleData
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Navigate().GoToUrl("https://sampleapp.tricentis.com/101/app.php");

            ValidateElementPresence(driver, By.Id("make"));
            ValidateElementPresence(driver, By.Id("model"));
            ValidateElementPresence(driver, By.Id("cylindercapacity"));
            ValidateElementPresence(driver, By.Id("engineperformance"));
            ValidateElementPresence(driver, By.Id("dateofmanufacture"));
            ValidateElementPresence(driver, By.Id("numberofseats"));
            ValidateElementPresence(driver, By.Id("righthanddriveyes"));
            ValidateElementPresence(driver, By.Id("fuel"));
            ValidateElementPresence(driver, By.Id("payload"));
            ValidateElementPresence(driver, By.Id("totalweight"));
            ValidateElementPresence(driver, By.Id("listprice"));
            ValidateElementPresence(driver, By.Id("licenseplatenumber"));
            ValidateElementPresence(driver, By.Id("annualmileage"));
            ValidateElementPresence(driver, By.Id("nextenterinsurantdata"));

            var makeSelect = new SelectElement(driver.FindElement(By.Id("make")));
            makeSelect.SelectByValue("Audi");

            var modelSelect = new SelectElement(driver.FindElement(By.Id("model")));
            modelSelect.SelectByValue("Motorcycle");

            IWebElement cylinderCapacityInput = driver.FindElement(By.Id("cylindercapacity"));
            cylinderCapacityInput.SendKeys("1500");

            IWebElement enginePerformanceInput = driver.FindElement(By.Id("engineperformance"));
            enginePerformanceInput.SendKeys("100");

            driver.FindElement(By.Id("dateofmanufacture")).SendKeys("01/01/2020");

            var seatsSelect = new SelectElement(driver.FindElement(By.Id("numberofseats")));
            seatsSelect.SelectByValue("4");

            driver.FindElement(By.Id("righthanddriveyes")).Click();

            var fuelSelect = new SelectElement(driver.FindElement(By.Id("fuel")));
            fuelSelect.SelectByValue("Petrol");

            driver.FindElement(By.Id("payload")).SendKeys("500");
            driver.FindElement(By.Id("totalweight")).SendKeys("1500");
            driver.FindElement(By.Id("listprice")).SendKeys("25000");
            driver.FindElement(By.Id("licenseplatenumber")).SendKeys("ABC123");
            driver.FindElement(By.Id("annualmileage")).SendKeys("12000");

            driver.FindElement(By.Id("nextenterinsurantdata")).Click();
            
            // Next no Formulário
            driver.FindElement(By.Id("nextenterproductdata")).Click();

            // Validação das mensagens de erro
            ValidateErrorMessageHidden(driver, By.ClassName("error"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro: " + ex.Message);
        }
        finally
        {
            driver.Quit();
        }
    }

    static void ValidateElementPresence(IWebDriver driver, By by)
    {
        IWebElement element = driver.FindElement(by);
        if (!element.Displayed)
        {
            throw new Exception($"O elemento com o seletor '{by}' não está visível na página.");
        }
    }

    static void ValidateErrorMessageHidden(IWebDriver driver, By by)
    {
        var errorElements = driver.FindElements(by);
        foreach (var errorElement in errorElements)
        {
            if (errorElement.Displayed)
            {
                throw new Exception("A mensagem de erro está visível quando deveria estar oculta.");
            }
        }
    }
}