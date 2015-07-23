namespace Antix.Services.Validation
{
    public class ValidateRequest<TModel>
    {
        readonly TModel _model;
        readonly string _path;

        public ValidateRequest(
            TModel model,
            string path)
        {
            _model = model;
            _path = path;
        }

        public TModel Model
        {
            get { return _model; }
        }

        public string Path
        {
            get { return _path; }
        }
    }
}