// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Attributes;

Console.WriteLine("Hello, World!");


[MemoryDiagnoser]
public class Mtds
{
    [Params(10, 100, 1000, 10_000, 100_000)]
    public int Count;

    [Params(ElementPosition.AtStart, ElementPosition.AtMiddle, ElementPosition.AtEnd)]
    public ElementPosition ElementPosition;

    private int[] _array;
    private IEnumerable<int> _enumerable;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _array = Enumerable.Repeat(1, Count).ToArray();
        if (ElementPosition is ElementPosition.AtStart)
            _array[2] = 5;
        else if (ElementPosition is ElementPosition.AtMiddle)
        {
            var middle = Count / 2;
            _array[middle] = 5;
        }
        else
        {
            _array[^2] = 5;
        }


        _enumerable = _array.ToArray();
    }

    [Benchmark(Baseline = true)]
    public bool HasDiff_Enumerator_Array()
    {
        var enumerator = _array.GetEnumerator();

        if (!enumerator.MoveNext())
            return false;

        var first = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (first != current)
                return true;
        }

        return false;
    }

    [Benchmark]
    public bool HasDiff_Enumerator_Enum()
    {
        using var enumerator = _enumerable.GetEnumerator();

        if (!enumerator.MoveNext())
            return false;

        var first = enumerator.Current;

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;

            if (first != current)
                return true;
        }

        return false;
    }
    
    [Benchmark]
    public bool HasDiff_ToHashSet_Array()
    {
        var set = _array.ToHashSet();
        return set.Count > 1;
    }
    
    [Benchmark]
    public bool HasDiff_ToHashSet_Enum()
    {
        var set = _enumerable.ToHashSet();
        return set.Count > 1;
    }
    
    [Benchmark]
    public bool HasDiff_NewHashSet_Array()
    {
        HashSet<int> set = [_array[0]];

        foreach (var a in _array)
        {
            if (set.Contains(a))
                return true;
        }

        return false;
    }
}

public enum ElementPosition
{
    AtStart,
    AtMiddle,
    AtEnd
}