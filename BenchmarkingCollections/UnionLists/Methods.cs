using BenchmarkDotNet.Attributes;

namespace UnionLists;

[MemoryDiagnoser()]
public class Methods
{
    [Params(10, 100, 1_000, 10_000, 100_000)]
    public int Count = default;

    private long[] _collection1 = default!, _collection2= default!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(Seed: 34567);

        _collection1 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToArray();
        _collection2 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToArray();
    }

    [Benchmark]
    public long[] ConcatDistinct()
    {
        return _collection1.Concat(_collection2).Distinct().ToArray();
    }

    [Benchmark]
    public long[] ConcatUnion()
    {
        return _collection1.Union(_collection2).ToArray();
    }

    [Benchmark]
    public long[] HashSetToArray()
    {
        var set1 = _collection1.ToHashSet();
        set1.UnionWith(_collection2);
        return set1.ToArray();
    }

    [Benchmark]
    public HashSet<long> HashSet()
    {
        var set1 = _collection1.ToHashSet();
        set1.UnionWith(_collection2);
        return set1;
    }
    
    
}