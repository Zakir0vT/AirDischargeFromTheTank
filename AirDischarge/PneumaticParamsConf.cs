using Newtonsoft.Json;
using System.IO;

namespace AirDischarge
{
    public abstract class PneumaticParamsConf
    {
        public static PneumaticParams GetFromConfig
        {
            get
            {
                var path = string.Concat(nameof(PneumaticParams), ".json");
                var serializer = new JsonSerializer();

                if (File.Exists(path))
                {
                    using (var file = File.OpenText(path))
                    {
                        return (PneumaticParams)serializer.Deserialize(file, typeof(PneumaticParams));
                    }
                }

                using (var sw = File.CreateText(path))
                {
                    var pneumaticParams = new PneumaticParams();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(sw,  pneumaticParams);
                    return pneumaticParams;
                }
            }
        }
    }
}
