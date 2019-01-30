﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToBuy.Middleware
{
    public class CsvModelBinder<T> : IModelBinder where T : IConvertible
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var fieldName = bindingContext.FieldName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(fieldName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(fieldName, valueProviderResult);

            var model = new List<T>();

            foreach (string delimitedString in valueProviderResult.Values)
            {
                var splitValues = delimitedString
                    .Split(',')
                    .Cast<string>();

                var convertedValues = splitValues
                    .Select(str => Convert.ChangeType(str, typeof(T)))
                    .Cast<T>();

                model.AddRange(convertedValues);
            }

            bindingContext.Result = ModelBindingResult.Success(model);

            return Task.CompletedTask;
        }

        public class CsvModelBinderProvider : IModelBinderProvider
        {
            public IModelBinder GetBinder(ModelBinderProviderContext context)
            {
                if (context.Metadata.ModelType == typeof(List<int>))
                {
                    return new CsvModelBinder<int>();
                }

                return null;
            }
        }
    }
}
