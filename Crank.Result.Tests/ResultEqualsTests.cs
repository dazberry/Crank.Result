namespace Crank.Result.Tests
{
	public class ResultEqualsTests
	{
		[Test]
		public void ResultEqualsSelf()
		{
			// arrange
			Result result = (true, $"{Guid.NewGuid()}");

			// assert
			Assert.That(result.Equals(result), Is.True);
		}

		[Test]
		public void ResultEqualsSame()
		{
			// arrange
			var message = $"{Guid.NewGuid()}";
			Result result = (true, message);
			Result result1 = (true, message);

			// assert
			Assert.That(result.Equals(result1), Is.True);
		}

		[Test]
		public void ResultEqualsDifferent()
		{
			// arrange
			var message = $"{Guid.NewGuid()}";
			Result result = (true, message);
			Result result1 = (false, message);
			Result result2 = (true, $"{Guid.NewGuid()}");
			Result result3 = (false, $"{Guid.NewGuid()}");

			// assert
			Assert.That(result.Equals(result1), Is.False);
			Assert.That(result.Equals(result2), Is.False);
			Assert.That(result.Equals(result3), Is.False);
		}

		[Test]
		public void PrimaryResultEqualsSuccessSelf()
		{
			// arrange
			Result<Guid> result = Guid.NewGuid();

			// assert
			Assert.That(result.Equals(result), Is.True);
		}

		[Test]
		public void PrimaryResultEqualsSame()
		{
			// arrange
			Guid value = Guid.NewGuid();
			Result<Guid> result = value;
			Result<Guid> result1 = value;

			// assert
			Assert.That(result.Equals(result1), Is.True);
		}

		[Test]
		public void PrimaryResultEqualsDifferent()
		{
			// arrange
			Guid value = Guid.NewGuid();
			Result<Guid> result = value;
			Result<Guid> result1 = Guid.NewGuid();

			// assert
			Assert.That(result.Equals(result1), Is.False);
		}

		[Test]
		public void SecondaryResultEqualsFailureSelf()
		{
			// arrange
			Result<Guid, int> result = 404;

			// assert
			Assert.That(result.Equals(result), Is.True);
		}

		[Test]
		public void SecondaryResultEqualsSame()
		{
			// arrange
			Result<Guid, int> result = 404;
			Result<Guid, int> result1 = 404;

			// assert
			Assert.That(result.Equals(result1), Is.True);
		}

		[Test]
		public void SecondaryResultEqualsDifferent()
		{
			// arrange
			Result<Guid, int> result = 404;
			Result<Guid, int> result1 = 500;

			// assert
			Assert.That(result.Equals(result1), Is.False);
		}
	}
}
