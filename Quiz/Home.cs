
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Windows;
using System.Threading.Tasks;
using TextCopy;

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

        [Test,Order(3),Ignore("leave")]
        public void CopyToClipBoard()
        {
            IWebElement rightPane = driver.FindElement(By.ClassName("css-ier0s8"));
            rightPane.FindElements(By.TagName("div"))[0].FindElement(By.TagName("button")).Click();


            Review();        
            Thread.Sleep(2000);

            IWebElement continueWithoutCorrectBtn = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[1]/button"));
            continueWithoutCorrectBtn.Click();

            //
            //String copied = System.Windows.Clipboard.GetText();
            var c = clipBoardAsync();
            Thread.Sleep(2000);

            if(c.Result is not null)
            {
                Assert.NotNull(c.Result);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test, Order(3) , Ignore("text download")]
        public void DownloadTextFile()
        {

            IWebElement rightPane = driver.FindElement(By.ClassName("css-ier0s8"));
            IWebElement textButton =  rightPane.FindElements(By.TagName("div"))[1].FindElement(By.TagName("button"));
            string name = textButton.Text;
            textButton.Click();

            Review();
            Thread.Sleep(2000);

            IWebElement fileNameTextField = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div/input"));

            string filename = fileNameTextField.GetAttribute("value");

            IWebElement continueNameView = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/button[1]"));
            continueNameView.Click();
            Thread.Sleep(2000);


            IWebElement downloadWithoutCorrect = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[1]/button"));
            downloadWithoutCorrect.Click();


            Thread.Sleep(2000);

            string curFile = @"C:\Users\CBM\Downloads\"+ filename+".txt";
            if (File.Exists(curFile)){
                string[] lines = File.ReadAllLines(curFile);
                Assert.Greater(lines.Length, 0);
            }
            else
            {
                Assert.Fail();
            }



            //download with correct

            IWebElement downloadWithCorrect = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[2]/button"));
            downloadWithCorrect.Click();

            Thread.Sleep(2000);


            string durFile = @"C:\Users\CBM\Downloads\" + filename + ".txt";
            if (File.Exists(durFile))
            {
                string[] lines = File.ReadAllLines(durFile);
                Assert.Greater(lines.Length, 0);
            }
            else
            {
                Assert.Fail();
            }

            //download with correct end

        }

        [Test, Order(3)]
        public void DownloadPdfFile()
        {

            IWebElement rightPane = driver.FindElement(By.ClassName("css-ier0s8"));
            IWebElement textButton = rightPane.FindElement(By.XPath("//*[@id=\"root\"]/div[1]/div[1]/div/div/div[2]/div/div[5]/div[2]/button[2]"));
            string name = textButton.Text;
            textButton.Click();

            Review();
            Thread.Sleep(2000);

            IWebElement fileNameTextField = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div/input"));

            string filename = fileNameTextField.GetAttribute("value");

            IWebElement continueNameView = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/button[1]"));
            continueNameView.Click();
            Thread.Sleep(2000);


            IWebElement downloadWithoutCorrect = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[1]/button"));
            downloadWithoutCorrect.Click();


            Thread.Sleep(2000);

            string curFile = @"C:\Users\CBM\Downloads\" + filename + ".pdf";
            if (File.Exists(curFile))
            {
                string[] lines = File.ReadAllLines(curFile);
                Assert.Greater(lines.Length, 0);
            }
            else
            {
                Assert.Fail();
            }


            //download with correct

            IWebElement downloadWithCorrect = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div/div[2]/button"));
            downloadWithCorrect.Click();

            Thread.Sleep(2000);


            string durFile = @"C:\Users\CBM\Downloads\" + filename + ".pdf";
            if (File.Exists(durFile))
            {
                string[] lines = File.ReadAllLines(durFile);
                Assert.Greater(lines.Length, 0);
            }
            else
            {
                Assert.Fail();
            }

            //download with correct end

        }

        public void Review()
        {
           
            Thread.Sleep(5000);
            //find popup

            // driver.WindowHandles = driver.p;
            IWebElement rating = driver.FindElement(By.Id("review-modal-title"));
            String modal = rating.Text;

            IWebElement ratingEmail = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[2]/div[1]/div/input"));
            ratingEmail.SendKeys("omar@onescreensolutions.com");

            IWebElement ratingComment = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[2]/div[2]/div/textarea[1]"));
            ratingComment.SendKeys("Test Comment");

            IWebElement ratingStar = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[2]/div[1]/div/span[1]/span[2]"));
            ratingStar.Click();

            IWebElement ratingContinue = driver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[3]/button[1]"));
            ratingContinue.Click();
            Thread.Sleep(2000);
             
        }

        //public async void clipBoardAsync()
        //{
        //    var text = await ClipboardService.GetTextAsync();
        //    Console.WriteLine(text);
        //}

        public async Task<string> clipBoardAsync()
        {
            var text = await ClipboardService.GetTextAsync();
            Console.WriteLine(text);
            return text;
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