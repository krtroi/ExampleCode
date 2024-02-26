using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ktsoft.ModelBinders;

public class DecimalModelBinder : IModelBinder { 
    public Task BindModelAsync(ModelBindingContext context) {
        var value = GetValue(context);
        if(string.IsNullOrEmpty(value)) return Task.CompletedTask;
        if(IsDecimal(value, out var outValue)) return ShowCorrectValue(context, outValue);

        return ShowError(context, "Couldn't parse value!");
    }

    private static string GetValue(ModelBindingContext context) =>
        context.ValueProvider.GetValue(context.ModelName).FirstValue;

    private static bool IsDecimal(string value, out decimal outValue) => 
        decimal.TryParse(GetCorrectFormat(value), out outValue);

    private static string GetCorrectFormat(string value) =>
        value.Replace(".", ",").Trim();

    private static Task ShowCorrectValue(ModelBindingContext context, decimal value) {
        context.Result = ModelBindingResult.Success(value);
        return Task.CompletedTask;
    }

    private static Task ShowError(ModelBindingContext context, string errorText) {
        context.ModelState.TryAddModelError(context.ModelName, errorText);
        return Task.CompletedTask;
    }
}

