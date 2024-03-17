using Microsoft.AspNetCore.Mvc;
using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Models;
using Net_Core_WebApp.ViewModel;
using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Net_Core_WebApp.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Dependency injection for interacting with the file system for user data.
        /// </summary>
        private readonly IJsonFileWrapper _fileWrapper;

        /// <summary>
        /// The filename used to store and retrieve user data in JSON format.
        /// </summary>
        private readonly string fileName = "UserData.json";

        /// <summary>
        /// Logger for recording errors and events.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor for HomeController. Injected dependencies are used for file access and logging.
        /// </summary>
        /// <param name="logger">The logger instance to use.</param>
        /// <param name="fileWrapper">The file wrapper for interacting with the file system.</param>
        public HomeController(ILogger<HomeController> logger, IJsonFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
            _logger = logger;
        }


        /// <summary>
        /// GET request handler for the Index action.
        /// This method retrieves a PersonViewModel object and passes it to the view.
        /// </summary>
        /// <returns>An IActionResult representing the Index view with the PersonViewModel.</returns>
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Create a new PersonViewModel with an empty SuccessErrorMessageModel
                PersonViewModel model = new PersonViewModel { successErrorMessage = new SuccessErrorMessageModel() };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// POST request handler for the Submit action.
        /// This method handles form submissions, validates the model, saves data to a JSON file, 
        /// and displays a success message upon successful save.
        /// </summary>
        /// <param name="_model">The PersonViewModel object submitted from the form.</param>
        /// <returns>An IActionResult representing the Index view with the updated PersonViewModel 
        /// containing the success message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(PersonViewModel _model)
        {
            try
            {

                // Check if the model state is valid (assuming data annotations for validation)
                if (!ModelState.IsValid)
                {
                    return View("Index", _model); // Return to Index view with same model for error display
                }

                // Clear any previous model state errors (optional)
                ModelState.Clear();

                // Retrieve the list of users from the JSON file
                List<PersonModel> personList = _fileWrapper.GetUsersFromFile(fileName);

                // Add the submitted person to the user list
                personList.Add(_model.person);

                // Convert the updated user list to a JSON string
                string jsonString = JsonConvert.SerializeObject(personList);

                // Save the JSON string to the user data file
                _fileWrapper.WriteAllText(fileName, jsonString);

                // Create a new SuccessErrorMessageModel with success message
                _model.successErrorMessage = new SuccessErrorMessageModel();
                _model.successErrorMessage.IsErrored = false;
                _model.successErrorMessage.Message = $"{_model.person.FirstName} {_model.person.LastName} - saved successfully!";

                // Return to Index view with the updated PersonViewModel containing the success message
                return View("Index", _model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

    }
}
