using Microsoft.VisualStudio.TestTools.UnitTesting;
using TLEParser;
using System.Threading.Tasks;
using System;

namespace TLEParser.Tests
{
  [TestClass]
  public class TLEParserTests
  {
    [TestMethod]
    public void ParseBasicString()
    {
      var iss = @"ISS (ZARYA)             
1 25544U 98067A   18298.51635846  .00001514  00000-0  67960-4 0  9998
2 25544  51.6406  89.2479 0003892 336.3122 134.3245 15.53861856138782";

      var tle = TLEParser.TLE.Parse(iss);

      Assert.AreEqual("ISS (ZARYA)", tle.Name);
      Assert.AreEqual("98067A", tle.InternationalDesignator);
      Assert.AreEqual(0.00001514m, tle.FirstDerivativeOfMeanMotion);
      Assert.AreEqual(0.0m, tle.SecondDerivativeOfMeanMotion);
      Assert.AreEqual(0.00006796m, tle.Drag);
      Assert.AreEqual(0, tle.EphemerisType);
      Assert.AreEqual(25544, tle.Number);
      Assert.AreEqual(51.6406m, tle.Inclination);
      Assert.AreEqual(89.2479m, tle.RightAscensionOfTheAscendingNode);
      Assert.AreEqual(0.0003892m, tle.Eccentricity);
      Assert.AreEqual(336.3122m, tle.ArgumentOfPerigee);
      Assert.AreEqual(134.3245m, tle.MeanAnomaly);
      Assert.AreEqual(15.53861856m, tle.MeanMotion);
      Assert.AreEqual(13878, tle.Revolutions);
    }

    public void ParseExampleFromGitHub()
    {
      var lines = new string[]{
                "ISS (ZARYA)",
                "1 25544U 98067A   08264.51782528 -.00002182  00000-0 -11606-4 0  2927",
                "2 25544  51.6416 247.4627 0006703 130.5360 325.0288 15.72125391563537"
            };

      var tle = TLEParser.TLE.Parse(lines[0], lines[1], lines[2]);

      Assert.AreEqual("ISS (ZARYA)", tle.Name);
      Assert.AreEqual("98067A", tle.InternationalDesignator);
      Assert.AreEqual(-0.00002182m, tle.FirstDerivativeOfMeanMotion);
      Assert.AreEqual(0.0m, tle.SecondDerivativeOfMeanMotion);
      Assert.AreEqual(-0.000011606m, tle.Drag);
      Assert.AreEqual(new DateTime(2008, 8, 20, 12, 2, 40, 1040), tle.Date);
      Assert.AreEqual(0, tle.EphemerisType);
      Assert.AreEqual(25544, tle.Number);
      Assert.AreEqual(51.6416m, tle.Inclination);
      Assert.AreEqual(247.4627m, tle.RightAscensionOfTheAscendingNode);
      Assert.AreEqual(0.0006703m, tle.Eccentricity);
      Assert.AreEqual(130.536m, tle.ArgumentOfPerigee);
      Assert.AreEqual(325.0288m, tle.MeanAnomaly);
      Assert.AreEqual(15.721253915m, tle.MeanMotion);
      Assert.AreEqual(56353, tle.Revolutions);
    }

    [TestMethod]
    public void TestParseNum()
    {
      Assert.AreEqual(0.00001514m, TLEParser.TLE.ParseNum(".00001514"));
      Assert.AreEqual(-0.02m, TLEParser.TLE.ParseNum("-.02"));
    }

    [TestMethod]
    public void TestParseNumWithPower()
    {
      Assert.AreEqual(0.0m, TLEParser.TLE.ParseNumWithPower("0.0000-0"));
      Assert.AreEqual(0.01m, TLEParser.TLE.ParseNumWithPower("1.0000-2"));
    }


  }
}
