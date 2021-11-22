using System;

public sealed class NullMassException : Exception
{
    public int Value { get; }
    public NullMassException(string message, int val) : base(message)
    {
        Value = val;
    }
}
