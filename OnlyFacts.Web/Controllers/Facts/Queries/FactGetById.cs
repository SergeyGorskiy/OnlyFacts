using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.AspNetCore.Controllers.Base;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OnlyFacts.Web.Data;
using OnlyFacts.Web.ViewModels;

namespace OnlyFacts.Web.Controllers.Facts.Queries
{
    public class FactGetByIdRequest: OperationResultRequestBase<FactViewModel>
    {
        public FactGetByIdRequest(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }

    public class FactGetByIdRequestHandler: OperationResultRequestHandlerBase<FactGetByIdRequest, FactViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FactGetByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override async Task<OperationResult<FactViewModel>> Handle(FactGetByIdRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<FactViewModel>();
            operation.AppendLog("Searching fact in DB");
            var entity = await _unitOfWork.GetRepository<Fact>().GetFirstOrDefaultAsync(
                predicate: x => x.Id == request.Id,
                include: i => i.Include(x => x.Tags));

            if (entity is null)
            {
                operation.AddWarning($"Факт с идентификатором {request.Id} не найден");
                return operation;
            }

            operation.AppendLog("Fact found. Mapping ...");
            operation.Result = _mapper.Map<FactViewModel>(entity);
            operation.AppendLog("Return fact to UI");
            return operation;
        }
    }
}
