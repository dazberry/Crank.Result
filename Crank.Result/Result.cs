using System.Runtime.CompilerServices;

namespace Crank.Result
{
	/// <summary>
	/// A "Basic Result" contains a succeeded/failed flag and an optional string message value.
	/// </summary>
	public class Result
	{
		public bool Succeeded { get; init; } = false;
		public bool Failed => !Succeeded;

		public string? Message { get; init; } = string.Empty;

		protected Result(bool succeeded, string? message = default)
		{
			Succeeded = succeeded;
			Message = message ?? string.Empty;
		}

		// Result res = value
		public static implicit operator Result(bool succeeded) => new(succeeded);


		// Result res = (value, message)
		public static implicit operator Result((bool succeeded, string message) tup) =>
			new(tup.succeeded, tup.message);

		// var (value, message) = res;
		public void Deconstruct(out bool succeeded, out string? message)
		{
			succeeded = Succeeded;
			message = Message;
		}

		//res == bool
		public static bool operator ==(Result result, bool value) =>
			result is not null && result.Succeeded == value;

		//res != bool
		public static bool operator !=(Result result, bool value) =>
			!(result.Succeeded == value);

		public static Result Success()
			=> new(true);

		public static Result Fail(string? message = default) =>
			(false, message ?? string.Empty);

		public static Result<TPrimaryValue> Fail<TPrimaryValue>(TPrimaryValue value, string? message = default) =>
			Result<TPrimaryValue>.Fail(value, message);


		public virtual bool TryGetValue<T>(out T? value)
		{
			value = default;
			return false;
		}

		public virtual T? As<T>() => TryGetValue(out T? value) ? value : default;


		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (ReferenceEquals(obj, null))
			{
				return false;
			}

