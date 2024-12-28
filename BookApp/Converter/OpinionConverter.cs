using BookApp.Database.Models;
using BookApp.Models;

namespace BookApp.Converter;

public class OpinionConverter
{
    public static Opinion ConvertToOpinion(DbOpinion dbOpinion)
    {
        return new Opinion()
        {
            Id = dbOpinion.Id,
            Rate = dbOpinion.Rate,
            ReadDate = dbOpinion.ReadDate,
            Comment = dbOpinion.Comment,
        };
    }

    public static DbOpinion ConvertToDbOpinion(Opinion opinion)
    {
        return new DbOpinion()
        {
            Comment = opinion.Comment,
            Rate = opinion.Rate,
            ReadDate = opinion.ReadDate,
        };
    }
}