using Newtonsoft.Json;

namespace SimpleSampleLibrary
{
    public static class SimpleJsonConverter
    {
        public static string Convert(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
