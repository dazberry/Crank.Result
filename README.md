
# Crank.Result
 Crank result are a set of *slightly opinionated* result classes.
   
## Result 
 - Contains a Succeeded, Failed and an optional Message value. 
 - Failed == !Succeeded
 
### Setting the result:
Assignment:
> Result result = true; 
 
Tuple Assignment:
> Result result = (true, "Success message");
> Result result = (false, "Failure message");
 
Static Methods:
> Result result = Result.Success();
> Result result = Result.Fail();
> Result result = Result.Fail("Failure message");
 
> Result< int> result = Result.Fail< int>(404, "Failure message");
> // *note: fails up to Result< TPrimaryValue> from Result*
 
### Checking success:
> If (result == true) ...
> If (result.Succeeded) ...
> If (!result.Failed) ...
 
### Extracting values:
> var errorMessage = result.Message;
> var (succeeded, message) = result;
>
> // Return false and default as Result has no value
> bool haveValue = result.TryGetValue<int>(out int? value);
> int? value = result.As<int>();
 
 
## Result< TPrimaryValue >
- *inherits from Result*
- TPrimaryValue is accessible from the *Value* member
- **opinionated: when directly assigning a TPrimaryValue value, infers  success == true**
 
### Setting the result:
Assignment:
> Result< Guid> result = Guid.NewGuid();  
> // side effect: set Succeeded to true
 
Tuple Assignment:
> Result< Guid> result = (succeeded, Guid.NewGuid());
> Result< Guid> result = (succeeded, Guid.NewGuid(), "Error Message");
 
Static Methods:
> Result< Guid> result = Result< Guid>.Success(Guid.NewGuid());
> Result< Guid> result = Result< Guid>.Fail(value: Guid.NewGuid(), message: "Error message");
 
> Result< Guid, int> result = Result.Fail< int>(404, "Failure message");
> // *note: fails up to Result<TPrimaryResult, TSecondaryResult> from Result< TPrimaryResult>*
 
### Checking success:
> If (result == true) ...
> If (result.Succeeded) ...
> If (!result.Failed) ...
 
### Extracting values:
> Guid id = result.Value;
> Guid id = result;
> var (succeeded, guidValue) = result;
> var (succeeded, guidValue, message) = result; 
>
> bool haveValue = result.TryGetValue< Guid>(out Guid? value);
> Guid? value = result.As< Guid>();
>

#### [v1.2] Deref
> deconstruct reference type without nullable inference
> var (succeeded, value, message) = result.Deref();
> notes: 
>	if value is null, succeeded will be destructed as false
>	equivant of:
>		succeeded = (succeeded && value != null)
>	can change this behaviour by calling Deref(false)


 
## Result< TPrimaryValue, TSecondaryValue > 
 
- inherits from Result< TPrimaryValue> 
- Assignable to TResult< TPrimaryValue> where both TPrimaryValues are the same type
-- **opinionated: TSecondaryValue is accessible from the Value member (instead of TPrimaryValue)**
-- **opinionated: when directly assigning a TSecondaryValue value, infers success == false**
 
### Setting the result:
Assignment:
> Result< Guid, int> result = 404;
> // side effect: set Succeeded to false
 
Tuple Assignment:
> Result< Guid, int> result = (404, "Failure message");
> Result< Guid, int> result = (succeeded, 404, "Failure message");
 
Static Methods:
> Result< Guid, int>.Fail(value: Guid.NewGuid(), message: "Error message");
 
### Checking success:
> If (result == true) ...
> If (result.Succeeded) ...
> If (!result.Failed) ...
 
### Extracting Values
> int errorCode = result.Value;
> int errorCode = result;
> var (succeeded, intValue) = result;
> var (succeeded, intValue, message) = result;
>
> bool haveValue = result.TryGetValue< int>(out int? value);
> int? value = result.As< int>();
>
#### [v1.2] Deref
> deconstruct reference type without nullable inference
> var (succeeded, value, message) = result.Deref();
 
### Setting and extracting both values 
> Result<Guid, int> result = (true, Guid.NewGuid(), 200, "Success Message");
> (succeeded, primaryValue, secondaryValue) = result;
> (succeeded, primaryValue, secondaryValue, message) = result;
 
# Common usage examples
 
## Returning a result with an error message

	public Result InvokeOperation()
	{
		try
		{
			_repository.Invoke();
			return true;
		}
		catch (Exception ex)
		{
			return (false, ex.Message);
		}
	} 
## Returning success or additional error data
	public Result InvokeOperation
	{
		try
		{
			int resultCode = _repository.Invoke();
			if (resultCode > 0)
				return true;
 
			// fail up to Result<TPrimary>
			return Result.Fail(resultCode);
		}
		catch (Exception ex)
		{
			// fail up to Result<TPrimary>
			return Result.Fail(ex);
		}
	}
	
## Return success data or failure
	public Result<IEnumerable<RowData>> LoadRowData(string filename)
	{
		try
		{
			var data = _repository.LoadData(filename);
			return data;
		}
		catch (Exception ex)
		{
			// fail up to Result<TPrimary, TSecondary>
			return Result<IEnumerable<RowData>>.Fail(ex);
		}
	}
 
## Differentiating between different result types

	var result = LoadRowData();
	if (result.Succeeded) // happy case
		return Ok(result.Value);

	if (result is Result<IEnumerable<RowData>, int> errorResult)
		return ErrorResult(new { errorCode = errorResult.Value });

	if (result is Result<IEnumerable<RowData>, Exception> exceptionResult)
		return InternalServerError();

	...	alterative

	var result = LoadRowData();
	if (result.Succeeded) ...
	
	if (result.TryGetValue<int>(out int errorCode))
		return ErrorResult(new { errorCode });
	
	if (result.TryGetValue<Exception>(out Exception _))
		return InternalServerError();

## Change Logs

**[1.0]** 
Initial release

**[1.1]** 
Added NotNull property and tests

**[1.2]** 
Removed NotNull property and tests
Added Deref Result extensions methods and tests