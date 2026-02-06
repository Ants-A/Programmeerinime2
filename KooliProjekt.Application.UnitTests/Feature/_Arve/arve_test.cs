using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class arve_test : ServiceTestBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task should_exception_if_page_less_than_0(int page)
        {
            // Arrange
            var query = new arve_query { Page = page, PageSize = 5 };
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_query_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task should_exception_if_page_size_less_than_0(int page)
        {
            // Arrange
            var query = new arve_query { Page = 1, PageSize = page };
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_query_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Theory]
        [InlineData(25)]
        [InlineData(999)]
        public async Task should_exception_if_page_size_bigger_than_max(int page)
        {
            // Arrange
            var query = new arve_query { Page = 1, PageSize = page };
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_query_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);
            await DbContext.SaveChangesAsync();

            // Act
            await Assert.ThrowsAsync<ArgumentException>(() => handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task should_return_argument_null_exeption_if_request_null()
        {
            var handler = new arve_query_handler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new arve_query_handler(null);
            });
        }

        [Fact]
        public async Task should_return_object_if_object_exists()
        {
            // Arrange
            var query = new arve_query { Page = 1, PageSize = 5 };
            var arve = new Arve 
            { 
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_query_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);  
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.CurrentPage); // Cast to Arve before accessing Id
        }

        [Fact]
        public async Task should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new arve_query { Page = 101, PageSize = 10 };
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_query_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Empty(result.Value.Results);
        }
    }
}
