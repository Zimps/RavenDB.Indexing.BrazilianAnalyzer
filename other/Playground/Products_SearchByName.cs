namespace Playground;

public sealed class Products_SearchByName : AbstractIndexCreationTask<Product>
{
    public Products_SearchByName()
    {
        Map = products => products.Select(p => new
        {
            p.Name
        });

        Index(entry => entry.Name, FieldIndexing.Search);
        Analyze(entry => entry.Name, typeof(RavenBrazilianAnalyzer).AssemblyQualifiedName);
    }
}