using OpenCvSharp;
using System;
using System.Globalization;
using System.Windows.Data;

namespace TextToSpeech.Utility.Converters
{
	public class FlagEnumConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
				return Binding.DoNothing;

			if (!value.GetType().IsEnum || !parameter.GetType().IsEnum)
				throw new ArgumentException("Value and parameter must be of Enum type.");

			var enumValue = (Enum)value;
			var flag = (Enum)parameter;

			return enumValue.HasFlag(flag);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
				return Binding.DoNothing;

			if (!targetType.IsEnum || !parameter.GetType().IsEnum)
				throw new ArgumentException("TargetType and parameter must be of Enum type.");

			var isChecked = (bool)value;
			var flag = (Enum)parameter;
			var enumValue = (Enum)Enum.ToObject(targetType, 0);

			if (isChecked)
			{
				enumValue = (Enum)Enum.ToObject(targetType, System.Convert.ToInt32(enumValue) | System.Convert.ToInt32(flag));
			}
			else
			{
				enumValue = (Enum)Enum.ToObject(targetType, System.Convert.ToInt32(enumValue) & ~System.Convert.ToInt32(flag));
			}
			return enumValue;
		}
	}
	public class FlagEnumMultiValueConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values[0] is ThresholdTypes enumValue && values[1] is ThresholdTypes flag)
			{
				return enumValue.HasFlag(flag);
			}
			return false;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			if (value is bool isChecked && parameter is ThresholdTypes flag)
			{
				var enumValue = (ThresholdTypes)targetTypes[0];
				if (isChecked)
				{
					return new object[] { enumValue | flag };
				}
				else
				{
					return new object[] { enumValue & ~flag };
				}
			}
			return new object[] { Binding.DoNothing };

		}
	}


	//public class FlagEnumMultiConverter : IMultiValueConverter
	//{
	//	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		if (values.Length < 2 || !(values[0] is Enum currentFlags) || !(values[1] is Enum flag))
	//			return Binding.DoNothing;

	//		return currentFlags.HasFlag(flag);
	//	}

	//	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	//	{
	//		if (targetTypes.Length < 2 || !(targetTypes[0] is Type enumType) || !enumType.IsEnum)
	//			return new object[] { Binding.DoNothing };

	//		var currentFlags = (Enum)Enum.ToObject(enumType, 0);
	//		if (targetTypes[1] == typeof(Enum))
	//		{
	//			currentFlags = (Enum)parameter;
	//		}

	//		var isChecked = (bool)value;
	//		var flag = (Enum)parameter;

	//		if (isChecked)
	//		{
	//			return new object[] { Enum.ToObject(enumType, System.Convert.ToInt32(currentFlags) | System.Convert.ToInt32(flag)), Binding.DoNothing };
	//		}
	//		else
	//		{
	//			return new object[] { Enum.ToObject(enumType, System.Convert.ToInt32(currentFlags) & ~System.Convert.ToInt32(flag)), Binding.DoNothing };
	//		}
	//	}
	//}

	/// <summary>
	/// Provides for two way binding between a TestErrors Flag Enum property and a boolean value.
	/// TODO: make this more generic and add it to the converter dictionary if possible
	/// </summary>
	//public class TestActionFlagValueConverter : IValueConverter
	//{
	//	private ThresholdTypes target;

	//	public TestActionFlagValueConverter()
	//	{

	//	}

	//	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		ThresholdTypes mask = (ThresholdTypes)parameter;
	//		this.target = (ThresholdTypes)value;
	//		return ((mask & this.target) != 0);
	//	}

	//	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	//	{
	//		this.target ^= (ThresholdTypes)parameter;
	//		return this.target;
	//	}
	//}
}
