using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;

namespace Antix.Http.Filters.Logging
{
    public class ActionRequestEntry
    {
        readonly string _action;
        readonly IDictionary<string, object> _arguments;
        readonly string _clientIP;
        readonly string _controller;
        readonly HttpRequestHeaders _requestHeaders;
        readonly string _requestMethod;
        readonly Uri _requestUri;

        public ActionRequestEntry(
            HttpActionContext actionContext)
        {
            _controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            _action = actionContext.ActionDescriptor.ActionName;
            _arguments = actionContext.ActionArguments;

            _requestUri = actionContext.Request.RequestUri;
            _requestMethod = actionContext.Request.Method.Method;
            _requestHeaders = actionContext.Request.Headers;

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

        public string RequestMethod
        {
            get { return _requestMethod; }
        }

        public string ClientIP
        {
            get { return _clientIP; }
        }

        public HttpRequestHeaders RequestHeaders
        {
            get { return _requestHeaders; }
        }
    }
}