			return obj is Result instance &&
				Succeeded == instance.Succeeded &&
				Message == instance.Message;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Succeeded, Message ?? string.Empty);
		}
	}

	/// <summary>
	/// A "Primary Value Result" contains an additional typed value in addition to the
	/// success/fail flag and message value. By default value assignment implies success.
	/// </summary>
	/// <typeparam name="TPrimaryValue"></typeparam>
	public class Result<TPrimaryValue> : Result
	{
		protected readonly TPrimaryValue? _primaryValue = default;

		public TPrimaryValue? Value => _primaryValue;


		protected Result(bool succeeded, TPrimaryValue? value = default, string? message = default)
			: base(succeeded, message)
		{
			_primaryValue = value;
		}

		// Result<T> res = value
		public static implicit operator Result<TPrimaryValue>(TPrimaryValue result) =>
			new(true, result);

		// Result res = (success, value)
		public static implicit operator Result<TPrimaryValue>((bool succeeded, TPrimaryValue value) tup) =>
			new(tup.succeeded, tup.value);

		// Result res = (success, value, message)
		public static implicit operator Result<TPrimaryValue>((bool succeeded, TPrimaryValue value, string message) tup) =>
			new(tup.succeeded, tup.value, tup.message);

		// res == value
		public static implicit operator TPrimaryValue(Result<TPrimaryValue> result) =>
			result.Value!;

		public void Deconstruct(out bool succeeded, out TPrimaryValue? value)
		{
			succeeded = Succeeded;
			value = Value;
		}

		public void Deconstruct(out bool succeeded, out TPrimaryValue? value, out string? message)
		{
			succeeded = Succeeded;
			value = Value;
			message = Message;
		}

		public static bool operator ==(Result<TPrimaryValue> result, bool value) =>
			result is not null && result.Succeeded == value;

		public static bool operator !=(Result<TPrimaryValue> result, bool value) =>
			!(result.Succeeded == value);

		public static Result<TPrimaryValue> Success(TPrimaryValue value) =>
			new(true, value);

		public static Result<TPrimaryValue> Fail(TPrimaryValue? value = default, string? message = default) =>
			new(false, value, message);

		public static new Result<TPrimaryValue, TSecondaryValue> Fail<TSecondaryValue>(TSecondaryValue secondaryValue, string? message = default) =>
			Result<TPrimaryValue, TSecondaryValue>.Fail(secondaryValue, message);

		public override bool TryGetValue<T>(out T? value)
			where T : default
		{
			if (this is Result<T> result)
			{
				value = result.Value;
				return true;
			}

			value = default!;
			return false;
		}

		public override T? As<T>() where T : default => TryGetValue(out T? value) ? value : default;

		public override bool Equals(object? obj)
		{
			return base.Equals(obj) &&
				obj is Result<TPrimaryValue> instance &&
				EqualityComparer<TPrimaryValue>.Default.Equals(this.Value, instance.Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Succeeded, Message ?? string.Empty, Value);
		}
	}

	/// <summary>
	/// A "Secondary Value Result" contains an additional typed value in addition to the
	/// success/fail flag and message value. By default value assignment implies failure.
	/// </summary>
	/// <typeparam name="TPrimaryValue"></typeparam>
	/// <typeparam name="TSecondaryValue"></typeparam>
	public class Result<TPrimaryValue, TSecondaryValue> : Result<TPrimaryValue>
	{
		public new TSecondaryValue? Value { get; init; }

		protected Result(bool succeeded, TSecondaryValue? secondaryValue = default, string? message = default) :
			base(succeeded, default!, message)
		{
			Value = secondaryValue;
		}


		protected Result(bool succeeded, TPrimaryValue? primaryValue, TSecondaryValue? secondaryValue, string? message = default) :
			base(succeeded, primaryValue, message)
		{
			Value = secondaryValue;
		}


		// Result<Guid, int> res = value
		public static implicit operator Result<TPrimaryValue, TSecondaryValue>(TSecondaryValue value) =>
			new(false, value, default!);

		// Result<Guid, int> res = (value, message)
		public static implicit operator Result<TPrimaryValue, TSecondaryValue>((TSecondaryValue secondaryValue, string message) tup) =>
			new(false, tup.secondaryValue, tup.message);

		// Result<Guid, int> res = (succeeded, secondaryValue, message)
		public static implicit operator Result<TPrimaryValue, TSecondaryValue>((bool succeeded, TSecondaryValue secondaryValue, string message) tup) =>
			new(tup.succeeded, tup.secondaryValue, tup.message);

		// Result<Guid, int> res = (succeeded, primaryValue, secondaryValue, message)
		public static implicit operator Result<TPrimaryValue, TSecondaryValue>((bool succeeded, TPrimaryValue primaryValue, TSecondaryValue secondaryValue, string message) tup) =>
			new(false, tup.primaryValue, tup.secondaryValue, tup.message);


		//var (succeeded, value) =
		public void Deconstruct(out bool succeeded, out TSecondaryValue? value)
		{
			succeeded = Succeeded;
			value = Value;
		}

		//var (succeeded, value, message) =
		public void Deconstruct(out bool succeeded, out TSecondaryValue? value, out string? message)
		{
			succeeded = Succeeded;
			value = Value;
			message = Message;
		}

		//var (succeeded, primaryValue, secondaryValue, message) =
		public void Deconstruct(out bool succeeded, out TPrimaryValue? primaryValue, out TSecondaryValue? secondaryValue, out string? message)
		{
			succeeded = Succeeded;
			primaryValue = _primaryValue;
			secondaryValue = Value;
			message = Message;
		}


		public static bool operator ==(Result<TPrimaryValue, TSecondaryValue> result, bool value) =>
			result is not null && result.Succeeded == value;

		public static bool operator !=(Result<TPrimaryValue, TSecondaryValue> result, bool value) =>
			!(result.Succeeded == value);


		public static Result<TPrimaryValue, TSecondaryValue> Fail(TSecondaryValue secondaryValue = default!, string? message = default) =>
			new(false, secondaryValue, message);

		public override bool TryGetValue<T>(out T? value) where T : default
		{
			if (this is Result<TPrimaryValue, T> result)
			{
				value = result.Value;
				return true;
			}
			value = default;
			return false;
		}

		public override T? As<T>() where T : default => TryGetValue(out T? value) ? value : default;

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (ReferenceEquals(obj, null))
			{
				return false;
			}

			return obj is Result<TPrimaryValue, TSecondaryValue> instance &&
				Succeeded == instance.Succeeded &&
				Message == instance.Message &&
				EqualityComparer<TSecondaryValue>.Default.Equals(this.Value, instance.Value);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Succeeded, Message ?? string.Empty, Value);
		}
	}

}
