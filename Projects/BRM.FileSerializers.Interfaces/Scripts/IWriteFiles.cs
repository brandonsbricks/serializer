namespace BRM.FileSerializers.Interfaces
{
    public interface IWriteFiles
    {
        /// <summary>
        /// Serializes and writes the contents of <typeparamref name="TModel"/> to <paramref name="filePath"/>
        /// </summary>
        void Write<TModel>(string filePath, TModel model) where TModel : class;
    }
}
