using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace SODP.Application.Utils;

public class UpperCaseModelBinder : IModelBinder
{
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		var propertyName = bindingContext.FieldName;
		var valueProviderResult = bindingContext.ValueProvider.GetValue(propertyName);

		if (valueProviderResult != ValueProviderResult.None)
		{
			var valueAsString = valueProviderResult.FirstValue;
			var upperCaseValue = valueAsString?.ToUpper();

			bindingContext.Result = ModelBindingResult.Success(upperCaseValue);
		}

		return Task.CompletedTask;
	}
}
