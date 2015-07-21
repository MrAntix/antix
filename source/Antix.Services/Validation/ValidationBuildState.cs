using System.Collections.Generic;

namespace Antix.Services.Validation
{
    public class ValidationBuildState
    {
        readonly List<string> _errors;

        public ValidationBuildState()
        {
            _errors = new List<string>();
        }

        public List<string> Errors
        {
            get { return _errors; }
        }
    }
}