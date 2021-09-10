namespace Core.Utilities.Result.Abstract
{
    public interface IResult
    {
        public string Message { get; }
        public bool Success { get; }
    }
}
