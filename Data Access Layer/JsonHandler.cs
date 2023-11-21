namespace Data_Access_Layer;
using Newtonsoft.Json;

public class JsonHandler
{
    private string _filePath;
    public JsonHandler(string filePath)
    {
        _filePath = filePath;
    }
    public List<T> Read<T>()
    {
        string fileContent = File.ReadAllText(_filePath);

        List<T>? entities = JsonConvert.DeserializeObject<List<T>>(fileContent);

        return entities;
    }
    public void Write<T>(List<T> entities)
    {
        string updatedFileContent = JsonConvert.SerializeObject(entities, Formatting.Indented);

        File.WriteAllText(_filePath, updatedFileContent);
    }
}

