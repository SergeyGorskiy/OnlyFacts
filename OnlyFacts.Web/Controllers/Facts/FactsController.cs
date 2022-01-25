using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlyFacts.Web.Controllers.Facts.Queries;

namespace OnlyFacts.Web.Controllers.Facts
{
    public class FactsController : Controller
    {
        private readonly IMediator _mediator;
        public FactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(int? pageIndex, string tag, string search) => 
            View(await _mediator.Send(new FactGetPagedRequest(pageIndex ?? 1, tag, search), HttpContext.RequestAborted));

        public async Task<IActionResult> Show(Guid id) =>
            View(await _mediator.Send(new FactGetByIdRequest(id), HttpContext.RequestAborted));
    }
}
