// See https://aka.ms/new-console-template for more information

using BenchingSomeLinq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<BenchmarkingLinq>();

namespace BenchingSomeLinq
{
    // [SimpleJob(RuntimeMoniker.Net60, baseline: true)]
    // [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    [HideColumns("RatioSD", "Error", "Job")]
    [MemoryDiagnoser(true)]
    public class BenchmarkingLinq
    {

        // [Benchmark]
        // public void BenchmarkLambda()
        // {
        //     var set = Enumerable.Range(1, 10000).ToHashSet();
        //
        //     var array = Enumerable.Range(1, 10000).ToArray();
        //
        //     _ = array.Where(elem => set.Contains(elem)).ToArray();
        // }
        //
        // [Benchmark]
        // public void BenchmarkMethodGroup()
        // {
        //     var set = Enumerable.Range(1, 10000).ToHashSet();
        //
        //     var array = Enumerable.Range(1, 10000).ToArray();
        //
        //     _ = array.Where(set.Contains).ToArray();
        // }

        [Params(2, 10, 100, 1000, 10000)] public int RepeatCount;

        [Benchmark(Baseline = true)]
        public List<int> BenchmarkAddToList()
        {
            var list = Enumerable.Range(1, 50).ToList();

            var elements = Enumerable.Range(1, 50).ToArray();
            for (int i = 0; i < RepeatCount - 1; i++)
            {
                list.AddRange(elements);
            }

            return list;
        }

        [Benchmark]
        public List<int> BenchmarkConcatEnumerableToList1()
        {
            var enumerable = Enumerable.Range(1, 50);

            var elements = Enumerable.Range(1, 50).ToArray();
            for (int i = 0; i < RepeatCount - 1; i++)
            {
                enumerable = enumerable.Concat(elements);
            }

            var list = enumerable.ToList();

            return list;
        }

        [Benchmark]
        public List<int> BenchmarkConcatEnumerableToList2()
        {
            var enumerable = Enumerable.Empty<int>();

            var elements = Enumerable.Range(1, 50).ToArray();
            for (int i = 0; i < RepeatCount; i++)
            {
                enumerable = enumerable.Concat(elements);
            }

            var list = enumerable.ToList();

            return list;
        }
    }
}