using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Core;

[ApiController]
[Route("/app")]
public class AppController : ControllerBase {
  
  public AppController() { }

  [HttpGet]
  public object Get() {
    return new {
      Name = "EsportClashAPI",
    };
  }
}