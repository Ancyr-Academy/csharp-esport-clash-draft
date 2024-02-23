using System.Net;
using EsportClash.Core.Shared;

namespace EsportClashAPI.Modules.Core.Exceptions;

public class ExceptionMiddleware {
  private readonly RequestDelegate _next;

  public ExceptionMiddleware(RequestDelegate next) {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext httpContext) {
    try {
      await _next(httpContext);
    }
    catch (Exception ex) {
      await HandleExceptionAsync(httpContext, ex);
    }
  }

  public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex) {
    ExceptionViewModel viewModel;

    switch (ex) {
      case BadRequestException:
        viewModel = new ExceptionViewModel {
          Message = ex.Message,
          StatusCode = (int)HttpStatusCode.BadRequest,
          Type = "BadRequestException"
        };

        break;
      case NotFoundException:
        viewModel = new ExceptionViewModel {
          Message = ex.Message,
          StatusCode = (int)HttpStatusCode.NotFound,
          Type = "NotFoundException"
        };
        break;
      default:
        viewModel = new ExceptionViewModel {
          Message = ex.Message,
          StatusCode = (int)HttpStatusCode.InternalServerError,
          Type = "InternalServerError"
        };
        break;
    }

    httpContext.Response.StatusCode = viewModel.StatusCode;
    await httpContext.Response.WriteAsJsonAsync(viewModel);
  }
}