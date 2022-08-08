﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Mids_Reborn.Core;
using Mids_Reborn.Core.Base.Master_Classes;

namespace Mids_Reborn
{
    public abstract class clsConvertibleUnitValue
    {
        public static string FormatSpeedUnit()
        {
            return FormatSpeedUnit(MidsContext.Config.SpeedFormat);
        }
        
        public static string FormatSpeedUnit(Enums.eSpeedMeasure speedUnit)
        {
            return speedUnit switch
            {
                Enums.eSpeedMeasure.FeetPerSecond => "ft/s",
                Enums.eSpeedMeasure.MetersPerSecond => "m/s",
                Enums.eSpeedMeasure.MilesPerHour => "mph",
                Enums.eSpeedMeasure.KilometersPerHour => "km/h",
                _ => "ft/s"
            };
        }

        public static string FormatDistanceUnit()
        {
            return FormatDistanceUnit(MidsContext.Config.SpeedFormat);
        }

        public static string FormatDistanceUnit(Enums.eSpeedMeasure distanceUnit)
        {
            return distanceUnit switch
            {
                Enums.eSpeedMeasure.FeetPerSecond => "ft",
                Enums.eSpeedMeasure.MetersPerSecond => "m",
                Enums.eSpeedMeasure.MilesPerHour => "ft",
                Enums.eSpeedMeasure.KilometersPerHour => "m",
                _ => "ft"
            };
        }

        public static string FormatValue(int formatType, float value)
        {
            return formatType switch
            {
                0 => $"{value:##0.##}%", // Percentage
                1 => $"{value:##0.##}", // Numeric, 2 decimals
                2 => (value > 0 ? "+" : "") + $"{value:##0.##}", // Numeric, 2 decimals, with sign
                3 => $"{Math.Abs(value):##0.##}", // Numeric, 2 decimals (for mez protection)
                4 => $"{value:##0.##}/s", // Numeric, 2 decimals, per second
                5 => $"{value:##0.##} {FormatSpeedUnit()}", // Movement, speed
                6 => $"{value:##0.##} {FormatDistanceUnit()}", // Movement, distance
                7 => (value > 0 ? "+" : "") + $"{value:##0.##}%", // Percentage, 2 decimals, with sign
                _ => $"{value:##0.##}"
            };
        }

        public static string FormatValue(int formatType, string valueText)
        {
            // Required for designer-set values
            Regex r = new Regex(@"[^0-9\.\-]");
            return FormatValue(formatType, Convert.ToSingle(r.Replace(valueText, ""), CultureInfo.InvariantCulture.NumberFormat));
        }
    }
}