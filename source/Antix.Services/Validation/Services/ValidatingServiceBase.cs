using System.Linq;
using System.Threading.Tasks;
using Antix.Services.Models;

namespace Antix.Services.Validation.Services
{
    public abstract class ValidatingServiceBase<TModel, TResult> :
        IServiceInOut<TModel, IServiceResponse<TResult>>
    {
        readonly IValidator<TModel> _validator;

        protected ValidatingServiceBase(
            IValidator<TModel> validator)
        {
            _validator = validator;
        }

        public async Task<IServiceResponse<TResult>> ExecuteAsync(
            TModel model)
        {
            var errors =
                _validator.Validate(model);

            if (errors.Any())
            {
                if (!await CatchAsync(model, errors))
                    return ServiceResponse<TResult>.Empty.WithErrors(errors);
            }

            return await ThenAsync(model);
        }

        protected abstract Task<IServiceResponse<TResult>> ThenAsync(
            TModel model);

        protected virtual async Task<bool> CatchAsync(
            TModel model, string[] errors)
        {
            return false;
        }
    }

    public abstract class ValidatingServiceBase<TModel> :
        IServiceInOut<TModel, IServiceResponse>
    {
        readonly IValidator<TModel> _validator;

        protected ValidatingServiceBase(
            IValidator<TModel> validator)
        {
            _validator = validator;
        }

        public async Task<IServiceResponse> ExecuteAsync(
            TModel model)
        {
            var errors =
                _validator.Validate(model);

            if (errors.Any())
            {
                if (!await CatchAsync(model, errors))
                    return ServiceResponse.Empty.WithErrors(errors);
            }

            return await ThenAsync(model);
        }

        protected abstract Task<IServiceResponse> ThenAsync(
            TModel model);

        protected virtual async Task<bool> CatchAsync(
            TModel model, string[] errors)
        {
            return false;
        }
    }
}