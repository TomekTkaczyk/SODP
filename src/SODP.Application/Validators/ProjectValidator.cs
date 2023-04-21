using FluentValidation;
using SODP.Application.Services;
using SODP.Domain.Entities;
using System.Threading.Tasks;

namespace SODP.Application.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        //private readonly IStageService _stagesService;
        //public ProjectValidator(IStageService stageService)
        //{
        //    _stagesService = stageService;

        //    RuleFor(u => u.Number)
        //        .NotNull()
        //        .NotEmpty().When(u => u.Number != null, ApplyConditionTo.CurrentValidator)
        //        .WithMessage("Numer jest wymagany")
        //        .Matches(@"^([1-9]{1})([0-9]{3})$").When(u => u.Number != "", ApplyConditionTo.CurrentValidator)
        //        .WithMessage("Numer musi sk³adaæ siê z 4 cyfr.")
        //        .WithName("Numer");

        //    RuleFor(u => u.StageId)
        //        .GreaterThan(0)
        //        .WithMessage("Stadium jest wymagane.")
        //        .MustAsync((stageId, cancellation) => StageExist(stageId)).When(u => u.StageId > 0, ApplyConditionTo.CurrentValidator)
        //        .WithMessage(u => $"Stadium:{u.StageId} nie wystêpuje w bazie.")
        //        .WithName("Stadium");

        //    RuleFor(u => u.Name)
        //        .NotNull()
        //        .NotEmpty().When(u => u.Name != null, ApplyConditionTo.CurrentValidator)
        //        .WithMessage("Tytu³ jest wymagany.")
        //        .Matches(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$").When(u => u.Name != "", ApplyConditionTo.CurrentValidator)
        //        .WithMessage("Tytu³ moŸe zawieraæ litery, cyfry, spacjê i podkreœlenie. Pierwszy znak musi byæ liter¹.")
        //        .WithName("Tytu³");
        //}

        //private async Task<bool> StageExist(int stageId)
        //{
        //    return await _stagesService.ExistAsync(stageId);
        //}
    }
}