using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FasterGetDuplicates;

[SimpleJob(RuntimeMoniker.Net80)]
//[HideColumns("RatioSD", "Error", "Job")]
[MemoryDiagnoser(true)]
public class Bench
{
    private long[] _data = null!;

    [Params(10, 100, 1_000, 10_000, 100_000)] public int DataCount;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _data = new long[DataCount];

        for (int i = 0; i < DataCount; i++)
        {
            //пусть по 4 дубликата будет у каждого элемента.
            var divider = DataCount / 4;

            long element = i % divider;
            _data[i] = element;
        }
    }

    [Benchmark]
    public IReadOnlyList<long> FindDuplicates_GroupBy()
    {
        var dubs = _data.GroupBy(d => d)
            .Where(d => d.Count() > 1)
            .Select(d => d.Key).ToArray();

        return dubs;
    }

    [Benchmark]
    public IReadOnlyList<long> FindDuplicates_HashSet_SetToArray()
    {
        var set = new HashSet<long>();

        var result = new HashSet<long>();

        foreach (var element in _data.AsSpan())
        {
            var isDuplicated = set.Add(element) is false;

            if (isDuplicated)
                result.Add(element);
        }

        return result.ToArray();
    }
    
    [Benchmark]
    public IReadOnlyList<long> FindDuplicates_HashSet_ListToDistinctArray()
    {
        var set = new HashSet<long>();

        var result = new List<long>();

        foreach (var element in _data.AsSpan())
        {
            var isDuplicated = set.Add(element) is false;

            if (isDuplicated)
                result.Add(element);
        }

        return result.Distinct().ToArray();
    }

    [Benchmark]
    public IReadOnlyList<long> FindDuplicates_HashSet_Yield()
    {
        var set = new HashSet<long>();

        return DubsInner().Distinct().ToArray();

        IEnumerable<long> DubsInner()
        {
            foreach (var element in _data)
            {
                var isDuplicated = set.Add(element) is false;

                if (isDuplicated)
                    yield return element;
            }
        }
    }
}