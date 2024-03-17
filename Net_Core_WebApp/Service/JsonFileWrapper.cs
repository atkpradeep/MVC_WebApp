using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Models;
using Newtonsoft.Json;

namespace Net_Core_WebApp.Service
{

    public class JsonFileWrapper : IJsonFileWrapper
    {
        /// <summary>
        /// Logger for recording errors and events related to file operations.
        /// </summary>
        private readonly ILogger<JsonFileWrapper> logger;

        /// <summary>
        /// Constructor for JsonFileWrapper. Injected dependency is used for logging.
        /// </summary>
        /// <param name="logger">The logger instance to use.</param>
        public JsonFileWrapper(ILogger<JsonFileWrapper> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Writes the provided content to the specified file path.
        /// </summary>
        /// <param name="path">The path to the file to write to.</param>
        /// <param name="content">The content to write to the file.</param>
        /// <exception cref="Exception">Thrown if an error occurs while writing the file.</exception>
        public void WriteAllText(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString()); // Log the error message with details
                throw ex;
            }
        }

        /// <summary>
        /// Reads a list of PersonModel objects from the specified JSON file.
        /// </summary>
        /// <param name="path">The path to the JSON file containing user data.</param>
        /// <returns>A list of PersonModel objects if the file exists and can be parsed, 
        /// otherwise an empty list.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while reading the file or parsing the JSON data.</exception>
        public List<PersonModel> GetUsersFromFile(string path)
        {
            try
            {
                string jsonData = File.ReadAllText(path);
                if (string.IsNullOrEmpty(jsonData))
                {
                    return new List<PersonModel>(); // Return empty list if file is empty
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<PersonModel>>(jsonData);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString()); // Log the error message with details
                throw ex;
            }
        }
    }
}
