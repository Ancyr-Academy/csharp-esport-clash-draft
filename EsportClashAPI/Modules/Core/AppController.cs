using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Modules.Core;

[ApiController]
[Route("/app")]
public class AppController : ControllerBase {
  [HttpGet]
  public object Get() {
    return new {
      Name = "EsportClashAPI"
    };
  }
}