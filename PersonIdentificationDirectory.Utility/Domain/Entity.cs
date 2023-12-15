namespace PersonIdentificationDirectory.Utility.Domain
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
        public string SecondaryId { get; protected set; } = Guid.NewGuid().ToString();

        public static void ThrowDomainException(string message)
        {
            throw new Exception(message);
        }
    }
}
