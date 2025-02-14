namespace Crank.Result
{
	public static class ResultExtensions
	{
		public static (bool, TPrimaryValue, string?) Deref<TPrimaryValue>(this Result<TPrimaryValue> result, bool failIfValueIsNull = true)
		{
			var (succeeded, value, message) = result;
			return (failIfValueIsNull ? succeeded && value == null : succeeded, value ?? default!, message);
		}

		public static (bool, TSecondaryValue, string?) Deref<TPrimaryValue, TSecondaryValue>(this Result<TPrimaryValue, TSecondaryValue> result, bool failIfValueIsNull = true)
		{
			var (succeeded, value, message) = result;
			return (failIfValueIsNull ? succeeded && value == null : succeeded, value ?? default!, message);
		}

		public static async Task<(bool, TPrimaryValue, string?)> Deref<TPrimaryValue>(this Task<Result<TPrimaryValue>> result, bool failIfValueIsNull = true)
		{
			var (succeeded, value, message) = await result;
			return (failIfValueIsNull ? succeeded && value == null : succeeded, value ?? default!, message);
		}

		public static async Task<(bool, TSecondaryValue, string?)> Deref<TPrimaryValue, TSecondaryValue>(this Task<Result<TPrimaryValue, TSecondaryValue>> result, bool failIfValueIsNull = true)
		{
			var (succeeded, value, message) = await result;
			return (failIfValueIsNull ? succeeded && value == null : succeeded, value ?? default!, message);
		}
	}

}
