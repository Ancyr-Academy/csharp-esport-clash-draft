namespace EsportClash.Core.Shared;

public interface IUseCase<in TRequest, TResponse> {
  Task<TResponse> Handle(TRequest request);
}