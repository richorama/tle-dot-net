[![Build status](https://ci.appveyor.com/api/projects/status/r7dnhpxe5rk86ff0/branch/master?svg=true)](https://ci.appveyor.com/project/richorama/tle-dot-net/branch/master)

# TLE Parser for .NET

Two-line element parser.

A two-line element set (TLE) is a data format used to convey sets of orbital elements that describe the orbits of Earth-orbiting satellites.

For more information on the data structure, please consult [NASA Human Space Flight](https://spaceflight.nasa.gov/realdata/sightings/SSapplications/Post/JavaSSOP/SSOP_Help/tle_def.html)

## Installation

Use the [nuget package](https://www.nuget.org/packages/TLEParser/):

```
> Install-Package TLEParser
```

## Usage

```c#
var iss = @"ISS (ZARYA)             
1 25544U 98067A   18298.51635846  .00001514  00000-0  67960-4 0  9998
2 25544  51.6406  89.2479 0003892 336.3122 134.3245 15.53861856138782";

var tle = TLE.Parse(iss);

Console.WriteLine(tle.Name);
//ISS (ZARYA)
```

## License

MIT
