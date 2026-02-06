using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.Arve_;
using KooliProjekt.Application.Features.Auto_;
using KooliProjekt.Application.UnitTests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.Feature._Auto
{
    public class auto_save_test : ServiceTestBase
    {
        [Fact]
        public async Task Save_should_throw_when_request_is_null()
        {
            // Arrange
            var request = (auto_save_command)null;
            var handler = new auto_save_command_handler(DbContext);

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
            var request = new auto_save_command { Id = -10 };
            var handler = new auto_save_command_handler(GetFaultyDbContext());

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
            var request = new auto_save_command { Id = 0, broneeritav = true, tüüp = "Toyota"};
            var handler = new auto_save_command_handler(DbContext);

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.to_auto.SingleOrDefaultAsync(l => l.Id == 1);

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
            var listToAdd = new Auto { Id = 0, broneeritav = true, tüüp = "Toyota" };
            var request = new auto_save_command { Id = 1, broneeritav = false, tüüp = "Honda" };
            var handler = new auto_save_command_handler(DbContext);

            await DbContext.to_auto.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);
            var savedToDoList = await DbContext.to_auto.SingleOrDefaultAsync(l => l.Id == 1);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.HasErrors);
            Assert.NotNull(savedToDoList);
            Assert.Equal(request.tüüp, savedToDoList.tüüp);
        }

        [Fact]
        public async Task Save_should_return_error_if_list_does_not_exist()
        {
            // Arrange
            var listToAdd = new Auto { Id = 0, broneeritav = true, tüüp = "Toyota" };
            var request = new auto_save_command { Id = 8, broneeritav = true, tüüp = "Honda" };
            var handler = new auto_save_command_handler(DbContext);

            await DbContext.to_auto.AddAsync(listToAdd);
            await DbContext.SaveChangesAsync();

            // Act 
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.HasErrors);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234567890123456789012345678901234567890123456789000")]
        public void SaveValidator_should_return_false_when_title_is_invalid(string title)
        {
            // Arrange
            var validator = new auto_save_command_validator(DbContext);
            var command = new auto_save_command { Id = 0, broneeritav = true, tüüp = title };

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(nameof(auto_save_command.tüüp), result.Errors.First().PropertyName);
        }

        [Fact]
        public void SaveValidator_should_return_true_when_title_is_valid()
        {
            // Arrange
            var validator = new auto_save_command_validator(DbContext);
            var command = new auto_save_command { Id = 0, broneeritav = true, tüüp = "Toyota" };

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
