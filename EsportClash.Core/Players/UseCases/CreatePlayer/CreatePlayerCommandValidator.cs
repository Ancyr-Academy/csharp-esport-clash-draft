using FluentValidation;

namespace EsportClash.Core.Players.UseCases.CreatePlayer;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand> {
  public CreatePlayerCommandValidator() {
    RuleFor(x => x.Name).NotEmpty();
    RuleFor(x => x.MainRole).IsInEnum();
  }
}