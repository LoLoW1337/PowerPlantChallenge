using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Business.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Engie.Powerplant.lorenzo.Tests.Services
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
            var powerplants = GetPowerplantModels();
            var fuels = BuildFuels();
            var sut = new ProductionplanService();
            var load = 480;

            //Act
            var actualResults = await sut.CalculateUnitOfCommitment(powerplants, load, fuels);

            //Assert
            Assert.Equal(load, actualResults.Sum(x => x.P));
            foreach (var actualResult in actualResults)
            {
                output.WriteLine($"{actualResult.Name} : {actualResult.P} power");
                Assert.Equal(ExpectedResult()
                    .Where(x => x.Name == actualResult.Name)
                    .Select(x => x.P).SingleOrDefault(), actualResult.P);
            }
        }

        private FuelsModel BuildFuels()
        {
            return new FuelsModel
            {
                Gas = 13.4m,
                Kerosine = 50.8m,
                Co2 = 20,
                Wind = 60
            };
        }

        private static List<PowerplantModel> GetPowerplantModels()
        {
            return new List<PowerplantModel>
            {
                new PowerplantModel
                {
                    Name = "gasfiredbig1",
                    Type = PowerplantType.Gasfired,
                    Efficiency = 0.53m,
                    Pmin = 100,
                    Pmax = 460,
                },
                new PowerplantModel
                {
                     Name = "gasfiredbig2",
                     Type = PowerplantType.Gasfired,
                     Efficiency = 0.53m,
                     Pmin = 100,
                     Pmax = 460,
                 },
                new PowerplantModel
                {
                      Name = "gasfiredsomewhatsmaller",
                      Type = PowerplantType.Gasfired,
                      Efficiency =  0.37m,
                      Pmin = 40,
                      Pmax = 210,
                },
                new PowerplantModel
                {
                    Name = "tj1",
                    Type = PowerplantType.Turbojet,
                    Efficiency = 0.3m,
                    Pmin = 0,
                    Pmax = 16,
                },
                new PowerplantModel
                {
                    Name = "windpark1",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 150,
                },
                new PowerplantModel
                {
                    Name = "windpark2",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36,
                },
            };
        }

        private static List<PowerplantModel> ExpectedResult()
        {
            return new List<PowerplantModel>
            {
                new PowerplantModel
                {
                    Name = "gasfiredbig1",
                    Type = PowerplantType.Gasfired,
                    Efficiency = 0.53m,
                    Pmin = 100,
                    Pmax = 460,
                    P = 368
                },
                new PowerplantModel
                {
                    Name = "gasfiredbig2",
                    Type = PowerplantType.Gasfired,
                    Efficiency = 0.53m,
                    Pmin = 100,
                    Pmax = 460,
                    P = 0
                 },
                new PowerplantModel
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = PowerplantType.Gasfired,
                    Efficiency =  0.37m,
                    Pmin = 40,
                    Pmax = 210,
                    P = 0
                },
                new PowerplantModel
                {
                    Name = "tj1",
                    Type = PowerplantType.Turbojet,
                    Efficiency = 0.3m,
                    Pmin = 0,
                    Pmax = 16,
                    P = 0
                },
                new PowerplantModel
                {
                    Name = "windpark1",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 150,
                    P = 90
                },
                new PowerplantModel
                {
                    Name = "windpark2",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36,
                    P = 22
                },
            };
        }
    }
}
