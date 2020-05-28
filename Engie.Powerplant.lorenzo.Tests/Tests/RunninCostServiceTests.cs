using Engie.Powerplant.lorenzo.Tests.Core;
using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Business.Services;
using Xunit;
using Xunit.Abstractions;

namespace Engie.Powerplant.lorenzo.Tests.Tests
{
    public class RunninCostServiceTests
    {
        private readonly ITestOutputHelper output;

        public RunninCostServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData(PowerplantType.Gasfired, 460, 2760)]
        [InlineData(PowerplantType.Windturbine, 150, 0)]
        [InlineData(PowerplantType.Turbojet, 16, 96)]
        public void CalculateCO2EmissionCost_HappyFlow(PowerplantType powerplantType, int powerProduced ,decimal expextedCo2Cost )
        {
            //Arrange
            var powerplant = new PowerplantModel
            {
                P = powerProduced,
                Type = powerplantType
            };

            var fuels = PowerplantFixture.BuildFuels();

            var sut = new RunningCostService();

            //Act
            sut.CalculateCO2EmissionCost(powerplant, fuels);

            //Assert
            Assert.Equal(expextedCo2Cost, powerplant.CO2CostEmission);
        }

        [Theory]
        [InlineData(PowerplantType.Gasfired, 460, 2760, 0.53, 14390.19)]
        [InlineData(PowerplantType.Windturbine, 150, 0, 1, 0)]
        [InlineData(PowerplantType.Turbojet, 16, 96, 0.3, 2805.33)]
        public void CalculateRunningCost_HappyFlow(PowerplantType powerplantType, int powerProduced, decimal co2CostEmmission, decimal efficiency, decimal expectedRunningCost)
        {
            //Arrange
            var powerplant = new PowerplantModel
            {
                P = powerProduced,
                Type = powerplantType,
                Efficiency = efficiency,
                CO2CostEmission = co2CostEmmission
            };

            var fuels = PowerplantFixture.BuildFuels();

            var sut = new RunningCostService();

            //Act
            sut.CalculateRunningCost(powerplant, fuels);

            //Assert
            Assert.Equal(expectedRunningCost, powerplant.RunningCost);
        }
    }
}
