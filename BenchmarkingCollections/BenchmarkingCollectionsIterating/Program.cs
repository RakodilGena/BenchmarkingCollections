// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkingCollectionsIterating.BenchmarkingCollections>();

namespace BenchmarkingCollectionsIterating
{
    [MemoryDiagnoser(true)]
    public class BenchmarkingCollections
    {
        private IEnumerable<int> GetCollection
            => Enumerable.Range(1, 50);

        [Params(1, 2000)] public int RepeatCount;

        [Benchmark]
        public void Array()
        {
            int[] collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ArrayAsIEnumerable()
        {
            IEnumerable<int> collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ArrayAsICollection()
        {
            ICollection<int> collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ArrayAsIList()
        {
            IList<int> collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ArrayAsIReadOnlyCollection()
        {
            IReadOnlyCollection<int> collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ArrayAsIReadOnlyList()
        {
            IReadOnlyList<int> collection = GetCollection.ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void List()
        {
            List<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ListAsIEnumerable()
        {
            IEnumerable<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ListAsICollection()
        {
            ICollection<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ListAsIList()
        {
            IList<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ListAsIReadOnlyCollection()
        {
            IReadOnlyCollection<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
        [Benchmark]
        public void ListAsIReadOnlyList()
        {
            IReadOnlyList<int> collection = GetCollection.ToList();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
    
        [Benchmark]
        public void HashSet()
        {
            HashSet<int> collection = GetCollection.ToHashSet();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
    
        [Benchmark]
        public void HashSetAsISet()
        {
            ISet<int> collection = GetCollection.ToHashSet();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    
    
        [Benchmark]
        public void HashSetAsIReadOnlySet()
        {
            IReadOnlySet<int> collection = GetCollection.ToHashSet();
            for (int i = 0; i < RepeatCount; i++)
            {
                int sum = 0;
                foreach (var num in collection)
                {
                    sum += num;
                }
            }
        }
    }
}

