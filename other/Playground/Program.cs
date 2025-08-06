namespace Playground;

internal static class Program
{
    private static void Main(string[] args)
    {
        using var session = DocumentStoreHolder.Store.OpenSession();

        var results = session
            .Query<Product, Products_SearchByName>()
            .Where(r => r.Name.Contains("guarana"))
            .ToList();

        foreach (var result in results)
        {
            Console.WriteLine(result.Name);
        }

        Console.ReadLine();
    }
}