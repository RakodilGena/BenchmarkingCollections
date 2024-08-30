// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<Benchmarks>();


[MemoryDiagnoser]
public class Benchmarks
{
    private List<int> _list;
    private int[] _array;

    [Params(2, 5, 10, 100, 1000, 10_000, 100_000)]
    public int Count;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _array = Enumerable.Range(1, Count).ToArray();
        _list = _array.ToList();
    }

    [Benchmark(Baseline = true)]
    public int IteratingOverArray()
    {
        int sum = 0;
        foreach (var val in _array)
        {
            sum += val;
        }

        return sum;
    }

    [Benchmark]
    public int IteratingOverList()
    {
        int sum = 0;
        foreach (var val in _list)
        {
            sum += val;
        }

        return sum;
    }

    [Benchmark]
    public int SumArray()
    {
        return _array.Sum();
    }

    [Benchmark]
    public int SumList()
    {
        return _list.Sum();
    }

    [Benchmark]
    public int SumAfterLinqArray()
    {
        return _array.Select(c => c).Sum();
    }

    [Benchmark]
    public int SumAfterLinqList()
    {
        return _list.Select(c => c).Sum();
    }

    [Benchmark]
    public int IteratingOverArraySpan()
    {
        int sum = 0;
        foreach (var val in _array.AsSpan())
        {
            sum += val;
        }

        return sum;
    }

    [Benchmark]
    public int IteratingOverListSpan()
    {
        int sum = 0;
        foreach (var val in CollectionsMarshal.AsSpan(_list))
        {
            sum += val;
        }

        return sum;
    }
}