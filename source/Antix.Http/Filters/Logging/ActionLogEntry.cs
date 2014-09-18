using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Antix.Http.Filters.Logging
{
    public class ActionLogEntry
    {
        private readonly string _action;
        private readonly IDictionary<string, object> _arguments;
        private readonly string _clientIP;
        private readonly string _controller;
        private readonly HttpMethod _requestMethod;
        private readonly Uri _requestUri;

        public ActionLogEntry(
            HttpActionContext actionContext)
        {
            _controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            _action = actionContext.ActionDescriptor.ActionName;
            _arguments = actionContext.ActionArguments;

            _requestUri = actionContext.Request.RequestUri;
            _requestMethod = actionContext.Request.Method;

            _clientIP = actionContext.Request.GetClientIpAddress();
        }

        public string Controller
        {
            get { return _controller; }
        }

        public string Action
        {
            get { return _action; }
        }

        public IDictionary<string, object> Arguments
        {
            get { return _arguments; }
        }

        public Uri RequestUri
        {
            get { return _requestUri; }
        }

        public HttpMethod RequestMethod
        {
            get { return _requestMethod; }
        }

        public string ClientIP
        {
            get { return _clientIP; }
        }
    }
}