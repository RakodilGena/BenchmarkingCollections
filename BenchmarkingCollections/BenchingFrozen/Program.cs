using System.Collections.Frozen;
using BenchingFrozen;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkingCollections>();

namespace BenchingFrozen
{
    [MemoryDiagnoser(true)]
    public class BenchmarkingCollections
    {
        [Params(10000)] 
        public int RepeatCount;
    
        private const int SEED = 55;
        private const int MIN_VALUE =1, MAX_VALUE = 1000;
    
        private IEnumerable<int> GetCollection
            => Enumerable.Range(MIN_VALUE, MAX_VALUE);
    
        [Benchmark]
        public void Array()
        {
            int[] collection = GetCollection.ToArray();
            var random = new Random(SEED);
        
            for (int i = 0; i < RepeatCount; i++)
            {
                var searched = random.Next(MIN_VALUE, MAX_VALUE);
                _ = collection.Contains(searched);
            }
        }
    
        [Benchmark]
        public void HashSet()
        {
            HashSet<int> set = GetCollection.ToHashSet();
            var random = new Random(SEED);
        
            for (int i = 0; i < RepeatCount; i++)
            {
                var searched = random.Next(MIN_VALUE, MAX_VALUE);
                _ = set.Contains(searched);
            }
        }
    
        [Benchmark]
        public void FrozenSet()
        {
            FrozenSet<int> set = GetCollection.ToFrozenSet();
            var random = new Random(SEED);
        
            for (int i = 0; i < RepeatCount; i++)
            {
                var searched = random.Next(MIN_VALUE, MAX_VALUE);
                _ = set.Contains(searched);
            }
        }
    
        [Benchmark]
        public void Dictionary()
        {
            var dictionary = GetCollection.ToDictionary(c=> c);
            var random = new Random(SEED);
        
            for (int i = 0; i < RepeatCount; i++)
            {
                var searched = random.Next(MIN_VALUE, MAX_VALUE);
                _ = dictionary.TryGetValue(searched, out _);
            }
        }
    
        [Benchmark]
        public void FrozenDictionary()
        {
            var dictionary = GetCollection.ToFrozenDictionary(c=> c);
            var random = new Random(SEED);
        
            for (int i = 0; i < RepeatCount; i++)
            {
                var searched = random.Next(MIN_VALUE, MAX_VALUE);
                _ = dictionary.TryGetValue(searched, out _);
            }
        }
    }
}