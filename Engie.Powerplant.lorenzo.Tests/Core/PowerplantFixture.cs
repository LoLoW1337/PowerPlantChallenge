using Engie.Powerplant.Lorenzo.Business.Enums;
using Engie.Powerplant.Lorenzo.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engie.Powerplant.lorenzo.Tests.Core
{
    public class PowerplantFixture
    {
        public static List<PowerplantModel> GetPowerplantModels()
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
                    MeritOrder = 3
                },
                new PowerplantModel
                {
                     Name = "gasfiredbig2",
                     Type = PowerplantType.Gasfired,
                     Efficiency = 0.53m,
                     Pmin = 100,
                     Pmax = 460,
                     MeritOrder = 4
                 },
                new PowerplantModel
                {
                      Name = "gasfiredsomewhatsmaller",
                      Type = PowerplantType.Gasfired,
                      Efficiency =  0.37m,
                      Pmin = 40,
                      Pmax = 210,
                      MeritOrder = 5
                },
                new PowerplantModel
                {
                    Name = "tj1",
                    Type = PowerplantType.Turbojet,
                    Efficiency = 0.3m,
                    Pmin = 0,
                    Pmax = 16,
                    MeritOrder = 6
                },
                new PowerplantModel
                {
                    Name = "windpark1",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 150,
                    MeritOrder = 1
                },
                new PowerplantModel
                {
                    Name = "windpark2",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36,
                    MeritOrder = 2
                },
            };
        }

        public static List<PowerplantModel> ExpectedResult()
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
                    P = 368,
                    MeritOrder = 3
                },
                new PowerplantModel
                {
                    Name = "gasfiredbig2",
                    Type = PowerplantType.Gasfired,
                    Efficiency = 0.53m,
                    Pmin = 100,
                    Pmax = 460,
                    P = 0,
                    MeritOrder = 4
                 },
                new PowerplantModel
                {
                    Name = "gasfiredsomewhatsmaller",
                    Type = PowerplantType.Gasfired,
                    Efficiency =  0.37m,
                    Pmin = 40,
                    Pmax = 210,
                    P = 0,
                    MeritOrder = 5
                },
                new PowerplantModel
                {
                    Name = "tj1",
                    Type = PowerplantType.Turbojet,
                    Efficiency = 0.3m,
                    Pmin = 0,
                    Pmax = 16,
                    P = 0,
                    MeritOrder = 6
                },
                new PowerplantModel
                {
                    Name = "windpark1",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 150,
                    P = 90,
                    MeritOrder = 1
                },
                new PowerplantModel
                {
                    Name = "windpark2",
                    Type = PowerplantType.Windturbine,
                    Efficiency = 1,
                    Pmin = 0,
                    Pmax = 36,
                    P = 22,
                    MeritOrder = 2
                },
            };
        }
        public static FuelsModel BuildFuels()
        {
            return new FuelsModel
            {
                Gas = 13.4m,
                Kerosine = 50.8m,
                Co2 = 20,
                Wind = 60
            };
        }
    }
}
