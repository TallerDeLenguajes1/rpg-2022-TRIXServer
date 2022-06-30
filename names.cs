using System.Text.Json.Serialization

public class name
{
    [JsonPropertyName["first"]]
    public string First { get; set; }

    [JsonPropertyName["last"]]
    public string Last { get; set; }

}

public class result
{
    [JsonPropertyName("name")]
    public name Name { get; set; }

}

public class rootNames 
{
    [JsonPropertyName("result")]
    public List<result> Results { get; set; }
    
}