using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Net_Core_WebApp.Models;
using Newtonsoft.Json;

namespace Net_Core_WebApp.Controllers
{
    public class ErrorController : Controller
    {
        /// <summary>
        /// Logger for recording error details.
        /// </summary>
        private readonly ILogger<ErrorController> _logger;

        /// <summary>
        /// Default error message displayed to the user in case of unexpected errors.
        /// </summary>
        private readonly string ErrorMessage = "Sorry! We got an error. We are working on it.";

        /// <summary>
        /// Constructor for ErrorController. Injected dependency is used for logging.
        /// </summary>
        /// <param name="logger">The logger instance to use.</param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles generic errors by retrieving details from the IExceptionHandlerPathFeature
        /// and logging the error details. Displays a user-friendly error message and the 
        /// default error message in the view.
        /// </summary>
        /// <returns>An IActionResult representing the Error view with the error details.</returns>
        [Route("Error")]
        public IActionResult Error()
        {
            var model = new ErrorModel();

            try
            {
                // Get details of the exception from IExceptionHandlerPathFeature
                var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                model.ExceptionPath = exceptionHandler?.Path;
                model.ExceptionMessage = exceptionHandler?.Error?.Message;
                model.StackTrace = exceptionHandler?.Error?.StackTrace;

                // Log the error details
                _logger.LogError(JsonConvert.SerializeObject(model));

                // Set user-friendly error message for specific exception (optional)
                ViewBag.Error = model.ExceptionMessage; // Or a more specific message based on exception type

                return View("Error", model);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions during error handling
                model.ExceptionPath = ex.Source;
                model.ExceptionMessage = ex.Message;
                model.StackTrace = ex.StackTrace;

                _logger.LogError(JsonConvert.SerializeObject(model));

                ViewBag.Error = ErrorMessage; // Display default error message

                return View("Error", model);
            }
        }

        /// <summary>
        /// Handles specific HTTP status codes by retrieving details from the IStatusCodeReExecuteFeature
        /// and logging the error details. Displays a user-friendly error message based on the status code 
        /// and the default error message in the view.
        /// </summary>
        /// <param name="statusCode">The HTTP status code that triggered this action.</param>
        /// <returns>An IActionResult representing the Error view with the error details.</returns>
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var model = new ErrorModel();

            try
            {
                // Get details of the status code from IStatusCodeReExecuteFeature
                var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

                switch (statusCode)
                {
                    case 404:
                        model.ExceptionPath = statusCodeResult?.OriginalPath;
                        model.ExceptionMessage = "Sorry, The resources you requested could not be found.";
                        model.StackTrace = statusCodeResult?.OriginalQueryString;
                        break;
                    default:
                        // Handle other status codes with specific messages (optional)
                        break;
                }

                _logger.LogError(JsonConvert.SerializeObject(model));

                ViewBag.Error = model.ExceptionMessage; // Or a more specific message based on status code

                return View("Error", model);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions during error handling
                model.ExceptionPath = ex.Source;
                model.ExceptionMessage = ex.Message;
                model.StackTrace = ex.StackTrace;

                _logger.LogError(JsonConvert.SerializeObject(model));

                ViewBag.Error = ErrorMessage; // Display default error message

                return View("Error", model);
            }
        }
    }
}
