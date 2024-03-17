using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Net_Core_WebApp.Contract;
using Net_Core_WebApp.Controllers;
using Net_Core_WebApp.Models;
using Net_Core_WebApp.ViewModel;

namespace ASPNetCoreWebApp_Test
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IJsonFileWrapper> _mockFileWrapper;
        private HomeController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockFileWrapper = new Mock<IJsonFileWrapper>();
            _controller = new HomeController(null, _mockFileWrapper.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewWithPersonViewModel()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOfType(viewResult.Model, typeof(PersonViewModel));
        }

        //Success Scenario
        [TestMethod]
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
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            Assert.AreEqual("Index", viewResult.ViewName); // Ensure view name is correct

            // Assert model state is valid
            Assert.IsTrue(controller.ModelState.IsValid);

            // Assert data is saved (verify with mock)
            mockFileWrapper.Verify(f => f.WriteAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            // Assert success message in view model
            var returnedModel = (PersonViewModel)viewResult.Model;
            Assert.IsFalse(returnedModel.successErrorMessage.IsErrored);
            Assert.IsNotNull(returnedModel.successErrorMessage.Message);
        }
    }
}