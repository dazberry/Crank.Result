namespace Crank.Result.Tests
{
	public class SecondaryResultTests
	{

		[Test]
		public void AssigningASecondaryValueResultFromAValue_ImpliesAFailAndShouldSetTheValue()
		{
			//arrange
			bool expectedResult = false;
			int expectedValue = 404;

			//act
			Result<Guid, int> secondaryResultAssignedFromValue = expectedValue;
			var (succeeded, secondaryValueResult) = secondaryResultAssignedFromValue;
			var (_, _, message) = secondaryResultAssignedFromValue;

			//assert
			Assert.That(secondaryResultAssignedFromValue == expectedResult, Is.EqualTo(true));
			Assert.That(secondaryResultAssignedFromValue.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(secondaryResultAssignedFromValue.Failed, Is.EqualTo(!expectedResult));
			Assert.That(secondaryResultAssignedFromValue.Message, Is.Empty);
			Assert.That(secondaryResultAssignedFromValue.Value, Is.EqualTo(expectedValue));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(secondaryValueResult, Is.EqualTo(expectedValue));
			Assert.That(message, Is.Empty);
		}

		[Test]
		public void CreatingASecondaryValueResultFromTheFailMethod_ShouldSetTheValue()
		{
			//arrange
			bool expectedResult = false;
			int expectedValue = 404;
			string msg = $"{Guid.NewGuid()}";

			//act
			Result<Guid, int> secondaryResultFromMethod = Result<Guid, int>.Fail(expectedValue, msg);
			var (succeeded, secondaryValueResult) = secondaryResultFromMethod;
			var (_, _, message) = secondaryResultFromMethod;

			//assert
			Assert.That(secondaryResultFromMethod == expectedResult, Is.EqualTo(true));
			Assert.That(secondaryResultFromMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(secondaryResultFromMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(secondaryResultFromMethod.Message, Is.EqualTo(msg));

			Assert.That(expectedValue, Is.EqualTo(secondaryResultFromMethod.Value));
			Assert.That(expectedValue, Is.EqualTo(secondaryValueResult));

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(msg));
		}

		[Test]
		public void AssigningAllValuesFromATuple_ShouldSetAllValues()
		{
			//arrange
			bool expectedResult = false;
			Guid primaryValue = Guid.NewGuid();
			int secondaryValue = 404;
			string msg = $"{Guid.NewGuid()}";

			//act
			Result<Guid, int> secondaryResultFromMethod = (expectedResult, primaryValue, secondaryValue, msg);
			var (succeeded, primaryResultValue, secondaryResultValue, message) = secondaryResultFromMethod;

			//assert
			Assert.That(secondaryResultFromMethod == expectedResult, Is.EqualTo(true));
			Assert.That(secondaryResultFromMethod.Succeeded, Is.EqualTo(expectedResult));
			Assert.That(secondaryResultFromMethod.Failed, Is.EqualTo(!expectedResult));
			Assert.That(secondaryResultFromMethod.Message, Is.EqualTo(msg));

			Assert.That(primaryResultValue, Is.EqualTo(primaryValue));

			Assert.That(secondaryResultValue, Is.EqualTo(secondaryResultFromMethod.Value));
			Assert.That(secondaryResultValue, Is.EqualTo(secondaryValue));			

			Assert.That(succeeded, Is.EqualTo(expectedResult));
			Assert.That(message, Is.EqualTo(msg));
		}
	}
}
