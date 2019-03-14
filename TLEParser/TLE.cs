using System;
using System.Linq;

namespace TLEParser
{
  public class TLE
  {
    private TLE()
    { }

    public string Name { get; private set; }
    public int Number { get; private set; }
    public string InternationalDesignator { get; private set; }
    public decimal FirstDerivativeOfMeanMotion { get; private set; }
    public decimal SecondDerivativeOfMeanMotion { get; private set; }
    public decimal Drag { get; private set; }
    public decimal EphemerisType { get; private set; }
    public decimal Inclination { get; private set; }
    public decimal RightAscensionOfTheAscendingNode { get; private set; }
    public decimal Eccentricity { get; private set; }
    public decimal ArgumentOfPerigee { get; private set; }
    public decimal MeanAnomaly { get; private set; }
    public decimal MeanMotion { get; private set; }
    public DateTime Date { get; private set; }
    public int Revolutions { get; private set; }

    public static TLE Parse(string line0, string line1, string line2)
    {
      var tle = new TLE();

      // line 0
      tle.Name = line0.Trim();

      // line 1
      var line1Number = line1.Substring(0, 1);
      if (line1Number != "1") throw new FormatException("Line 1 should start with a '1'");

      tle.InternationalDesignator = line1.Substring(9, 8).Trim();

      var twoDigitYear = int.Parse(line1.Substring(18, 2).Trim());
      var dayFraction = ParseNum(line1.Substring(20, 12));
      var days = Math.Floor(dayFraction);
      var hoursFraction = 24 * (dayFraction - days);
      var hours = Math.Floor(hoursFraction);
      var minutesFraction = 60 * (hoursFraction - hours);
      var minutes = Math.Floor(minutesFraction);
      var secondsFraction = 60 * (minutesFraction - minutes);
      var seconds = Math.Floor(secondsFraction);
      var milliseconds = 1000 * (secondsFraction - seconds);

      tle.Date = new DateTime(2000 + twoDigitYear, 1, 1)
          .AddDays((int)days)
          .AddHours((int)hours)
          .AddMinutes((int)minutes)
          .AddSeconds((int)seconds)
          .AddMilliseconds((int)milliseconds);

      tle.FirstDerivativeOfMeanMotion = ParseNum(line1.Substring(33, 10));
      tle.SecondDerivativeOfMeanMotion = ParseNumWithPower("0." + line1.Substring(44, 6).Trim());
      tle.Drag = ParseNumWithPower("0." + line1.Substring(53, 8).Trim());
      tle.EphemerisType = int.Parse(line1.Substring(62, 2).Trim());

      // line 2
      var line2Number = line2.Substring(0, 1);
      if (line2Number != "2") throw new FormatException("Line 2 should start with a '2'");

      tle.Number = int.Parse(line2.Substring(2, 6).Trim());
      tle.Inclination = ParseNum(line2.Substring(9, 7));
      tle.RightAscensionOfTheAscendingNode = ParseNum(line2.Substring(17, 8));
      tle.Eccentricity = ParseNum("0." + line2.Substring(26, 7));
      tle.ArgumentOfPerigee = ParseNum(line2.Substring(34, 8));
      tle.MeanAnomaly = ParseNum(line2.Substring(43, 8));
      tle.MeanMotion = ParseNum(line2.Substring(52, 11));
      tle.Revolutions = int.Parse(line2.Substring(63, 5).Trim());

      return tle;
    }

    public static TLE Parse(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));

      var lines = value.Split('\n').Select(x => x.Trim()).ToArray();

      if (lines.Length != 3) throw new FormatException("Expected 3 lines");

      return Parse(lines[0], lines[1], lines[2]);
    }

    public static decimal ParseNum(string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return 0m;
      return decimal.Parse(value.Trim());
    }

    public static decimal ParseNumWithPower(string value)
    {
      var num = ParseNum(value.Substring(0, value.Length - 2));
      var pow = ParseNum(value.Substring(value.Length - 2, 2));

      return num * (decimal)Math.Pow(10, (double)pow);
    }

  }
}
