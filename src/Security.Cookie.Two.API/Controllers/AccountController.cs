﻿using Microsoft.AspNetCore.Mvc;
using Security.Cookie.Application.Dtos.Accounts.Registers;
using Security.Cookie.Application.Dtos.Accounts.SignIns;
using Security.Cookie.Application.Interfaces.Services;

namespace Security.Cookie.Two.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountSignInService accountSignInManagerService;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            IAccountSignInService accountSignInManagerService, ILogger<AccountController> logger)
                => (this.accountSignInManagerService, this.logger) = (accountSignInManagerService, logger);

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(
            [FromBody] RequestSignInDto requestDto)
        {
            // Add here your IP for safelist

            try
            {
                var responseDto = await accountSignInManagerService.SignInAsync(requestDto);

                return Ok(responseDto);
            }
            catch (Exception e)
            {
                logger.LogCritical("An error has occured!", e);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet("signin")]
        public IActionResult SignInAsync([FromQuery]string returnUrl)
        {
            // Here if for redirect
            try
            {
                return Ok(returnUrl);
            }
            catch (Exception e)
            {
                logger.LogCritical("An error has occured!", e);

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RequestRegisterDto requestDto)
        {
            try
            {
                await accountSignInManagerService.RegisterAsync(requestDto);

                return Ok();
            }
            catch (Exception e)
            {
                logger.LogCritical("An error has occured!", e.ToString());

                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("access/denied")]
        public IActionResult AccessDenied()
        {
            return new UnauthorizedObjectResult("You are not authorize!");
        }
    }
}
