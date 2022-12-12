For use Bat.Dapper just do it :

1- Install Bat.Dapper on your project


2- Use it in bussiness logic
for example :

    public class PageRepo : IPageRepo
    {
        private SqlConnection _sqlConnection;

        public PageRepo(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("CrawlerDbContext"));
        }


        public async Task<bool> AddAsync(CrawledPageDto model)
        {
            await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[InsertPage]",
                    new { Page = model.ToTableValuedParameter("[dbo].[Tvp_Page]") });

            return true;
        }

        public async Task<bool> UpdateAsync(CrawledPageDto model)
        {
            await _sqlConnection.ExecuteSpCommandAsync<int>("[Instagram].[UpdatePage]",
                    new { Page = model.ToTableValuedParameter("[dbo].[Tvp_Page]") });

            return true;
        }

        public async Task<bool> DeleteAsync(string pageId)
        {
            var query = "DELETE [Instagram].[Page] " +
                        "WHERE  [Username] = @Username";
            
            return await _sqlConnection.ExecuteQueryCommandAsync(query, new { Username = pageId });
        }

        public async Task<Page> FindAsync(string pageId)
        {
            var query = "SELECT * " +
                        "FROM   [Instagram].[Page] " +
                        "WHERE  [Username] = @Username";
                
            return await _sqlConnection.ExecuteQuerySingleAsync<Page>(query, new { Username = pageId });
        }

        public async Task<IEnumerable<Page>> GetAsync(DateTime lastCrawlDate, PagingParameter pagingParameter)
        {
            var query = "SELECT		* " +
                        "FROM		[Instagram].[Page] p " +
                        "WHERE      p.ModifyDateMi <= @lastCrawlDate " +
                        "ORDER BY	p.PageId ASC " +
                        "OFFSET		@PageSize * (@PageNumber - 1) ROWS " +
                        "FETCH NEXT	@PageSize ROWS ONLY;";

            return await _sqlConnection.ExecuteQueryAsync<Page>(query,
                new { lastCrawlDate = lastCrawlDate, pagingParameter.PageNumber, pagingParameter.PageSize });
        }
    }

And :

    public sealed class CrawlerUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public CrawlerUnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public IPageRepo PageRepo => _serviceProvider.GetService<IPageRepo>();
    }
