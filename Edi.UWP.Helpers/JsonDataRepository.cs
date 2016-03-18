using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace Edi.UWP.Helpers
{
    public class JsonDataRepository<T> where T : class
    {
        /// <summary>
        /// Json file name which will be used to save data
        /// </summary>
        public string JsonFileName { get; set; }

        public JsonDataRepository(string jsonfilename)
        {
            JsonFileName = jsonfilename;
        }

        /// <summary>
        /// Sava data to json file
        /// </summary>
        /// <param name="data">data object</param>
        /// <returns>Task</returns>
        public async Task SaveDataAsync(T data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (Stream stream = await ApplicationData.Current.LocalFolder
                                         .OpenStreamForWriteAsync(JsonFileName, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, data);
            }
        }

        /// <summary>
        /// Get data from json file
        /// </summary>
        /// <returns>data object</returns>
        public async Task<T> LoadDataAsync()
        {
            try
            {
                Stream ms = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JsonFileName);
                var serializer = new DataContractJsonSerializer(typeof(T));
                object obj = serializer.ReadObject(ms);
                var ids = obj as T;
                return ids;
            }
            catch (FileNotFoundException)
            {
                return default(T);
            }
        }
    }
}
