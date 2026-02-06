using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Klient_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.Application.UnitTests.Features
{
    public class klient_delete_test : ServiceTestBase
    {
        [Fact]
        public void Delete_should_throw_when_dbcontext_is_null()
        {
            var db_context = (ApplicationDbContext)null;
            var exception = Assert.Throws<ArgumentNullException>(() =>
            {
                new klient_delete_command_handler(db_context);
            });

            Assert.Equal(nameof(db_context), exception.ParamName);
        }

        [Fact]
        public async Task Delete_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (klient_delete_command)null;
            var handler = new klient_delete_command_handler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Delete_should_return_when_request_id_is_null_or_negative(int Id)
        {
            // Arrange
            var query = new klient_delete_command { Id = Id };
            var faultyDbContext = GetFaultyDbContext();
            var handler = new klient_delete_command_handler(faultyDbContext);

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
        }

        [Fact]
        public async Task Delete_should_remove_existing_list()
        {
            // Arrange
            var query = new klient_delete_command { Id = 1 };
            var handler = new klient_delete_command_handler(DbContext);

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
            var listTest = await DbContext.to_klient.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }

        [Fact]
        public async Task Delete_should_not_fail_when_list_does_not_exists()
        {
            // Arrange
            var query = new klient_delete_command { Id = 101 };
            var handler = new klient_delete_command_handler(DbContext);

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
            var listTest = await DbContext.to_klient.FindAsync(query.Id);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.Null(listTest);
        }
    }
}
