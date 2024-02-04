using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SODP.WebApi.v0_01.Controllers;

[ApiController]
[Route("/api/v0_01/dictionary")]
public class DictionaryController : ApiBaseController
{
	public DictionaryController(
		ISender sender,
		ILogger<DictionaryController> logger) : base(sender, logger) { }
}
