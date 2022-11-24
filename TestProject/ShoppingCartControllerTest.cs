using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting_APiSample.Controllers;

namespace TestProject
{
    public class ShoppingCartControllerTest
    {
        private readonly ShoppingCartController controller;
        private readonly IShoppingCartService service;

        public ShoppingCartControllerTest()
        {
            this.service = new ShoppingCartServiceFake();
            this.controller = new ShoppingCartController(service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = this.controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = this.controller.GetAll() as OkObjectResult;

            //Assert
            var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = this.controller.GetById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = this.controller.GetById(testGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = this.controller.GetById(testGuid) as OkObjectResult;

            // Assert
            Assert.IsType<ShoppingItem>(okResult.Value);
            Asert.Equal(testGuid, (okResult.Value as ShoppingItem).Id);
        }
    }
}
