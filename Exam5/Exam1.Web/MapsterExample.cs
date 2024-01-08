using Mapster;

namespace Exam1.Web;

public class MapsterExample
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Adderess { get; set; }
}

public class Demo
{
    public MapsterExample map = new MapsterExample() { Adderess = "Nichu", Id = 1, Name = "Fahim" };

    public void Copy()
    {
        var dto = map.Adapt(map);
        Console.WriteLine(dto);
    }
    
}