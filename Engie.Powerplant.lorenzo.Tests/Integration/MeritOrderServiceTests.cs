using Engie.Powerplant.lorenzo.Tests.Core;
using Engie.Powerplant.Lorenzo.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Engie.Powerplant.lorenzo.Tests.Integration
{
    public class MeritOrderServiceTests
    {
        private readonly ITestOutputHelper output;

        public MeritOrderServiceTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public async void SetMeritOrder_HappyFlow()
        {
            //Arrange
            var powerplants = PowerplantFixture.GetPowerplantModels();
            var fuels = PowerplantFixture.BuildFuels();

            var sut = new MeritOrderService();

            //Act
            var actualResults = await sut.SetMeritOrder(powerplants, fuels);

            //Assert
            foreach (var actualResult in actualResults)
            {
                Assert.NotEqual(0, actualResult.MeritOrder);
                output.WriteLine($"{actualResult.Name} : {actualResult.MeritOrder} position");
                Assert.Equal(PowerplantFixture.ExpectedResult()
                    .Where(x => x.Name == actualResult.Name)
                    .Select(x => x.MeritOrder).SingleOrDefault(), actualResult.MeritOrder);
            }
        }
    }
}
