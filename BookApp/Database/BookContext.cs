using AsyncAwaitBestPractices;
using BookApp.Database.Models;
using SQLite;

namespace BookApp.Database;

public class BookContext
{
    private SQLiteAsyncConnection? Database;

    public BookContext()
    {
        Init().SafeFireAndForget();
    }

    private async Task Init()
    {
        if (Database is not null)
        {
            return;
        }

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<DbOpinion>();
        await Database.CreateTableAsync<DbBook>();
    }

    public virtual async Task<List<DbBook>?> GetReadedBook()
    {
        if (Database is null)
        {
            return null;
        }
        var list = await Database.Table<DbBook>().ToListAsync();
        return list;
    }

    public virtual async Task AddBookToReaded(DbBook dbReadBook, DbOpinion dbOpinion)
    {
        if (Database is null)
        {
            return;
        }
        var count = await Database.InsertAsync(dbReadBook);
        if (count > 0)
        {
            dbOpinion.BookId = dbReadBook.Id;
            await Database.InsertAsync(dbOpinion);
        }
    }

    public virtual async Task DeleteReadedBook(string isbn)
    {
        if (Database is null)
        {
            return;
        }

        var dbReadedBook = await Database.Table<DbBook>().Where(x => x.Isbn == isbn).FirstOrDefaultAsync();
        if (dbReadedBook is null)
        {
            return;
        }

        await Database.DeleteAsync(dbReadedBook);
    }

    public virtual async Task<bool> IsReaded(string isbn)
    {
        if (Database is null)
        {
            return false;
        }

        var dbReadedBook = await Database.Table<DbBook>().Where(x => x.Isbn == isbn).FirstOrDefaultAsync();
        return dbReadedBook is not null;
    }

    public virtual async Task<List<DbOpinion>> GetOpinions(string isbn)
    {
        if (Database is null)
        {
            return [];
        }

        var dbReadedBook = await Database.Table<DbBook>().Where(x => x.Isbn == isbn).FirstOrDefaultAsync();
        if (dbReadedBook is null)
        {
            return [];
        }

        var dbOpinions = await Database.Table<DbOpinion>().Where(x => x.BookId == dbReadedBook.Id).ToListAsync();
        return dbOpinions;
    }

    public virtual async Task AddOpinion(DbOpinion opinion, DbBook book)
    {
        if (Database is null)
        {
            return;
        }
        var dbReadedBook = await Database.Table<DbBook>().Where(x => x.Isbn == book.Isbn).FirstOrDefaultAsync();
        if (dbReadedBook is null)
        {
            await AddBookToReaded(book, opinion);
            return;
        }
        opinion.BookId = dbReadedBook.Id;
        await Database.InsertAsync(opinion);
    }

    public virtual async Task DeleteOpinion(int id)
    {
        if (Database is null)
        {
            return;
        }
        var dbOpinion = await Database.Table<DbOpinion>().Where(x => x.Id == id).FirstOrDefaultAsync();
        var dbReadedBook = await Database.Table<DbBook>().Where(x => x.Id == dbOpinion.BookId).FirstOrDefaultAsync();
        if (dbReadedBook is null)
        {
            return;
        }
        await Database.DeleteAsync<DbOpinion>(id);
        if (await Database.Table<DbOpinion>().Where(x => x.BookId == dbReadedBook.Id).CountAsync() == 0)
        {
            await DeleteReadedBook(dbReadedBook.Isbn);
        }
    }
}