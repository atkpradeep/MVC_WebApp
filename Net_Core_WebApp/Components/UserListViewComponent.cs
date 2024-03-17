using Microsoft.AspNetCore.Mvc;
using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Models;

namespace Net_Core_WebApp.Components
{
    public class UserListViewComponent : ViewComponent
    {
        private readonly IJsonFileWrapper _fileWrapper;
        private readonly string fileName = "UserData.json";
        public UserListViewComponent(IJsonFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }
        public IViewComponentResult Invoke()
        {
            List<PersonModel> personList = _fileWrapper.GetUsersFromFile(fileName);
            return View(personList);
        }

    }
}
