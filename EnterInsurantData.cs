using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class EnterInsurantData
{
    static void Main()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            // Navega para a URL do formulário
            driver.Navigate().GoToUrl("https://sampleapp.tricentis.com/101/app.php");

            // Preenche o formulário
            driver.FindElement(By.Id("firstname")).SendKeys("Emidio");
            driver.FindElement(By.Id("lastname")).SendKeys("Mignozzetti");
            driver.FindElement(By.Id("birthdate")).SendKeys("01/01/1990");
            driver.FindElement(By.Id("gendermale")).Click();
            driver.FindElement(By.Id("streetaddress")).SendKeys("Rua General Bittencout");
            new SelectElement(driver.FindElement(By.Id("country"))).SelectByValue("Brazil");
            driver.FindElement(By.Id("zipcode")).SendKeys("06016040");
            driver.FindElement(By.Id("city")).SendKeys("Osasco");
            new SelectElement(driver.FindElement(By.Id("occupation"))).SelectByValue("Employee");
            driver.FindElement(By.Id("speeding")).Click(); // Seleciona um hobby
            driver.FindElement(By.Id("website")).SendKeys("https://github.com/Migzinho/TesteVericode/");

            // Submete o formulário
            driver.FindElement(By.Id("nextenterproductdata")).Click();

            // Validações
            ValidateFieldValue(driver, By.Id("firstname"), "John", "First Name");
            ValidateSelectElement(driver, By.Id("country"), "Brazil", "Country");
            ValidateErrorMessageHidden(driver, By.Id("firstname"));
            ValidateErrorMessageHidden(driver, By.Id("country"));

            Console.WriteLine("Validações bem-sucedidas.");
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

    static void ValidateFieldValue(IWebDriver driver, By by, string expectedValue, string fieldName)
    {
        string actualValue = driver.FindElement(by).GetAttribute("value");
        if (actualValue != expectedValue)
        {
            throw new Exception($"Validação falhou: o valor do campo '{fieldName}' é '{actualValue}', esperado '{expectedValue}'.");
        }
    }

    static void ValidateSelectElement(IWebDriver driver, By by, string expectedText, string fieldName)
    {
        var selectElement = new SelectElement(driver.FindElement(by));
        string selectedOption = selectElement.SelectedOption.Text;
        if (selectedOption != expectedText)
        {
            throw new Exception($"Validação falhou: o valor selecionado para '{fieldName}' é '{selectedOption}', esperado '{expectedText}'.");
        }
    }

    static void ValidateErrorMessageHidden(IWebDriver driver, By by)
    {
        var errorElement = driver.FindElement(by).FindElement(By.XPath("following-sibling::span[@class='error']"));
        if (errorElement.Displayed)
        {
            throw new Exception("A mensagem de erro está visível quando deveria estar oculta.");
        }
    }
}
