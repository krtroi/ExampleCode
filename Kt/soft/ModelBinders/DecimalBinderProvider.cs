using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ktsoft.ModelBinders;

public class DecimalBinderProvider : IModelBinderProvider { 
    public IModelBinder? GetBinder(ModelBinderProviderContext context) {
        if(context == null) throw new ArgumentNullException(nameof(context));
        return context.Metadata.ModelType == typeof(decimal) ? new DecimalModelBinder() : null;
    }
}

