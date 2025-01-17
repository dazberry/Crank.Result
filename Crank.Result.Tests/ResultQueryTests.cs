namespace Crank.Result.Tests
{
	public class ResultQueryTests
	{

		[Test]
		public void WhenUpcastingFromAPrimaryResult()
		{
			//arrange
			var successValue = Guid.NewGuid();
			Result<Guid> actualResult = successValue;

			//act
			Result result = actualResult;

			//assert
			if (result is Result<Guid> valueResult)
			{
				Assert.Multiple(() =>
				{
					Assert.That(valueResult.Value, Is.EqualTo(successValue));
					Assert.That(valueResult.Succeeded, Is.True);
					Assert.That(valueResult.Failed, Is.False);
					Assert.That(valueResult == true, Is.True);
				});
				return;
			}

			Assert.Fail();
		}

		[Test]
		public void WhenUpcastingFromASecondaryResult()
		{
			//arrange
			var failedValue = 404;
			Result<Guid, int> actualResult = failedValue;

			//act
			Result<Guid> result = actualResult;

			//assert
			if (result is Result<Guid, int> failedResult)
			{
				Assert.Multiple(() =>
				{
					Assert.That(failedResult.Value, Is.EqualTo(failedValue));
					Assert.That(failedResult.Succeeded, Is.False);
					Assert.That(failedResult == true, Is.False);
					Assert.That(failedResult != true, Is.True);
				});
				return;
			}

			if (result is Result<Guid> _)
			{
				Assert.Fail();
				return;
			}

			Assert.Fail();
		}

		[Test]
		public void WhenUpcastingFromASecondaryResultToASuccessResult()
		{
			//arrange
			var failedValue = 404;
			Result<Guid, int> actualResult = failedValue;

			//act
			Result<Guid> result = actualResult;

			//assert
			if (result == true && result is Result<Guid> _)
			{
				Assert.Fail();
				return;
			}

			if (result is Result<Guid, string> _)
			{
				Assert.Fail();
			}

			if (result is Result res)
			{
				Assert.Multiple(() =>
				{
					Assert.That(res.Succeeded, Is.False);
					Assert.That(res.Failed, Is.True);
				});
			}

			if (result is Result<Guid, int> failedResult)
			{
				Assert.Multiple(() =>
				{
					Assert.That(failedResult.Value, Is.EqualTo(failedValue));
					Assert.That(failedResult.Succeeded, Is.False);
				});
				return;
			}

			Assert.Fail();
		}

		[Test]
		public void WhenUpcastingFromASecondaryResultToASuccessResult2()
		{
			//arrange
			var failedValue = 404;
			Result<Guid, int> actualResult = failedValue;

			//act
			Result<Guid> result = actualResult;

			//assert
			if (result.Succeeded && result is Result<Guid> _)
			{
				Assert.Fail();
				return;
			}

			if (result is Result<Guid, string> _)
			{
				Assert.Fail();
			}

			if (result is Result<Guid, int> failedResult)
			{
				Assert.Multiple(() =>
				{
					Assert.That(failedResult.Value, Is.EqualTo(failedValue));
					Assert.That(failedResult.Succeeded, Is.False);
				});
				return;
			}

			Assert.Fail();
		}

		[Test]
		public void WhenUpcastingFromASecondaryResultToASuccessResult3()
		{
			//arrange
			var failedValue = 404;
			Result<Guid, int> actualResult = failedValue;

			//act
			Result<Guid> result = actualResult;


			switch (result)
			{
				case Result<Guid> successResult when successResult.Succeeded:
					Assert.Fail();
					break;

				case Result<Guid, string> _:
					Assert.Fail();
					break;

				case Result<Guid, int> failedResultInt:
					Assert.That(failedResultInt.Value, Is.EqualTo(failedValue));
					Assert.That(failedResultInt.Succeeded, Is.False);
					break;

				case Result<Guid> successResult:
					Assert.Fail();
					break;

				default:
					Assert.Fail();
					break;
			}
		}




	}
}
