namespace Crank.Result.Tests
{
	public class ResultTryGetValue
	{
		[Test]
		public void TryGetSuccessValueFromAResult()
		{
			// arrange
			Guid value = Guid.NewGuid();
			Result result = Result<Guid>.Fail(value);

			// act
			var tryGetResult = result.TryGetValue<Guid>(out var resultValue);


			//assert
			Assert.That(tryGetResult, Is.True);
			Assert.That(resultValue, Is.EqualTo(value));
		}

		[Test]
		public void TryGetAFailureValueFromAResult()
		{
			// arrange
			int value = 404;
			Result result = Result<Guid, int>.Fail(value);

			// act
			var tryGetGuid = result.TryGetValue<Guid>(out var resultGuidValue);
			var tryGetInt = result.TryGetValue<int>(out var resultIntValue);


			// assert
			Assert.That(tryGetInt, Is.True);
			Assert.That(tryGetGuid, Is.False);
			Assert.That(resultIntValue, Is.EqualTo(value));
		}

		[Test]
		public void TryGetAResultValueFromAResultValue()
		{
			// arrange
			Result result = (true, "Hello World!");

			// act
			var tryGetGuid = result.TryGetValue<Guid>(out var _);
			var tryGetInt = result.TryGetValue<int>(out var _);

			// assert
			Assert.That(tryGetInt, Is.False);
			Assert.That(tryGetGuid, Is.False);
		}


		[Test]
		public void TryGetAPrimaryResultFromAPrimaryResult()
		{
			// arrange
			Guid value = Guid.NewGuid();
			Result<Guid> result = value;

			// act
			var tryGetResult = result.TryGetValue<Guid>(out var resultValue);


			//assert
			Assert.That(tryGetResult, Is.True);
			Assert.That(resultValue, Is.EqualTo(value));
		}

		[Test]
		public void TryGetAFailureResultFromAFailureResult()
		{
			// arrange
			int value = 404;
			Result<Guid, int> result = 404;

			// act
			var tryGetResult = result.TryGetValue<int>(out var resultValue);

			//assert
			Assert.That(tryGetResult, Is.True);
			Assert.That(resultValue, Is.EqualTo(value));
		}


		[Test]
		public void TryGetFailureValueFromSuccessValue()
		{
			// arrange
			int value = 404;
			Result<Guid> result = Result<Guid, int>.Fail(404);

			// act
			var tryGetGuid = result.TryGetValue<Guid>(out var _);
			var tryGetInt = result.TryGetValue<int>(out var resultIntValue);


			//assert
			Assert.That(tryGetInt, Is.True);
			Assert.That(tryGetGuid, Is.False);
			Assert.That(resultIntValue, Is.EqualTo(value));
		}

		[Test]
		public void PrimaryValueGetValueAs()
		{
			// arrange
			Guid value = Guid.NewGuid();

			// act
			Result<Guid> result = value;
			var asGuid = result.As<Guid>();
			var asInt = result.As<int>();
			var asString = result.As<string>();

			// assert

			Assert.IsTrue(value == asGuid);
			Assert.IsTrue(asInt == default);
			Assert.IsTrue(asString == default);
		}

		[Test]
		public void FailureValueGetValueAs()
		{
			// arrange
			Guid value = Guid.NewGuid();

			// act
			Result<int, Guid> result = value;
			var asGuid = result.As<Guid>();
			var asInt = result.As<int>();
			var asString = result.As<string>();

			// assert

			Assert.IsTrue(value == asGuid);
			Assert.IsTrue(asInt == default);
			Assert.IsTrue(asString == default);
		}


	}
}
