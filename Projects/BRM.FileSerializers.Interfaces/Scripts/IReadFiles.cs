namespace BRM.FileSerializers.Interfaces
{
    public interface IReadFiles
    {
        /// <summary>
        /// Deserializes data located at <paramref name="filePath"/> to object of type <typeparamref name="TModel"/>
        /// </summary>
        TModel Read<TModel>(string filePath);
    }
}
