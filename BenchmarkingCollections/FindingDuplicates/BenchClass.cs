using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FindingDuplicates;

[SimpleJob(RuntimeMoniker.Net80)]
//[HideColumns("RatioSD", "Error", "Job")]
[MemoryDiagnoser(true)]
public class BenchClass
{

    [Params(10, 100, 1000, 10_000, 100_000)]
    public int Count;
    
    public long[] Array;
    public IReadOnlyList<long> ReadOnlyList;

    [GlobalSetup]
    public void Setup()
    {
        int topValue = 20;

        int seed = 500;
        var rnd = new Random(seed);

        Array = new long[Count];
        for (int i = 0; i < Count; i++)
            Array[i] = rnd.Next(topValue);

        ReadOnlyList = Array;
    }
    
    [Benchmark]
    public bool ArrayHasDuplicates_DistinctLinq()
    {
        return Array.Length != Array.Distinct().Count();
    }
    
    [Benchmark]
    public bool ArrayHasDuplicates_GroupByLinq()
    {
        return Array.GroupBy(x => x).Any(g => g.Count() > 1);
    }
    
    [Benchmark]
    public bool ArrayHasDuplicates_HashSet1()
    {
        return !Array.All(new HashSet<long>().Add);
    }
    
    [Benchmark]
    public bool ArrayHasDuplicates_HashSet2()
    {
        var hashSet = new HashSet<long>();
        foreach(var x in Array) 
        {
            if (!hashSet.Add(x))
            {
                return true;
            }
        }

        return false;
    }
    
    [Benchmark]
    public bool ArrayHasDuplicates_HashSet3()
    {
        return (new HashSet<long>(Array).Count < Array.Length);
    }


    [Benchmark]
    public bool ArrayHasDuplicates_MostaExisting()
    {
        if (Array.Length is 0)
            return false;

        return Array.Any(primary => Array.Count(compared => primary == compared) > 1);
    }
    
    [Benchmark]
    public bool ReadOnlyListHasDuplicates_DistinctLinq()
    {
        return ReadOnlyList.Count != ReadOnlyList.Distinct().Count();
    }
    
    [Benchmark]
    public bool ReadOnlyListHasDuplicates_GroupByLinq()
    {
        return ReadOnlyList.GroupBy(x => x).Any(g => g.Count() > 1);
    }
    
    [Benchmark]
    public bool ReadOnlyListHasDuplicates_HashSet1()
    {
        return !ReadOnlyList.All(new HashSet<long>().Add);
    }
    
    [Benchmark]
    public bool ReadOnlyListHasDuplicates_HashSet2()
    {
        var hashSet = new HashSet<long>();
        foreach(var x in ReadOnlyList) 
        {
            if (!hashSet.Add(x))
            {
                return true;
            }
        }

        return false;
    }
    
    [Benchmark]
    public bool ReadOnlyListHasDuplicates_HashSet3()
    {
        return (new HashSet<long>(ReadOnlyList).Count < ReadOnlyList.Count);
    }


    [Benchmark]
    public bool ReadOnlyListHasDuplicates_MostaExisting()
    {
        if (ReadOnlyList.Count is 0)
            return false;

        return ReadOnlyList.Any(primary => ReadOnlyList.Count(compared => primary == compared) > 1);
    }
}
