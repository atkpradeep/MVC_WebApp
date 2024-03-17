using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Controllers;
using Net_Core_WebApp.Models;
using Net_Core_WebApp.ViewModel;

namespace Net_Core_WebApp_Test
{
    public class HomeControllerTests
    {
        private Mock<IJsonFileWrapper> _mockFileWrapper;
        private HomeController _controller;

        [Fact]
        public void Index_ReturnsViewWithPersonViewModel()
        {
            // Arrange
            _mockFileWrapper = new Mock<IJsonFileWrapper>();
            _controller = new HomeController(null, _mockFileWrapper.Object);

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType(typeof(ViewResult), result);
            var viewResult = (ViewResult)result;
            Assert.IsType(typeof(PersonViewModel), viewResult.Model);
        }

        [Fact]
        public void Submit_ValidModel_SavesDataAndReturnsViewWithSuccessMessage()
        {
            // Arrange
            var mockFileWrapper = new Mock<IJsonFileWrapper>();
            mockFileWrapper.Setup(f => f.GetUsersFromFile(It.IsAny<string>()))
                .Returns(new List<PersonModel>()); // Simulate empty list
            var controller = new HomeController(null, mockFileWrapper.Object);

            var personViewModel = new PersonViewModel
            {
                person = new PersonModel { FirstName = "Pradeep", LastName = "Atkari" },
                successErrorMessage = new SuccessErrorMessageModel()
            };

            // Act
            var result = controller.Submit(personViewModel);

            // Assert
            Assert.IsType(typeof(ViewResult), result);
            var viewResult = (ViewResult)result;
            Assert.Equal("Index", viewResult.ViewName); // Ensure view name is correct

            // Assert data is saved (verify with mock)
            mockFileWrapper.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            // Assert success message in view model
            var returnedModel = (PersonViewModel)viewResult.Model;
            Assert.False(returnedModel.successErrorMessage.IsErrored);
            Assert.NotNull(returnedModel.successErrorMessage.Message);
        }

        
    }

    public interface IModelValidator
    {
        void ValidateModel(PersonViewModel model, ModelStateDictionary modelState);
    }
}