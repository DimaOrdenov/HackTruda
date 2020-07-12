using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HackTruda.BusinessLogic.Interfaces;
using HackTruda.Definitions.Enums;
using HackTruda.Definitions.Exceptions;
using HackTruda.Services.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace HackTruda.BusinessLogic
{
    public abstract class BaseLogic<T> : IBaseLogic<T>
    {
        private readonly IRestClient _client;
        private readonly UserContext _userContext;
        private readonly IDebuggerService _debuggerService;

        public BaseLogic(IRestClient client, UserContext context, IDebuggerService debuggerService)
        {
            _client = client;
            _userContext = context;
            _debuggerService = debuggerService;

            //_client.CookieContainer = new CookieContainer();
            _client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            if (_client.BaseUrl == null)
            {
                throw new ArgumentNullException(nameof(_client.BaseUrl));
            }

            BadRequest += BadRequestEventHandler;
            NoContent += NoContentEventHandler;
            NotFound += NotFoundEventHandler;
            TooManyRequests += TooManyRequestsEventHandler;
            Unauthorized += UnauthorizedEventHandler;
            DefaultHttpError += DefaultHttpErrorEventHandler;
            TimeoutRequest += TimeoutRequestEventHandler;
            NetworkUnreachable += NetworkUnreachableEventHandler;
            BeforeRequest += BeforeRequestEventHandler;
            AfterRequest += AfterRequestEventHandler;
        }

        public delegate void ResponseHandler(IRestResponse response);
        public delegate void RequestHandler(IRestRequest request);
        public delegate void RequestDebugHandler(IRestRequest request, string message);
        public delegate void ResponseDebugHandler(IRestRequest request, IRestResponse response, string message);

        public event ResponseHandler BadRequest;
        public event ResponseHandler NoContent;
        public event ResponseHandler NotFound;
        public event ResponseHandler TooManyRequests;
        public event ResponseHandler Unauthorized;
        public event ResponseHandler DefaultHttpError;
        public event ResponseHandler TimeoutRequest;
        public event ResponseHandler NetworkUnreachable;
        public event RequestDebugHandler BeforeRequest;
        public event ResponseDebugHandler AfterRequest;

        protected int Timeout = 30000;

        protected abstract string Route { get; }

        public virtual Task<IEnumerable<T>> Get(CancellationToken token) =>
            ExecuteAsync<IEnumerable<T>>(new RestRequest(Route, Method.GET), token);

        public virtual Task<T> Get(string id, CancellationToken token) =>
            ExecuteAsync<T>(
                new RestRequest(Route + $"/{id}", Method.GET),
                token);

        public virtual Task<bool> Post<TRequest>(TRequest requestModel, CancellationToken token) =>
            ExecuteAsync(
                new RestRequest(Route, Method.POST)
                    .AddJsonBody(requestModel),
                token);

        public virtual Task<bool> Put<TRequest>(TRequest requestModel, int id, CancellationToken token) =>
            ExecuteAsync(
                new RestRequest(Route, Method.PUT)
                    .AddParameter("id", id)
                    .AddJsonBody(requestModel),
                token);

        public virtual Task<bool> Delete(string id, CancellationToken token) =>
            ExecuteAsync(
                new RestRequest(Route, Method.DELETE)
                    .AddParameter("id", id),
                token);

        protected async Task<bool> ExecuteAsync(IRestRequest request, CancellationToken token)
        {
            AddDefaultHeader(request);
            request.Timeout = Timeout;

#if DEBUG
            Guid guid = Guid.NewGuid();
            BeforeRequest?.Invoke(request, guid.ToString());
            request.AdvancedResponseWriter = (input, resp) => resp.RawBytes = ReadAsBytes(input);
#endif

            var response = await _client.ExecuteAsync(request, token);

#if DEBUG
            AfterRequest?.Invoke(request, response, guid.ToString());
#endif

            if (!response.IsSuccessful)
            {
                CheckResponse(response);
            }

            return response.IsSuccessful;
        }

        protected async Task<TOut> ExecuteAsync<TOut>(IRestRequest request, CancellationToken token)
        {
            AddDefaultHeader(request);
            request.Timeout = Timeout;

#if DEBUG
            Guid guid = Guid.NewGuid();
            BeforeRequest?.Invoke(request, guid.ToString());
            request.AdvancedResponseWriter = (input, resp) => resp.RawBytes = ReadAsBytes(input);
#endif

            var response = await _client.ExecuteAsync(request, token);

#if DEBUG
            AfterRequest?.Invoke(request, response, guid.ToString());
#endif

            if (!response.IsSuccessful)
            {
                CheckResponse(response);
            }

            return JsonConvert.DeserializeObject<TOut>(response.Content);
        }

        private void UnauthorizedEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.Unauthorized, response.Content);

        private void TooManyRequestsEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.TooManyRequests, response.Content);

        private void NotFoundEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.NotFound, response.Content);

        private void NoContentEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.NoContent, response.Content);

        private void BadRequestEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.BadRequest, response.Content);

        private void DefaultHttpErrorEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.Unknown, response.Content);

        private void TimeoutRequestEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.Timeout, response.Content);

        private void NetworkUnreachableEventHandler(IRestResponse response) =>
            throw new BusinessLogicException(LogicExceptionType.NoInternet, response.Content);

        private void AfterRequestEventHandler(IRestRequest request, IRestResponse response, string message) =>
            _debuggerService?.Log(
                new StringBuilder()
                    .AppendLine(message + " | Response: ")
                    .AppendLine(request.Method + " :: " + _client.BaseUrl + request.Resource)
                    .AppendLine("ContentType: " + response.ContentType)
                    .AppendLine("StatusCode: " + response.StatusCode)
                    .AppendLine("Error: " + response.ErrorMessage)
                    .AppendLine("Content: " + response.Content)
                    .ToString());

        private void BeforeRequestEventHandler(IRestRequest request, string message) =>
            _debuggerService?.Log(
                new StringBuilder()
                    .AppendLine(message + " | Request: ")
                    .AppendLine(request.Method + " :: " + _client.BaseUrl + request.Resource)
                    .AppendJoin(Environment.NewLine, request.Parameters.Select(param => $"{param.Name} = {JsonConvert.SerializeObject(param.Value)}"))
                    .ToString());

        private byte[] ReadAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];

            using (var ms = new MemoryStream())
            {
                int read;

                try
                {
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    return ms.ToArray();
                }
                catch (WebException ex)
                {
                    return Encoding.UTF8.GetBytes(ex.Message);
                }
            }
        }

        private void CheckResponse(IRestResponse response)
        {
            WebException webException = response?.ErrorException as WebException;

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    BadRequest?.Invoke(response);
                    break;
                case HttpStatusCode.NotFound:
                    NotFound?.Invoke(response);
                    break;
                case HttpStatusCode.Unauthorized:
                    Unauthorized?.Invoke(response);
                    break;
                case (HttpStatusCode)429:
                    TooManyRequests?.Invoke(response);
                    break;
                case HttpStatusCode.NoContent:
                    NoContent?.Invoke(response);
                    break;
                case 0:
                    if (webException?.Status == WebExceptionStatus.NameResolutionFailure ||
                        webException?.Status == WebExceptionStatus.ConnectFailure)
                    {
                        NetworkUnreachable?.Invoke(response);

                        return;
                    }

                    if (webException?.Status == WebExceptionStatus.Timeout)
                    {
                        TimeoutRequest?.Invoke(response);

                        return;
                    }

                    DefaultHttpError?.Invoke(response);

                    break;
                default:
                    DefaultHttpError?.Invoke(response);
                    break;
            }
        }

        private void AddDefaultHeader(IRestRequest request)
        {
            //    if (_userContext.IsAuthenticated)
            {
                request.AddCookie("access_token", _userContext.Token);
                request.AddHeader("Authorization", "Bearer " + _userContext.Token);
                //        }
                //        else
                //        {
                //            request.AddHeader("Authorization", "Bearer " + _userContext.DefaultToken);
                //        }
                //        if (_userContext.GenuineLogin != null)
                //        {
                //            request.AddHeader("GenuineLogin", _userContext.GenuineLogin);
                //        }
            }
        }
    }
}
