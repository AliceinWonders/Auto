﻿using Microsoft.AspNetCore.Mvc;

[Route("api")]
[ApiController]
public class ApiController : ControllerBase {
    [HttpGet]
    public IActionResult Get() {
        var result = new {
            message = "Welcome to the Auto API!",
            _links = new {
                vehicles = new {
                    href = "/api/vehicles"
                }
            }
        };
        return new JsonResult(result);
    }
}