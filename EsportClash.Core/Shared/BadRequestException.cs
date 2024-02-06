using FluentValidation.Results;

namespace EsportClash.Core.Shared;

public class BadRequestException : Exception {
  public List<string> ValidationErrors { get; set; }
  
  public BadRequestException(string message) : base(message) {
    
  }

  public BadRequestException(string message, ValidationResult result) : base(message) {
    ValidationErrors = new();
    foreach (var error in result.Errors) {
      ValidationErrors.Add(error.ErrorMessage);
    }  
  }
}