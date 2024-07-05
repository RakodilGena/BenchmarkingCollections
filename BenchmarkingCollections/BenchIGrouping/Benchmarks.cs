using BenchmarkDotNet.Attributes;

namespace BenchIGrouping;

[MemoryDiagnoser]
public class Benchmarks
{
    private sealed record Record(long ParentId, long SelfId, string Filler, int Addition);



    //[Params(10, 100, 1000, 10000, 100000)] public int CollectionCount = 0;
    // IEnumerable<Record> GetInitialCollection()
    //     => Enumerable.Range(1, CollectionCount)
    //         .Select(id => new Record(
    //             ParentId: GetParentId(id),
    //             SelfId: id,
    //             Filler: $"theres some filler for {id}",
    //             Addition: 1)
    //         );
    
    [Params(0, 1, 2, 3, 4)] public int CollectionIndex = 0;

    public Benchmarks()
    {
        _initialCollections = new[]
        {
            GetInitialCollection(10),
            GetInitialCollection(100),
            GetInitialCollection(1000),
            GetInitialCollection(10000),
            GetInitialCollection(100000),
        };
    }

    private Record[][] _initialCollections = null!;
        
    Record[] GetInitialCollection(int count)
    => Enumerable.Range(1, count)
        .Select(id => new Record(
            ParentId: GetParentId(id),
            SelfId: id,
            Filler: $"theres some filler for {id}",
            Addition: 1)
        ).ToArray();

    long GetParentId(long selfId)
    {
        //every 10 records has new parentId
        var parentId = selfId / 10 + 1;
        return parentId;
    }

    [Benchmark]
    public void ToArrayNotUsed_Tuples()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                (ParentId: gr.Key,
                    Elements: gr));

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayInnerElements_Tuples()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                (ParentId: gr.Key,
                    Elements: gr.ToArray()));

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayGrouping_Tuples()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                (ParentId: gr.Key,
                    Elements: gr)).ToArray();

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayGroupingAndElements_Tuples()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                (ParentId: gr.Key,
                    Elements: gr.ToArray())).ToArray();

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }


    [Benchmark]
    public void ToArrayNotUsed_AnonClasses()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                new
                {
                    ParentId = gr.Key,
                    Elements = gr
                });

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayInnerElements_AnonClasses()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                new
                {
                    ParentId = gr.Key,
                    Elements = gr.ToArray()
                });

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayGrouping_AnonClasses()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                new
                {
                    ParentId = gr.Key,
                    Elements = gr
                }).ToArray();

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }

    [Benchmark]
    public void ToArrayGroupingAndElements_AnonClasses()
    {
        var collection = _initialCollections[CollectionIndex];

        var groups = collection.GroupBy(record => record.ParentId)
            .Select(gr =>
                new
                {
                    ParentId = gr.Key,
                    Elements = gr.ToArray()
                }).ToArray();

        var sum = groups.Sum(gr => gr.Elements.Sum(e => e.Addition));
    }
}