﻿using Engie.Powerplant.lorenzo.Tests.Core;
using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Interfaces;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Business.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Engie.Powerplant.lorenzo.Tests.Integration
{
    public class ProductionplanServiceTests
    {

        private readonly ITestOutputHelper output;

        public ProductionplanServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void CalculateUnitOfCommitment_HappyFlow()
        {
            //Arrange
            var powerplants = PowerplantFixture.GetPowerplantModels();
            var fuels = PowerplantFixture.BuildFuels();
            var load = 480;

            var mockMeritOrderService = new Mock<IMeritOrderService>();
            mockMeritOrderService
                .Setup(m => m.SetMeritOrder(powerplants, fuels))
                .ReturnsAsync(powerplants.OrderBy(p => p.MeritOrder).ToList());

            var sut = new ProductionplanService(mockMeritOrderService.Object);

            //Act
            var actualResults = await sut.CalculateUnitOfCommitment(powerplants, load, fuels);

            //Assert
            Assert.Equal(load, actualResults.Sum(x => x.P));
            foreach (var actualResult in actualResults)
            {
                output.WriteLine($"{actualResult.Name} : {actualResult.P} power");
                Assert.Equal(PowerplantFixture.ExpectedResult()
                    .Where(x => x.Name == actualResult.Name)
                    .Select(x => x.P).SingleOrDefault(), actualResult.P);
            }
        }

        [Theory]
        [InlineData(400)]
        [InlineData(800)]
        [InlineData(256)]
        [InlineData(810)]
        [InlineData(333)]
        [InlineData(456)]
        [InlineData(140)]
        [InlineData(600)]
        public async void CalculateUnitOfCommitment_Sum_Of_Power_Should_Be_Equal_To_The_Load(int expectedLoad)
        {
            //Arrange
            var powerplants = PowerplantFixture.GetPowerplantModels();
            var fuels = PowerplantFixture.BuildFuels();

            var mockMeritOrderService = new Mock<IMeritOrderService>();
            mockMeritOrderService
                .Setup(m => m.SetMeritOrder(powerplants, fuels))
                .ReturnsAsync(powerplants.OrderBy(p => p.MeritOrder).ToList());

            var sut = new ProductionplanService(mockMeritOrderService.Object);

            //Act
            var actualResults = await sut.CalculateUnitOfCommitment(powerplants, expectedLoad, fuels);

            //Assert
            Assert.Equal(expectedLoad, actualResults.Sum(x => x.P));         
        }
    }
}