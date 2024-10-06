using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;

[Binding]
public class BookStoreSteps
{
    private IWebDriver driver;

    [Given(@"the user is using the browser ""(.*)""")]
    public void GivenTheUserIsUsingTheBrowser(string browser)
    {
        switch (browser.ToLower())
        {
            case "chrome":
                driver = new ChromeDriver();
                break;
            case "firefox":
                driver = new FirefoxDriver();
                break;
            case "edge":
                driver = new EdgeDriver();
                break;
            default:
                throw new ArgumentException("Unsupported browser: " + browser);
        }
    }

    [Given(@"the user is on the bookstore homepage")]
    public void GivenTheUserIsOnTheBookstoreHomepage()
    {
        driver.Navigate().GoToUrl("http://bookstore-url.com");
    }

    [When(@"the user searches for a book by entering the title ""(.*)"" and clicking the search button")]
    public void WhenTheUserSearchesForABookByEnteringTheTitleAndClickingTheSearchButton(string bookTitle)
    {
        var searchBar = driver.FindElement(By.Name("search"));
        searchBar.SendKeys(bookTitle);

        var searchButton = driver.FindElement(By.Id("searchButton"));
        searchButton.Click();
    }

    [Then(@"the search results should include the book with the title ""(.*)""")]
    public void ThenTheSearchResultsShouldIncludeTheBookWithTheTitle(string bookTitle)
    {
        var searchResults = driver.FindElements(By.CssSelector(".search-result"));
        bool containsBook = searchResults.Any(result => result.Text.Contains(bookTitle));
        Assert.IsTrue(containsBook, $"The book '{bookTitle}' was not found in the search results.");
    }

    [Given(@"the user has performed a search that includes the book titled ""(.*)""")]
    public void GivenTheUserHasPerformedASearchThatIncludesTheBookTitled(string bookTitle)
    {
        // search book
        WhenTheUserSearchesForABookByEnteringTheTitleAndClickingTheSearchButton(bookTitle);
    }

    [When(@"the user clicks on the link for ""(.*)"" in the search results")]
    public void WhenTheUserClicksOnTheLinkForInTheSearchResults(string bookTitle)
    {
        var bookLink = driver.FindElement(By.LinkText(bookTitle));
        bookLink.Click();
    }

    [Then(@"the book details page for ""(.*)"" should be displayed, showing detailed information about the book")]
    public void ThenTheBookDetailsPageForShouldBeDisplayed(string bookTitle)
    {
        var title = driver.FindElement(By.Id("bookTitle")).Text;
        var author = driver.FindElement(By.Id("bookAuthor")).Text;
        var description = driver.FindElement(By.Id("bookDescription")).Text;

        Assert.AreEqual(bookTitle, title, "Book title is incorrect.");
        Assert.IsTrue(!string.IsNullOrEmpty(author), "Author is missing.");
        Assert.IsTrue(!string.IsNullOrEmpty(description), "Book description is missing.");
    }

    [AfterScenario]
    public void TearDown()
    {
        driver.Quit();
    }
}
