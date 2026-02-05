using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.Feature._Arve
{
    public class arve_save_test : ServiceTestBase
    {
        protected ApplicationDbContext GetFaultyDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            var dbContext = new ApplicationDbContext(options.Options);

            return dbContext;
        }
        [Fact]
        public void Save_should_throw_when_dbcontext_is_null()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new arve_save_command_handler(null);
            });
        }

        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (arve_save_command)null;
            var handler = new arve_save_command_handler(DbContext);

            // Act && Assert
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await handler.Handle(request, CancellationToken.None);
            });
            Assert.Equal("request", ex.ParamName);
        }

        [Fact]
        public async Task Save_should_return_when_id_is_negative()
        {
            // Arrange
            var request = new arve_save_command { Id = -10 };
            var handler = new arve_save_command_handler(GetFaultyDbContext());

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var hasIdError = result.PropertyErrors.Any(e => e.Key == "Id");

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
            Assert.True(hasIdError);
        }

        [Fact]
        public async Task Save_should_save_new_list()
        {
            // Arrange
            var request = new arve_save_command { Id = 0, arve_omanik = 69, rendi_aeg = 69, summa = 420 };
            var handler = new arve_save_command_handler(DbContext);

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.to_arve.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(1, savedToDoList.Id);
        }

        [Fact]
        public async Task Save_should_save_existing_list()
        {
            // Arrange
            var listToAdd = new Arve { Id = 0, arve_omanik = 69, rendi_aeg = 69, summa = 420 };
            var request = new arve_save_command { Id = 1, arve_omanik = 420, rendi_aeg = 420, summa = 6900 };
            var handler = new arve_save_command_handler(DbContext);

            await DbContext.to_arve.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.to_arve.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(request.arve_omanik, savedToDoList.arve_omanik);
        }

        [Fact]
        public async Task Save_should_return_error_if_list_does_not_exist()
        {
            // Arrange
            var listToAdd = new Arve { Id = 0, arve_omanik = 69, rendi_aeg = 69, summa = 420 };
            var request = new arve_save_command { Id = 8, arve_omanik = 420, rendi_aeg = 420, summa = 6900 };
            var handler = new arve_save_command_handler(DbContext);

            await DbContext.to_arve.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }
    }
}
