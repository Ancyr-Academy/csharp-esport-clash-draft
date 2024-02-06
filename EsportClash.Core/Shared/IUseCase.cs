namespace EsportClash.Core.Shared;

public interface IUseCase<in TRequest, TResponse> {
  Task<TResponse> Execute(TRequest request);
}