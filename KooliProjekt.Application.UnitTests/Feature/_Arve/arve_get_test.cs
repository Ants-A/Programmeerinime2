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
    public class arve_get_test : ServiceTestBase
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Get_should_return_null_request_id_is_zero_or_less(int Id)
        {
            // Arrange
            var dbContext = GetFaultyDbContext();
            var query = new arve_get_query { Id = Id };
            var handler = new arve_get_handler(dbContext);
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            await DbContext.to_arve.AddAsync(arve);
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
            var handler = new arve_get_handler(DbContext);
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null!, CancellationToken.None));
        }


        [Fact]
        public async Task Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new arve_get_handler(null);
            });
            var query = new arve_get_query { Id = 0 };
            var handler = new arve_get_handler(DbContext);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new arve_get_query { Id = 1 };
            var arve = new Arve 
            { 
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_get_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);  
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.Id); // Cast to Arve before accessing Id
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new arve_get_query { Id = 101 };
            var arve = new Arve
            {
                arve_omanik = 23,
                rendi_aeg = 69,
                summa = 420,
            };
            var handler = new arve_get_handler(DbContext);
            await DbContext.to_arve.AddAsync(arve);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
