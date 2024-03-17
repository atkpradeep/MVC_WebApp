using Net_Core_WebApp.Models;

namespace Net_Core_WebApp.Contract
{
    public interface IJsonFileWrapper
    {
        /// <summary>
        /// Save list of person object to Json file
        /// </summary>
        /// <param name="path">Json file name / path</param>
        /// <param name="content">Json string content</param>
        void WriteAllText(string path, string content);

        /// <summary>
        /// Get existing person list from json file
        /// </summary>
        /// <param name="path">Json file name / path</param>
        /// <returns>list of existing persons</returns>
        List<PersonModel> GetUsersFromFile(string path);
    }
}
