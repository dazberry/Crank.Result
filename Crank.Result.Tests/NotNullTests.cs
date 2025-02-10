
namespace Crank.Result.Tests
{
	public class NotNullTests
	{

		private class ResultContent
		{
			public Guid Value { get; set; }
		}

		[Test]
		public void GivenAPrimaryResultWithSuccessAndAValue_ShouldReturnTrueAndValue()
		{
			// arrange
			var result = Result<ResultContent>.Success(new ResultContent());

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(true));
			Assert.NotNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(true));
			Assert.NotNull(result.Value);
		}

		[Test]
		public void GivenAPrimaryResultWithSuccessAndNoValue_ShouldReturnFalseAndDefault()
		{
			// arrange
			var result = Result<ResultContent>.Success(default!);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(true));
			Assert.IsNull(result.Value);
		}

		[Test]
		public void GivenAPrimaryResultWithFailureAndValue_ShouldReturnFalseAndValue()
		{
			// arrange
			var result = Result<ResultContent>.Fail(new ResultContent());

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNotNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(false));
			Assert.IsNotNull(result.Value);
		}

		[Test]
		public void GivenAPrimaryResultWithFailureAndNoValue_ShouldReturnFalseAndDefault()
		{
			// arrange
			var result = Result<ResultContent>.Fail(default!);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(false));
			Assert.IsNull(result.Value);
		}


		[Test]
		public void GivenASecondaryResultWithSuccessAndAValue_ShouldReturnTrueAndValue()
		{
			// arrange
			Result<int, ResultContent> result = (true, new ResultContent(), string.Empty);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(true));
			Assert.NotNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(true));
			Assert.NotNull(result.Value);
		}

		[Test]
		public void GivenASecondaryResultWithSuccessAndNoValue_ShouldReturnFalseAndDefault()
		{
			// arrange
			Result<int, ResultContent> result = (true, default!, string.Empty);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(true));
			Assert.IsNull(result.Value);
		}

		[Test]
		public void GivenASecondaryResultWithFailureAndValue_ShouldReturnFalseAndValue()
		{
			// arrange
			Result<int, ResultContent> result = (false, new ResultContent(), string.Empty);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNotNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(false));
			Assert.IsNotNull(result.Value);
		}

		[Test]
		public void GivenASecondaryResultWithFailureAndNoValue_ShouldReturnFalseAndDefault()
		{
			// arrange
			Result<int, ResultContent> result = (false, default!, string.Empty);

			// act
			var (success, content, _) = result.NotNull;

			// assert
			Assert.That(success, Is.EqualTo(false));
			Assert.IsNull(content);

			Assert.That(result.Succeeded, Is.EqualTo(false));
			Assert.IsNull(result.Value);
		}

	}
}
