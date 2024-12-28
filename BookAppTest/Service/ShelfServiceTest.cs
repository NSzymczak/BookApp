using BookApp.Database;
using BookApp.Database.Models;
using BookApp.Models;
using BookApp.Services.Shelf;
using Moq;

namespace BookAppTest.Service;

public class ShelfServiceTest
{
    public class ShelfServiceTests
    {
        private readonly Mock<BookContext> _mockBookContext;
        private readonly ShelfService _shelfService;

        public ShelfServiceTests()
        {
            _mockBookContext = new Mock<BookContext>();
            _shelfService = new ShelfService(_mockBookContext.Object);
        }

        [Fact]
        public async Task GetReadedBooks_ShouldReturnListOfBooks_WhenBooksExist()
        {
            // Arrange
            var dbBooks = new List<DbBook>
            {
                new () { Isbn = "123", Title = "Book 1", Authors = "Autor 1" },
                new () { Isbn = "456", Title = "Book 2" , Authors = "Autor 1" }
            };
            _mockBookContext.Setup(x => x.GetReadedBook()).ReturnsAsync(dbBooks);

            // Act
            var books = await _shelfService.GetReadedBooks();

            // Assert
            Assert.Equal(2, books.Count);
            Assert.Equal("123", books[0].ISBN);
            Assert.Equal("456", books[1].ISBN);
        }

        [Fact]
        public async Task GetReadedBooks_ShouldReturnEmptyList_WhenNoBooksExist()
        {
            // Arrange
            _mockBookContext.Setup(x => x.GetReadedBook()).ReturnsAsync((List<DbBook>)null);

            // Act
            var books = await _shelfService.GetReadedBooks();

            // Assert
            Assert.Empty(books);
        }

        [Fact]
        public async Task GetOpinionsAboutBook_ShouldReturnListOfOpinions_WhenOpinionsExist()
        {
            // Arrange
            var dbOpinions = new List<DbOpinion>
            {
                new () { Id = 1, Comment = "Great!" },
                new () { Id = 2, Comment = "Not bad." }
            };
            _mockBookContext.Setup(x => x.GetOpinions("123")).ReturnsAsync(dbOpinions);

            // Act
            var opinions = await _shelfService.GetOpinionsAboutBook("123");

            // Assert
            Assert.Equal(2, opinions.Count);
            Assert.Equal("Great!", opinions[0].Comment);
            Assert.Equal("Not bad.", opinions[1].Comment);
        }

        [Fact]
        public async Task AddBookToRead_ShouldCallContextMethodWithConvertedData()
        {
            // Arrange
            var opinion = new Opinion { Comment = "Amazing!", Rate = 5 };
            var book = new Book("123") { Title = "Test Book" };

            // Act
            await _shelfService.AddBookToRead(opinion, book);

            // Assert
            _mockBookContext.Verify(x => x.AddBookToReaded(It.IsAny<DbBook>(), It.IsAny<DbOpinion>()), Times.Once);
        }

        [Fact]
        public async Task DeleteReadedBook_ShouldCallContextMethod_WhenIsbnIsValid()
        {
            // Act
            await _shelfService.DeleteReadedBook("123");

            // Assert
            _mockBookContext.Verify(x => x.DeleteReadedBook("123"), Times.Once);
        }

        [Fact]
        public async Task IsBookReaded_ShouldReturnTrue_WhenBookExists()
        {
            // Arrange
            _mockBookContext.Setup(x => x.IsReaded("123")).ReturnsAsync(true);

            // Act
            var result = await _shelfService.IsBookReaded("123");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task IsBookReaded_ShouldReturnFalse_WhenBookDoesNotExist()
        {
            // Arrange
            _mockBookContext.Setup(x => x.IsReaded("123")).ReturnsAsync(false);

            // Act
            var result = await _shelfService.IsBookReaded("123");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteOpinion_ShouldCallContextMethod_WhenIdIsValid()
        {
            // Act
            await _shelfService.DeleteOpinion(1);

            // Assert
            _mockBookContext.Verify(x => x.DeleteOpinion(1), Times.Once);
        }

        [Fact]
        public async Task AddOpinion_ShouldCallContextMethodWithConvertedData()
        {
            // Arrange
            var opinion = new Opinion { Comment = "Good read.", Rate = 4 };
            var book = new Book("123") { Title = "Another Test Book" };

            // Act
            await _shelfService.AddOpinion(opinion, book);

            // Assert
            _mockBookContext.Verify(x => x.AddOpinion(It.IsAny<DbOpinion>(), It.IsAny<DbBook>()), Times.Once);
        }
    }
}