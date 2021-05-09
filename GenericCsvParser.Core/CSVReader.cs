namespace GenericCsvParser.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public static class CSVReader
    {
        public async static Task<List<T>> ReadCsv<T>(string path, char delimiter = ';') where T : class, new()
        {
            var entities = Activator.CreateInstance<List<T>>();
            var headersDict = new Dictionary<string, int>();

            using (var streamReader = new StreamReader(path))
            {
                var headerLine = await streamReader.ReadLineAsync();

                var headers = headerLine.Split(delimiter);
                for(var i = 0; i < headers.Length; i++)
                {
                    headersDict.Add(headers[i], i);
                }

                while (!streamReader.EndOfStream)
                {
                    var type = typeof(T);
                    var properties = type.GetProperties();

                    var currentValues = (await streamReader.ReadLineAsync()).Split(delimiter);

                    var obj = new T();
                    foreach (var prop in properties)
                    {
                        var propType = prop.PropertyType;
                        var currentValue = currentValues[headersDict[prop.Name]].ToString();

                        prop.SetValue(obj, ConvertedValue(currentValue, propType));
                    }
                    entities.Add(obj);
                }
            }

            return entities;
        }

        private static object ConvertedValue(string currentValue, Type propType)
        {
            if (propType == typeof(int)) return Convert.ToInt32(currentValue);

            if (propType == typeof(string)) return currentValue;

            if (propType == typeof(Guid)) return Guid.Parse(currentValue);

            if (propType == typeof(double)) return Convert.ToDouble(currentValue);

            if (propType == typeof(float)) return float.Parse(currentValue);

            if (propType == typeof(bool)) return Convert.ToBoolean(currentValue);

            if (propType == typeof(decimal)) return Convert.ToDouble(currentValue);

            throw new NotImplementedException();
        }
    }
}
