namespace Playground;

public static class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> LazyStore =
        new(() =>
        {
            var store = new DocumentStore
            {
                Urls = ["http://localhost:8080"],
                Database = "Northwind"
            };

            store.Initialize();

            var asm = Assembly.GetExecutingAssembly();
            IndexCreation.CreateIndexes(asm, store);

            return store;
        });

    public static IDocumentStore Store => LazyStore.Value;
}