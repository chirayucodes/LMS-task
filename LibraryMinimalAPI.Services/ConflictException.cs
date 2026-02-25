namespace LibraryMinimalAPI.Services
{
    public sealed class ConflictException(string message) : Exception(message);
}
