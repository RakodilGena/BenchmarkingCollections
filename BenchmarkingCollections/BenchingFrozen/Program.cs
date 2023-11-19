using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkingCollections>();

[MemoryDiagnoser(true)]
public class BenchmarkingCollections
{
    [Params(10000)] public int RepeatCount;
    
    private IEnumerable<int> GetCollection
        => Enumerable.Range(1, 1000);
    
    [Benchmark]
    public void Array()
    {
        int[] collection = GetCollection.ToArray();
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = collection.Contains(searched);
        }
    }
    
    [Benchmark]
    public void HashSet()
    {
        HashSet<int> set = GetCollection.ToHashSet();
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = set.Contains(searched);
        }
    }
    
    [Benchmark]
    public void FrozenSet()
    {
        FrozenSet<int> set = GetCollection.ToFrozenSet();
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = set.Contains(searched);
        }
    }
    
    [Benchmark]
    public void FrozenSetFromHashSet()
    {
        FrozenSet<int> set = GetCollection.ToHashSet().ToFrozenSet();
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = set.Contains(searched);
        }
    }
    
    [Benchmark]
    public void Dictionary()
    {
        var dictionary = GetCollection.ToDictionary(c=> c);
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = dictionary.TryGetValue(searched, out _);
        }
    }
    
    [Benchmark]
    public void FrozenDictionary()
    {
        var dictionary = GetCollection.ToFrozenDictionary(c=> c);
        var random = new Random();
        
        for (int i = 0; i < RepeatCount; i++)
        {
            var searched = random.Next(1, 500);
            _ = dictionary.TryGetValue(searched, out _);
        }
    }
}