using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendgridEmaiWebAPI.Model;
using SendgridEmaiWebAPI.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SendgridEmaiWebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;
        private ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _logger = logger;
        }


        [HttpPost]

        public async Task<IActionResult> PostAsync([FromBody]EmailModel m)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("about to send an email to {0}", m.To);

                    var tos = new List<string>();
                    tos.Add(m.To);

                    var ccs = new List<string>();
                    ccs.Add(m.Cc);

                    Response result = await _emailSender.SendEmailAsync(tos, ccs, m.Subject, m.Message);
                    if (result.StatusCode == HttpStatusCode.Accepted)
                    {
                        return StatusCode(200);
                    }
                    else
                    {
                        _logger.LogError("Bad response from sendgrid {0}", result.StatusCode);
                        return StatusCode(500);
                    }
                }
                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _logger.LogError("email failed to send; application error {0}", ex.Message);
                return StatusCode(500);
            }
        }
    }
}
