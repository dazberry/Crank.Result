namespace Crank.Result.Tests
{

	/// <summary>
	///
	/// </summary>
	public class SimpleResultTests
	{
		[Datapoint]
		public bool[] boolArray = { true, false };


		[Theory]
		public void AssigningASimpleResultFromABoolean_ShouldSetTheSucceededValue(bool expectedResult)
		{
			//arrange
			//act
			Result simpleResultAssignedFromABoolean = expectedResult;
			var (succeeded, message) = simpleResultAssignedFromABoolean;

			//assert
			Assert.That(simpleResultAssignedFromABoolean == expectedResult, Is.EqualTo(true));
			Assert.That(simpleResultAssignedFromABoolean.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Message, Is.Empty);

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultAssignedFromABoolean.Message));
		}

		[Theory]
		public void AssigningASimpleResultFromATuple_ShouldSetTheSucceededValue(bool expectedResult)
		{
			//arrange
			//act
			Result simpleResultAssignedFromABoolean = (expectedResult);
			var (succeeded, message) = simpleResultAssignedFromABoolean;

			//assert
			Assert.That(simpleResultAssignedFromABoolean == expectedResult, Is.EqualTo(true));
			Assert.That(simpleResultAssignedFromABoolean.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Message, Is.Empty);

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultAssignedFromABoolean.Message));
		}

		[Theory]
		public void AssigningASimpleResultFromATuple_ShouldSetTheSucceededValueAndMessage(bool expectedResult)
		{
			//arrange
			string msg = $"{Guid.NewGuid}";
			//act
			Result simpleResultAssignedFromABoolean = (expectedResult, msg);
			var (succeeded, message) = simpleResultAssignedFromABoolean;

			//assert
			Assert.That(simpleResultAssignedFromABoolean == expectedResult, Is.EqualTo(true));
			Assert.That(simpleResultAssignedFromABoolean.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultAssignedFromABoolean.Message, Is.EqualTo(msg));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultAssignedFromABoolean.Message));
		}

		[Test]
		public void CreatingASimpleValueFromTheSuccessMethod_ShouldSetTheSucceededValue()
		{
			//arrange
			bool expectedResult = true;
			//act
			Result simpleResultCreatedFromSuccessMethod = Result.Success();
			var (succeeded, message) = simpleResultCreatedFromSuccessMethod;

			//assert
			Assert.That(simpleResultCreatedFromSuccessMethod == true, Is.EqualTo(true));
			Assert.That(simpleResultCreatedFromSuccessMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Message, Is.Empty);

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultCreatedFromSuccessMethod.Message));
		}

		[Test]
		public void CreatingASimpleValueFromTheFailMethod_ShouldSetTheFailedValue()
		{
			//arrange
			bool expectedResult = false;
			//act
			Result simpleResultCreatedFromSuccessMethod = Result.Fail();
			var (succeeded, message) = simpleResultCreatedFromSuccessMethod;

			//assert
			Assert.That(simpleResultCreatedFromSuccessMethod == false, Is.EqualTo(true));
			Assert.That(simpleResultCreatedFromSuccessMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Message, Is.Empty);

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultCreatedFromSuccessMethod.Message));
		}

		[Test]
		public void CreatingASimpleValueFromTheFailMethodWithMessage_ShouldSetTheFailedValue()
		{
			//arrange
			bool expectedResult = false;
			string msg = $"{Guid.NewGuid}";
			//act
			Result simpleResultCreatedFromSuccessMethod = Result.Fail(msg);
			var (succeeded, message) = simpleResultCreatedFromSuccessMethod;

			//assert
			Assert.That(simpleResultCreatedFromSuccessMethod == true, Is.EqualTo(expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(simpleResultCreatedFromSuccessMethod.Message, Is.EqualTo(msg));


			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(simpleResultCreatedFromSuccessMethod.Message));
		}

		[Test]
		public void FailingUpFromASimpleResult_ShouldCreateAPrimaryResult()
		{
			//arrange
			bool expectedResult = false;
			int expectedErrorCode = 404;
			string expectedMessage = $"{Guid.NewGuid}";

			//act
			Result<int> primaryResultCreatedFromFailMethod = Result.Fail(expectedErrorCode, expectedMessage);
			var (succeeded, errorCode, message) = primaryResultCreatedFromFailMethod;

			//assert
			Assert.That(primaryResultCreatedFromFailMethod == true, Is.EqualTo(expectedResult));
			Assert.That(primaryResultCreatedFromFailMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(primaryResultCreatedFromFailMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(primaryResultCreatedFromFailMethod.Message, Is.EqualTo(expectedMessage));

			Assert.That(primaryResultCreatedFromFailMethod.Value, Is.EqualTo(expectedErrorCode));


			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(primaryResultCreatedFromFailMethod.Message));
		}

	}
}
