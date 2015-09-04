﻿using MvcTemplate.Controllers;
using MvcTemplate.Services;
using MvcTemplate.Validators;
using NSubstitute;
using System;
using Xunit;

namespace MvcTemplate.Tests.Unit.Controllers
{
    public class ValidatedControllerTests : AControllerTests
    {
        private ValidatedController<IValidator, IService> controller;
        private IValidator validator;
        private IService service;

        public ValidatedControllerTests()
        {
            service = Substitute.For<IService>();
            validator = Substitute.For<IValidator>();
            controller = Substitute.ForPartsOf<ValidatedController<IValidator, IService>>(validator, service);
        }

        #region Constructor: ValidatedController(TService service, TValidator validator)

        [Fact]
        public void ValidatedController_SetsValidator()
        {
            Object actual = controller.Validator;
            Object expected = validator;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ValidatedController_SetsValidatorAlerts()
        {
            Object expected = controller.Alerts;
            Object actual = validator.Alerts;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ValidatedController_SetsModelState()
        {
            Object expected = controller.ModelState;
            Object actual = validator.ModelState;

            Assert.Same(expected, actual);
        }

        #endregion

        #region Method: OnActionExecuting(ActionExecutingContext filterContext)

        [Fact]
        public void OnActionExecuting_SetsServiceCurrentAccountId()
        {
            ReturnsCurrentAccountId(controller, "Test");

            controller.OnActionExecuting(null);

            String expected = controller.CurrentAccountId;
            String actual = service.CurrentAccountId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OnActionExecuting_SetsValidatorCurrentAccountId()
        {
            ReturnsCurrentAccountId(controller, "Test");

            controller.OnActionExecuting(null);

            String expected = controller.CurrentAccountId;
            String actual = validator.CurrentAccountId;

            Assert.Equal(expected, actual);
        }

        #endregion

        #region Method: Dispose()

        [Fact]
        public void Dispose_DisposesValidatorAndService()
        {
            controller.Dispose();

            service.Received().Dispose();
            validator.Received().Dispose();
        }

        [Fact]
        public void Dispose_CanBeCalledMultipleTimes()
        {
            controller.Dispose();
            controller.Dispose();
        }

        #endregion
    }
}
