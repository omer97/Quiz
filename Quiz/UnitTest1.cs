using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Quiz
{
    public class Tests
    {
        IWebDriver driver;
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver("C:\\Users\\CBM\\Downloads\\chromedriver_win32\\chromedriver.exe");
            driver.Navigate().GoToUrl("https://quizwiz.ai/");

        }

        [Test, Order(1)]

        public void Popup()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IWebElement btnStartAddingContent = driver.SwitchTo().ActiveElement().FindElement(By.ClassName("MuiButtonBase-root"));
            if (btnStartAddingContent != null)
            {
                btnStartAddingContent.Click();
            }
            else { Assert.Fail(); }


        }

        [Test, Order(2)]
        public void PasteText()
        {
            IWebElement leftpane = driver.FindElement(By.Id("quiz-input-field"));
            leftpane.SendKeys("The clickAndWait command doesn't get converted when you choose the Webdriver format in the Selenium IDE. Here is the workaround. Add the wait line below. Realistically, the problem was the click or event that happened before this one—line 1 in my C# code. But really, just make sure you have a WaitForElement before any action where you're referencing a \"By\" object.");

            IWebElement btnCreateQuiz = driver.FindElement(By.Id(":r1:"));
            btnCreateQuiz.Click(); 
            //new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(driver =>
            // driver.FindElement(By.Id("scrollable-quiz-content-box")).FindElements(By.ClassName("MuiBox-root")).ToList().Count > 0
            //);
             
            //  new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(driver =>
            // driver.FindElement(By.XPath("//*[@id=\"root\"]/div[1]/div[1]/div/div/div[1]/div/form/div[2]/div[1]/button[2]"))
            //); 
            //driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(100);

            Thread.Sleep(10000);
            List<string> qustList = new List<string>();
            List<string> ansList = new List<string>();

            IWebElement rightPane = driver.FindElement(By.Id("scrollable-quiz-content-box"));
            List<IWebElement> questionAnswerListChildren = rightPane.FindElements(By.XPath("//div[contains(@id,'Question-Answer-box')]")).ToList<IWebElement>();


            foreach (var q in questionAnswerListChildren)
            {
                var questionList = q.FindElements(By.XPath("*"));

                //checkbox code
                questionList[0].FindElement(By.ClassName("PrivateSwitchBase-input")).Click(); 
                //checkbox code end


               

                IWebElement question = questionList[0].FindElement(By.TagName("textarea"));
                string questionText = question.Text;
                qustList.Add(questionText);    //making list of questions
                //if (String.IsNullOrEmpty(questionText)) { break; }

                //Traversing in Answers

                List<IWebElement> answerList = questionList[1].FindElements(By.ClassName("css-16izr03")).ToList();
                foreach (var ans in answerList)
                {
                    IWebElement answer = ans.FindElement(By.TagName("textarea"));
                    string answerText = answer.Text;
                    ansList.Add(answerText);
                    // if (String.IsNullOrEmpty(answerText)) { break; }
                } 
            }
             
            if (qustList.Count > 0 && ansList.Count > 0)
            {
                // Assert.Pass();
            }
            else
            {
                //Assert.Fail();

            } 

        }

        [Test,Order(3)]
        public void CopyToClipBoard()
        {
            IWebElement rightPane = driver.FindElement(By.ClassName("css-ier0s8"));
            rightPane.FindElements(By.TagName("div"))[0].FindElement(By.TagName("button")).Click();

            Thread.Sleep(5000);
        }

        public void SelectQuestions()
        {

        }



        [OneTimeTearDown]
        public void Exit()
        {
            driver.Quit();
        }
    }
}