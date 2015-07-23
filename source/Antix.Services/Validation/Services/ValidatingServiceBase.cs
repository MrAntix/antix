using System.Linq;
using System.Threading.Tasks;
using Antix.Services.Models;

namespace Antix.Services.Validation.Services
{
    public abstract class ValidatingServiceBase<TModel, TResult> :
        IServiceInOut<TModel, ServiceResponse<TResult>>
    {
        readonly IValidator<TModel> _validator;

        protected ValidatingServiceBase(
            IValidator<TModel> validator)
        {
            _validator = validator;
        }

        public async Task<ServiceResponse<TResult>> ExecuteAsync(
            TModel model)
        {
            var errors = await _validator
                .ValidateAsync(model);

            if (errors.Any())
            {
                if (!await CatchAsync(model, errors))
                    return ServiceResponse<TResult>.Empty.WithErrors(errors);
            }

            return await ThenAsync(model);
        }

        protected abstract Task<ServiceResponse<TResult>> ThenAsync(
            TModel model);

        protected virtual async Task<bool> CatchAsync(
            TModel model, string[] errors)
        {
            return false;
        }
    }

    public abstract class ValidatingServiceBase<TModel> :
        IServiceInOut<TModel, ServiceResponse>
    {
        readonly IValidator<TModel> _validator;

        protected ValidatingServiceBase(
            IValidator<TModel> validator)
        {
            _validator = validator;
        }

        public async Task<ServiceResponse> ExecuteAsync(
            TModel model)
        {
            var errors =await _validator
                .ValidateAsync(model);

            if (errors.Any())
            {
                if (!await CatchAsync(model, errors))
                    return ServiceResponse.Empty.WithErrors(errors);
            }

            return await ThenAsync(model);
        }

        protected abstract Task<ServiceResponse> ThenAsync(
            TModel model);

        protected virtual async Task<bool> CatchAsync(
            TModel model, string[] errors)
        {
            return false;
        }
    }
}