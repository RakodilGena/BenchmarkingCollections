using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;

namespace FasterIntersects;

[MemoryDiagnoser()]
public class Methods
{
    [Params(10, 100, 1_000, 10_000, 100_000)]
    public int Count = default;

    private long[] _array1 = default!;
    private long[] _array2 = default!;
    private HashSet<long> _hashSet1 = default!;
    private HashSet<long> _hashSet2 = default!;
    private FrozenSet<long> _frozenSet1 = default!;
    private FrozenSet<long> _frozenSet2 = default!;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var random = new Random(Seed: 1337);

        _array1 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToArray();
        _array2 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToArray();
        
        _hashSet1 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToHashSet();
        _hashSet2 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToHashSet();
        
        _frozenSet1 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToFrozenSet();
        _frozenSet2 = Enumerable.Range(0, Count).Select(_ => random.NextInt64()).ToFrozenSet();
    }

    [Benchmark]
    public long[] IntersectArrays_Stupid()
    {
        return _array1.Where(i1 => _array2.Contains(i1)).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectArrays_Linq()
    {
        return _array1.Intersect(_array2).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectArrays_WithHashSet()
    {
        var hashSet = _array1.ToHashSet();
        hashSet.IntersectWith(_array2);
        return hashSet.ToArray();
    }
    
    [Benchmark]
    public long[] IntersectHashSets_Stupid()
    {
        return _hashSet1.Where(i1 => _hashSet2.Contains(i1)).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectHashSets_Linq()
    {
        return _hashSet1.Intersect(_hashSet2).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectHashSets_IntersectWith()
    {
        _hashSet1.IntersectWith(_hashSet2);
        return _hashSet1.ToArray();
    }
    
    [Benchmark]
    public long[] IntersectFrozenSets_Stupid()
    {
        return _frozenSet1.Where(i1 => _frozenSet2.Contains(i1)).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectFrozenSets_Linq()
    {
        return _frozenSet1.Intersect(_frozenSet2).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_HashSetWithArray_Linq()
    {
        return _hashSet1.Intersect(_array1).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_HashSetWithArray_IntersectWith()
    { 
        _hashSet1.IntersectWith(_array1);
        return _hashSet1.ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_ArrayWithHashSet()
    {
        return _array1.Intersect(_hashSet1).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_FrozenSetWithArray()
    {
        return _frozenSet1.Intersect(_array1).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_ArrayWithFrozenSet()
    {
        return _array1.Intersect(_frozenSet1).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_HashSetWithFrozenSet_Linq()
    {
        return _hashSet1.Intersect(_frozenSet1).ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_HashSetWithFrozenSet_IntersectWith()
    {
        _hashSet1.IntersectWith(_frozenSet1);
        return _hashSet1.ToArray();
    }
    
    [Benchmark]
    public long[] IntersectCombo_FrozenSetWithHashSet()
    {
        return _frozenSet1.Intersect(_hashSet1).ToArray();
    }
}