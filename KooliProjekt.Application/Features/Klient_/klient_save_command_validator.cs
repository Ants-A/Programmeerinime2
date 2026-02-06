using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Klient_
{
    // 15.11.2025
    // Valideerimise klass SaveToDoListCommand käsu jaoks
    // Võetakse programmi poolt külge automaatselt
    public class klient_save_command_validator : AbstractValidator<klient_save_command>
    {
        public klient_save_command_validator(ApplicationDbContext context)
        {
            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email on vajalik")
                .MaximumLength(32).WithMessage("Email ei saa olla pikkem kui 32 tähte")
                // Oma loogikaga valideerimise reegel
                // Siin võib kasutada DbContexti klassi
                .Custom((s, context) =>
                {
                    // Command või query, mida valideerime
                    var command = context.InstanceToValidate;

                    // Oma valideerimise loogika
                    // koos vea lisamisega
                    //var failure = new ValidationFailure();
                    //failure.AttemptedValue = command.ProjectId;
                    //failure.ErrorMessage = "Cannot find project with Id " + command.ProjectId;
                    //failure.PropertyName = nameof(command.ProjectId);

                    //context.AddFailure(failure);
                });
        }
    }
}
