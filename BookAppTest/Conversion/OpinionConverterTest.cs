using BookApp.Converter;
using BookApp.Database.Models;
using BookApp.Models;

namespace BookAppTest.Conversion;

public class OpinionConverterTests
{
    [Fact]
    public void ConvertToOpinion_ShouldMapDbOpinionToOpinionCorrectly()
    {
        // Arrange
        var dbOpinion = new DbOpinion
        {
            Id = 1,
            Rate = 5,
            ReadDate = new DateTime(2023, 12, 25),
            Comment = "Great book!"
        };

        // Act
        var opinion = OpinionConverter.ConvertToOpinion(dbOpinion);

        // Assert
        Assert.Equal(dbOpinion.Id, opinion.Id);
        Assert.Equal(dbOpinion.Rate, opinion.Rate);
        Assert.Equal(dbOpinion.ReadDate, opinion.ReadDate);
        Assert.Equal(dbOpinion.Comment, opinion.Comment);
    }

    [Fact]
    public void ConvertToDbOpinion_ShouldMapOpinionToDbOpinionCorrectly()
    {
        // Arrange
        var opinion = new Opinion
        {
            Id = 1,
            Rate = 4,
            ReadDate = new DateTime(2022, 11, 20),
            Comment = "Good read!"
        };

        // Act
        var dbOpinion = OpinionConverter.ConvertToDbOpinion(opinion);

        // Assert
        Assert.Equal(opinion.Rate, dbOpinion.Rate);
        Assert.Equal(opinion.ReadDate, dbOpinion.ReadDate);
        Assert.Equal(opinion.Comment, dbOpinion.Comment);
    }

    [Fact]
    public void ConvertToOpinion_WithNullComment_ShouldHandleGracefully()
    {
        // Arrange
        var dbOpinion = new DbOpinion
        {
            Id = 2,
            Rate = 3,
            ReadDate = new DateTime(2021, 6, 15),
            Comment = null
        };

        // Act
        var opinion = OpinionConverter.ConvertToOpinion(dbOpinion);

        // Assert
        Assert.Equal(dbOpinion.Id, opinion.Id);
        Assert.Equal(dbOpinion.Rate, opinion.Rate);
        Assert.Equal(dbOpinion.ReadDate, opinion.ReadDate);
        Assert.Null(opinion.Comment);
    }

    [Fact]
    public void ConvertToDbOpinion_WithNullComment_ShouldHandleGracefully()
    {
        // Arrange
        var opinion = new Opinion
        {
            Id = 3,
            Rate = 2,
            ReadDate = new DateTime(2020, 3, 10),
            Comment = null
        };

        // Act
        var dbOpinion = OpinionConverter.ConvertToDbOpinion(opinion);

        // Assert
        Assert.Equal(opinion.Rate, dbOpinion.Rate);
        Assert.Equal(opinion.ReadDate, dbOpinion.ReadDate);
        Assert.Null(dbOpinion.Comment);
    }
}