using System.Net;

namespace Moxxii.WEB.Repositories
{
    public class HttpResponseWrapper<T>
    {
        #region Constructor
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }
        #endregion

        #region Vars
        public bool Error { get; set; }

        public T? Response { get; set; }

        public HttpResponseMessage HttpResponseMessage { get; set; }
        #endregion

        #region Methods
        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;
            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado";
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            }
            else if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que logearte para hacer esta operación";
            }
            else if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permisos para hacer esta operación";
            }

            return "Ha ocurrido un error inesperado";
        }
        #endregion

    }
}
