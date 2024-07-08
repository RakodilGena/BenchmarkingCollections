using BenchmarkDotNet.Attributes;

namespace FastestEnumCollectionContains;

[MemoryDiagnoser]
public class Methods
{
    
    [Benchmark(Baseline = true)]
    public bool Contains_Array()
    {
        MyEnum[] arr = [
            MyEnum.Zero,
            MyEnum.One,
            MyEnum.Two,
            MyEnum.Three,
            MyEnum.Four,
            MyEnum.Five,
            MyEnum.Six,
            MyEnum.Seven,
            MyEnum.Eight, 
            MyEnum.Nine, 
            MyEnum.Ten
        ];

        var options = arr.AsSpan();

        bool contains = true;
        foreach (var opt in options)
        {
            if (arr.Contains(opt))
                continue;

            contains = false;
        }

        return contains;
    }
    [Benchmark]
    public bool Contains_Span()
    {
        ReadOnlySpan<MyEnum> span = [
            MyEnum.Zero,
            MyEnum.One,
            MyEnum.Two,
            MyEnum.Three,
            MyEnum.Four,
            MyEnum.Five,
            MyEnum.Six,
            MyEnum.Seven,
            MyEnum.Eight, 
            MyEnum.Nine, 
            MyEnum.Ten
        ];

        bool contains = true;
        foreach (var opt in span)
        {
            if (span.ContainsEnum(opt))
                continue;

            contains = false;
        }

        return contains;
    }
    
    [Benchmark]
    public bool Contains_SpanUnderlying()
    {
        ReadOnlySpan<int> span = [
            (int)MyEnum.Zero,
            (int)MyEnum.One,
            (int)MyEnum.Two,
            (int)MyEnum.Three,
            (int)MyEnum.Four,
            (int)MyEnum.Five,
            (int)MyEnum.Six,
            (int)MyEnum.Seven,
            (int)MyEnum.Eight, 
            (int)MyEnum.Nine, 
            (int)MyEnum.Ten
        ];

        bool contains = true;
        foreach (var opt in span)
        {
            if (span.Contains(opt))
                continue;

            contains = false;
        }

        return contains;
    }
    
    private enum MyEnum
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight, 
        Nine, 
        Ten
    }
}