namespace Crank.Result.Tests
{
	public class PrimaryResultTests
	{
		[Datapoint]
		public bool[] boolArray = { true, false };


		[Test]
		public void AssigningAPrimaryValueResultFromAValue_ShouldSetTheSucceededValue()
		{
			//arrange
			bool expectedResult = true;
			Guid expectedValue = Guid.NewGuid();

			//act
			Result<Guid> primaryResultAssignedFromValue = expectedValue;
			var (succeeded, resultValue) = primaryResultAssignedFromValue;
			var (_, _, message) = primaryResultAssignedFromValue;

			//assert
			Assert.That(primaryResultAssignedFromValue == expectedResult, Is.EqualTo(true));
			Assert.That(primaryResultAssignedFromValue.Succeeded, Is.EqualTo(expectedResult)) ;
			Assert.That(primaryResultAssignedFromValue.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultAssignedFromValue.Message, Is.Empty);
			Assert.That(primaryResultAssignedFromValue.Value, Is.EqualTo(expectedValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(resultValue, Is.EqualTo(expectedValue));
			Assert.That(message, Is.Empty);
		}

		[Theory]
		public void AssigningAPrimaryValueResultFromATuple_ShouldSetResultAndValue(bool expectedResult)
		{
			//arrange
			Guid expectedValue = Guid.NewGuid();

			//act
			Result<Guid> primaryResultAssignedFromValue = (expectedResult, expectedValue);
			var (succeeded, resultValue) = primaryResultAssignedFromValue;
			var (_, _, message) = primaryResultAssignedFromValue;

			//assert
			Assert.That(primaryResultAssignedFromValue == expectedResult, Is.EqualTo(true));
			Assert.That(primaryResultAssignedFromValue.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(primaryResultAssignedFromValue.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultAssignedFromValue.Message, Is.Empty);
			Assert.That(primaryResultAssignedFromValue.Value, Is.EqualTo(expectedValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(resultValue, Is.EqualTo(expectedValue));
			Assert.That(message, Is.Empty);
		}

		[Theory]
		public void AssigningAPrimaryValueResultFromATuple_ShouldSetResultValueAndMessage(bool expectedResult)
		{
			//arrange
			Guid expectedValue = Guid.NewGuid();
			string expectedMessage = $"{Guid.NewGuid()}";

			//act
			Result<Guid> primaryResultAssignedFromValue = (expectedResult, expectedValue, expectedMessage);
			var (succeeded, resultValue) = primaryResultAssignedFromValue;
			var (_, _, message) = primaryResultAssignedFromValue;

			//assert
			Assert.That(primaryResultAssignedFromValue == expectedResult, Is.EqualTo(true));
			Assert.That(primaryResultAssignedFromValue.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(primaryResultAssignedFromValue.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultAssignedFromValue.Message, Is.EqualTo(expectedMessage));
			Assert.That(primaryResultAssignedFromValue.Value, Is.EqualTo(expectedValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(resultValue, Is.EqualTo(expectedValue));
			Assert.That(message, Is.EqualTo(expectedMessage));
		}



		[Test]
		public void CreatingAPrimaryValueResultFromTheSuccessMethod_ShouldSetTheSucceededValue()
		{
			//arrange
			bool expectedResult = true;
			Guid value = Guid.NewGuid();

			//act
			Result<Guid> primaryResultFromSuccessMethod = Result<Guid>.Success(value);
			var (succeeded, primaryValue) = primaryResultFromSuccessMethod;
			var (_, _, message) = primaryResultFromSuccessMethod;

			//assert
			Assert.That(primaryResultFromSuccessMethod == expectedResult, Is.EqualTo(true));
			Assert.That(primaryResultFromSuccessMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(primaryResultFromSuccessMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultFromSuccessMethod.Message, Is.Empty);

			Assert.That(value, Is.EqualTo(primaryResultFromSuccessMethod.Value));
			Assert.That(value, Is.EqualTo(primaryValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.Empty);
		}

		[Test]
		public void CreatingAPrimaryValueResultFromTheFailMethod_ShouldSetTheFailedValue()
		{
			//arrange
			bool expectedResult = false;
			Guid value = Guid.NewGuid();
			string msg = $"{Guid.NewGuid()}";

			//act
			Result<Guid> primaryResultFromSuccessMethod = Result<Guid>.Fail(value, msg);
			var (succeeded, primaryValue) = primaryResultFromSuccessMethod;
			var (_, _, message) = primaryResultFromSuccessMethod;

			//assert
			Assert.That(primaryResultFromSuccessMethod == expectedResult, Is.EqualTo(true));
			Assert.That(primaryResultFromSuccessMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(primaryResultFromSuccessMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultFromSuccessMethod.Message, Is.EqualTo(msg));

			Assert.That(value, Is.EqualTo(primaryResultFromSuccessMethod.Value));
			Assert.That(value, Is.EqualTo(primaryValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(msg));
		}


		[Test]
		public void CallingFailWithATypeParameter_ShouldCreateASecondaryResult()
		{
			//arrange
			bool expectedResult = false;
			int expectedErrorCode = 404;
			string msg = $"{Guid.NewGuid()}";

			//act
			Result<Guid, int> secondaryResultFromFailMethod = Result<Guid>.Fail<int>(expectedErrorCode, msg);
			var (succeeded, secondaryValue) = secondaryResultFromFailMethod;
			var (_, _, message) = secondaryResultFromFailMethod;

			//assert
			Assert.That(secondaryResultFromFailMethod == expectedResult, Is.EqualTo(true));
			Assert.That(secondaryResultFromFailMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(secondaryResultFromFailMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(secondaryResultFromFailMethod.Message, Is.EqualTo(msg));

			Assert.That(expectedErrorCode, Is.EqualTo(secondaryResultFromFailMethod.Value));
			Assert.That(expectedErrorCode, Is.EqualTo(secondaryValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(msg));
		}

	}
}
