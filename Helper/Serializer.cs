using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Helper
{
    public static class Serializer
    {
        /// <summary>
        /// Enhances interoperability of different data models, but with similar properties. 
        /// It makes objects become compatible with one another through serialization and deserialization
        /// </summary>
        /// <typeparam name="T">Destination data model type for which <paramref name="model"/> is to be deserialized into</typeparam>
        /// <param name="model">Source data model</param>
        /// <returns>A data model of type <typeparamref name="T"/></returns>
        public static async Task<T> GetDeserializedModelAsync<T>(object model) where T : class
        {
            var serializedModel = await Task.Run(() => JsonConvert.SerializeObject(model));
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(serializedModel));
        }

        /// <summary>
        /// Enhances interoperability of different data models, but with similar properties. 
        /// It makes objects become compatible with one another through serialization and deserialization
        /// This one takes inn a string of data
        /// </summary>
        /// <typeparam name="T">Destination data model type for which <paramref name="model"/> is to be deserialized into</typeparam>
        /// <param name="serializedModel">Source data as serialized string model</param>
        /// <returns>A data model of type <typeparamref name="T"/></returns>
        public static async Task<T> GetDeserializedModelAsync<T>(string serializedModel) where T : class
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(serializedModel));
        }

        /// <summary>
        /// Enhances interoperability of different data models, but with similar properties. 
        /// It makes objects become compatible with one another through serialization and deserialization
        /// This one is specifically designed for object data models received from client side.
        /// </summary>
        /// <typeparam name="T">Destination data model type for which <paramref name="model"/> is to be deserialized into</typeparam>
        /// <param name="model">Source data model</param>
        /// <returns>A data model of type <typeparamref name="T"/></returns>
        public static async Task<T> GetDeserializedClientModelAsync<T>(object model) where T : class
        {
            var serializedModel = model.ToString();
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(serializedModel));
        }

        /// <summary>
        /// Serializes a data object into a string
        /// </summary>
        /// <param name="model">Data model to serialize</param>
        /// <returns>Data as string</returns>
        public static async Task<string> GetSerializedModelAsync(object model)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(model));
        }
    }
}
