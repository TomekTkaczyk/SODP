using FluentValidation;
using SODP.Model;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SODP.Domain.Services;

namespace SODP.Application.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        private readonly IStagesService _stagesService; 
        public ProjectValidator(IStagesService stageService)
        {
            _stagesService = stageService;

            RuleFor(u => u.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Numer jest wymagany")
                .Matches(@"^([1-9]{1})([0-9]{3})$")
                .WithMessage("Numer musi sk�ada� si� z 4 cyfr.")
                .WithName("Numer");

            RuleFor(u => u.StageId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Stadium jest wymagane.")
                .MustAsync((stageId, cancellation) => StageExist(stageId))
                .WithMessage(u => $"Stadium:{u.StageId} nie wyst�puje w bazie.")
                .WithName("Stadium");

            RuleFor(u => u.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tytu� jest wymagany.")
                .Matches(@"^([a-zA-Z]{1,1})([1-9a-zA-Z_ ]{0,})$")
                .WithMessage("Tytu� mo�e zawiera� litery, cyfry, spacj� i podkre�lenie. Pierwszy znaku musi by� liter�.")
                .WithName("Tytu�");
        }

        private async Task<bool> StageExist(int stageId)
        {
            return await _stagesService.ExistAsync(stageId);
        }
    }
}