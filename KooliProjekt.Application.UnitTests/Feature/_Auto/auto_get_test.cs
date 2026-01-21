using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Auto_;
using KooliProjekt.Application.Features.Klient_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class auto_get_test : TestBase
    {
        [Fact]
        public async Task Get_throws_if_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new auto_get_handler(null);
            });
            var query = new auto_get_query { Id = 0 };
            var handler = new auto_get_handler(DbContext);
            var result = await handler.Handle(query, CancellationToken.None);

            Assert.Null(result.Value);
        }

        [Fact]
        public async Task Get_should_return_object_if_object_exists()
        {
            // Arrange
            var query = new auto_get_query { Id = 1 };
            var auto = new Auto 
            { 
                broneeritav = true,
                tüüp = "Honda",
            };
            var handler = new auto_get_handler(DbContext);
            await DbContext.to_auto.AddAsync(auto);  
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.NotNull(result.Value);
            Assert.Equal(1, result.Value.id);
        }

        [Fact]
        public async Task Get_should_return_null_if_object_does_not_exist()
        {
            // Arrange
            var query = new auto_get_query { Id = 101 };
            var auto = new Auto
            {
                broneeritav = true,
                tüüp = "Honda",
            };
            var handler = new auto_get_handler(DbContext);
            await DbContext.to_auto.AddAsync(auto);
            await DbContext.SaveChangesAsync();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.HasErrors);
            Assert.Null(result.Value);
        }
    }
}
