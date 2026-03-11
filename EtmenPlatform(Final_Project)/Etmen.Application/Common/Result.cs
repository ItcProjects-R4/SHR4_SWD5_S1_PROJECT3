namespace Etmen.Application.Common;

/// <summary>
/// Discriminated union: either a success value T or a failure Error.
/// Use instead of throwing exceptions for expected business failures.
/// </summary>
/// <typeparam name="T">The success value type.</typeparam>
public sealed class Result<T>
{
    private readonly T?     _value;
    private readonly Error  _error;

    public bool    IsSuccess => _error == Error.None;
    public bool    IsFailure => !IsSuccess;
    public T       Value     => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access Value on a failed Result.");
    public Error   Error     => _error;

    private Result(T value)          { _value = value; _error = Error.None; }
    private Result(Error error)      { _value = default; _error = error; }

    /// <summary>Creates a successful result.</summary>
    public static Result<T> Success(T value)    => new(value);

    /// <summary>Creates a failed result.</summary>
    public static Result<T> Failure(Error error) => new(error);

    /// <summary>Implicit conversion — allows returning T directly from a method.</summary>
    public static implicit operator Result<T>(T value)    => Success(value);

    /// <summary>Implicit conversion — allows returning Error directly from a method.</summary>
    public static implicit operator Result<T>(Error error) => Failure(error);

    /// <summary>Match — functional-style handling of both cases.</summary>
    public TOut Match<TOut>(Func<T, TOut> onSuccess, Func<Error, TOut> onFailure)
        => IsSuccess ? onSuccess(_value!) : onFailure(_error);
}

/// <summary>Non-generic Result for operations that return no value.</summary>
public sealed class Result
{
    private readonly Error _error;

    public bool  IsSuccess => _error == Error.None;
    public bool  IsFailure => !IsSuccess;
    public Error Error     => _error;

    private Result(Error error) { _error = error; }

    public static readonly Result Ok = new(Error.None);

    public static Result      Success()            => Ok;
    public static Result      Failure(Error error) => new(error);
    public static Result<T>   Success<T>(T value)  => Result<T>.Success(value);

    public static implicit operator Result(Error error) => Failure(error);
}
