using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AppController : ControllerBase {
  
  public AppController() { }

  [HttpGet(Name = "GetHome")]
  public object Get() {
    return new {
      Status = "Ok"
    };
  }
}