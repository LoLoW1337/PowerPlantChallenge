using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Models;
using Engie.Powerplant.Lorenzo.Business.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Engie.Powerplant.lorenzo.Tests.Services
{
    public class ProductionplanServiceTests
    {
        [Fact]
        public async void CalculateUnitOfCommitment_HappyFlow()
        {
            //Arrange
            var powerplants = new List<PowerplantModel>();
            var fuels = BuildFuels();
            var sut = new ProductionplanService();
            var load = 480;

            //Act
            var actualResults = await sut.CalculateUnitOfCommitment(powerplants, load, fuels);

            //Assert
            foreach (var actualResult in actualResults)
            {
                Assert.Equal(actualResult.P, ExpectedResult()
                    .Where(x => x.Name == actualResult.Name)
                    .Select(x => x.P).SingleOrDefault());
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
                    P = 368.4m
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
                    P = 21.6m
                },
                new PowerplantModel
                {
                    Name = "windpark2",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36,
                    P = 90
                },
            };
        }
    }
}
