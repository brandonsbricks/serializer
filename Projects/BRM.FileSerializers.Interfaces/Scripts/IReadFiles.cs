namespace BRM.FileSerializers.Interfaces
{
    public interface IReadFiles
    {
        /// <summary>
        /// Deserializes text to object of type <typeparamref name="TModel"/> located at <paramref name="filePath"/>
        /// </summary>
        TModel Read<TModel>(string filePath) where TModel : class;
    }
}
