namespace SODP.Domain.ValueObjects
{
    public abstract record ValueObject<T> where T : class
    {
        public T Value { get; }

        public ValueObject(T value)
        {                         
            Value = value;
        }
    }
}
