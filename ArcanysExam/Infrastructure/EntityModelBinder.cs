using ArcanysExam.Data;
using ArcanysExam.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ArcanysExam.Infrastructure
{
    public class EntityModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var original = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (original != ValueProviderResult.None)
            {
                var originalValue = original.FirstValue;
                int id;
                if (int.TryParse(originalValue, out id))
                {
                    var dbContext = bindingContext.HttpContext.RequestServices.GetService<ExamContext>();
                    IEntity entity = null;
                    if (bindingContext.ModelType == typeof(File))
                    {
                        entity = await dbContext.Set<File>().FindAsync(id);
                    }

                    bindingContext.Result = entity != null ? ModelBindingResult.Success(entity) : bindingContext.Result;
                }
            }
        }
    }
}
