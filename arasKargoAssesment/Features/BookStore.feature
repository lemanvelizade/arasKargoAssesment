Feature: Bookstore Web Application

 Scenario: Search for book
    Given the user is using the browser "chrome"
    Given the user is on the bookstore homepage
    When the user searches for a book by entering the title "The Great Gatsby" and clicking the search button
    Then the search results should include the book with the title "The Great Gatsby"

  Scenario: View details of book
    Given the user has performed a search that includes the book titled "The Great Gatsby"
    When the user clicks on the link for "The Great Gatsby" in the search results
    Then the book details page for "The Great Gatsby" should be displayed, showing detailed information about the book
