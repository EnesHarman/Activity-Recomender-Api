namespace Core.Utilities.Result.Abstract
{
    public interface IDataResult<T>:IResult
    {
        public T Data { get; }
    }
}
