namespace Crank.Result.Tests
{
	public class ResultExtensionTests
	{
		private class ReferenceClass
		{
			public Guid Id { get; set; } = Guid.Empty;
		}

		[TestCase(true)]
		[TestCase(false)]
		public void GivenAPrimaryResult_WithANonNullResult_ShouldReturnSuppliedSuccessValue(bool successful)
		{
			// arrange
			Result<ReferenceClass> result = (successful, new ReferenceClass() { Id = Guid.NewGuid() });

			// act
			var (success, value, _) = result.Deref();


			// assert
			Assert.That(success, Is.EqualTo(successful));
			Assert.That(value, Is.EqualTo(result.Value));
			Assert.That(value.Id, Is.Not.EqualTo(Guid.Empty));
		}

		[TestCase(true)]
		[TestCase(false)]
		public void GivenAPrimaryResult_WithANullReferenceValue_ShouldSetSuccessToFalse(bool successful)
		{
			// arrange
			// arrange
			Result<ReferenceClass> result = (successful, default!);

			// act
			var (success, value, _) = result.Deref();


			// assert
			Assert.That(success, Is.False);
			Assert.That(value, Is.Null);
		}

		[TestCase(true)]
		[TestCase(false)]
		public void GivenASecondaryResult_WithANonNullResult_ShouldReturnSuppliedSuccessValue(bool successful)
		{
			// arrange
			Result<Guid, ReferenceClass> result = (successful, new ReferenceClass() { Id = Guid.NewGuid() }, string.Empty);
			// act
			var (success, value, _) = result.Deref();


			// assert
			Assert.That(success, Is.EqualTo(successful));
			Assert.That(value, Is.EqualTo(result.Value));
			Assert.That(value.Id, Is.Not.EqualTo(Guid.Empty));
		}

		[TestCase(true)]
		[TestCase(false)]
		public void GivenASecondaryResult_WithANullReferenceValue_ShouldSetSuccessToFalse(bool successful)
		{
			// arrange
			// arrange
			Result<Guid, ReferenceClass> result = (successful, default!, string.Empty);

			// act
			var (success, value, _) = result.Deref();


			// assert
			Assert.That(success, Is.False);
			Assert.That(value, Is.Null);
		}


		private static Task<Result<TPrimaryValue>> Invoke<TPrimaryValue>(Result<TPrimaryValue> result) =>
				Task.FromResult(result);

		private static Task<Result<TPrimaryValue, TSecondaryValue>> Invoke<TPrimaryValue, TSecondaryValue>(Result<TPrimaryValue, TSecondaryValue> result) =>
				Task.FromResult(result);


		[TestCase(true)]
		[TestCase(false)]
		public async Task GivenAPrimaryResult_WithANonNullResult_ShouldReturnSuppliedSuccessValue_Async(bool successful)
		{
			// arrange
			Result<ReferenceClass> result = (successful, new ReferenceClass() { Id = Guid.NewGuid() });

			// act
			var (success, value, _) = await Invoke(result).Deref();


			// assert
			Assert.That(success, Is.EqualTo(successful));
			Assert.That(value, Is.EqualTo(result.Value));
			Assert.That(value.Id, Is.Not.EqualTo(Guid.Empty));
		}

		[TestCase(true)]
		[TestCase(false)]
		public async Task GivenAPrimaryResult_WithANullReferenceValue_ShouldSetSuccessToFalse_Async(bool successful)
		{
			// arrange
			// arrange
			Result<ReferenceClass> result = (successful, default!);

			// act
			var (success, value, _) = await Invoke(result).Deref();


			// assert
			Assert.That(success, Is.False);
			Assert.That(value, Is.Null);
		}

		[TestCase(true)]
		[TestCase(false)]
		public async Task GivenASecondaryResult_WithANonNullResult_ShouldReturnSuppliedSuccessValue_Async(bool successful)
		{
			// arrange
			Result<Guid, ReferenceClass> result = (successful, new ReferenceClass() { Id = Guid.NewGuid() }, string.Empty);

			// act
			var (success, value, _) = await Invoke(result).Deref();


			// assert
			Assert.That(success, Is.EqualTo(successful));
			Assert.That(value, Is.EqualTo(result.Value));
			Assert.That(value.Id, Is.Not.EqualTo(Guid.Empty));
		}

		[TestCase(true)]
		[TestCase(false)]
		public async Task GivenASecondaryResult_WithANullReferenceValue_ShouldSetSuccessToFalse_Async(bool successful)
		{
			// arrange
			Result<Guid, ReferenceClass> result = (successful, default!, string.Empty);

			// act
			var (success, value, _) = await Invoke(result).Deref();


			// assert
			Assert.That(success, Is.False);
			Assert.That(value, Is.Null);
		}
	}
}
