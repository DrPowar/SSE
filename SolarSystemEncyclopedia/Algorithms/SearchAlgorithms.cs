using SolarSystemEncyclopedia.Models;
using SolarSystemEncyclopedia.ViewModels;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SolarSystemEncyclopedia.Algorithms
{
    public static class SearchAlgorithms
    {
        public static (int value, bool isExponent) ParseScientificNotation(string value)
        {
            value = value.Trim(';');
            int intValue;

            if(int.TryParse(value, out intValue))
            {
                return (intValue, false);
            }

            string[] parts = value.Split(new string[] { "×10^" }, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid scientific notation: " + value);
            }

            int exponent = int.Parse(parts[1]);

            return (exponent, true);
        }

        public static string[] SplitSearchString(string input)
        {
            string[] parts = input.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim() + ";";
            }

            return parts;
        }

        public static (string Field, string Operator, string Value) ParseFilter(string filter)
        {
            var parts = filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid filter format. Expected format: 'Field Operator Value'");
            }

            string field = parts[0];
            string operatorSymbol = parts[1];
            string value = parts[2];

            return (field, operatorSymbol, value);
        }

        public static bool Compare(double target, string operatorSymbol, double value, bool isExponentTarget, bool isExponentValue)
        {
            double adjustedTarget = target;
            double adjustedValue = value;

            if (isExponentValue)
            {
                adjustedTarget = Math.Pow(10, target);
            }

            if (isExponentTarget)
            {
                adjustedValue = Math.Pow(10, value);
            }

            switch (operatorSymbol)
            {
                case ">":
                    return adjustedTarget > adjustedValue;
                case "<":
                    return adjustedTarget < adjustedValue;
                case "=":
                    return adjustedTarget == adjustedValue;
                default:
                    throw new ArgumentException("Invalid operator.");
            }
        }

        public static string ModelTypeFilter(string filter)
        {
            filter = filter.Trim(';');
            switch (filter)
            {
                case "Star":
                    return "Star";
                case "Planet":
                    return "Planet";
                case "Moon":
                    return "Moon";
                default:
                    return "";
            }

        }
    }
}
