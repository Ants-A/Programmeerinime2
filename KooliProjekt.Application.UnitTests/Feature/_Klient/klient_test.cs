using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Klient_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class klient_test : ServiceTestBase
    {
        protected ApplicationDbContext GetFaultyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var dbContext = new ApplicationDbContext(options.Options);

            return dbContext;
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Get_should_return_null_request_page_is_less_than_or_equal_to_zero(int page)
        {
            // Arrange
            var dbContext = GetFaultyDbContext();
            var query = new klient_query { Page = page, PageSize = 10, };
            var handler = new klient_query_handler(dbContext);
            var klient = new Klient
            {
                email = "mdea.midagi@techno.ee",
                nimi = "Toomas",
                phone = 537282012,
            };
            await DbContext.to_klient.AddAsync(klient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_argument_null_exeption_if_request_null()
        {
            var handler = new klient_query_handler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new klient_query_handler(null);
            });
            var query = new klient_query { Page = 0, PageSize = 5 };
            var handler = new klient_query_handler(DbContext);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new klient_query { Page = 1, PageSize = 5 };
            var klient = new Klient
            {
                email = "mdea.midagi@techno.ee",
                nimi = "Toomas",
                phone = 537282012,
            };
            var handler = new klient_query_handler(DbContext);
            await DbContext.to_klient.AddAsync(klient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CurrentPage); // Cast to Arve before accessing Id
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new klient_query { Page = 101, PageSize = 10 };
            var klient = new Klient
            {
                email = "mdea.midagi@techno.ee",
                nimi = "Toomas",
                phone = 537282012,
            };
            var handler = new klient_query_handler(DbContext);
            await DbContext.to_klient.AddAsync(klient);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Empty(result.Value.Results);
        }
    }
}
