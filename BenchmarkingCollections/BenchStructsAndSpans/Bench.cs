using BenchmarkDotNet.Attributes;

namespace BenchStructsAndSpans;

[MemoryDiagnoser]
public class Bench
{
    public record struct MyLilStruct(bool Bool, MyLilEnum Enum);
    
    public enum  MyLilEnum
    {
        Def,
        One,
        Two,
        Three
    }

    [Benchmark]
    public void ArrayByValue()
    {
        MyLilStruct[] arr = [
            new MyLilStruct(false, MyLilEnum.Def),
            new MyLilStruct(true, MyLilEnum.One),
            new MyLilStruct(false, MyLilEnum.Two),
            new MyLilStruct(true, MyLilEnum.Three)
        ];

        AcceptArrayByValue(arr);
    }

    private void AcceptArrayByValue(MyLilStruct[] arr)
    {
        
    }
    
    [Benchmark]
    public void ArrayByRef()
    {
        MyLilStruct[] arr = [
            new MyLilStruct(false, MyLilEnum.Def),
            new MyLilStruct(true, MyLilEnum.One),
            new MyLilStruct(false, MyLilEnum.Two),
            new MyLilStruct(true, MyLilEnum.Three)
        ];

        AcceptArrayByRef(arr);
    }

    private void AcceptArrayByRef(in MyLilStruct[] arr)
    {
        
    }
    
    [Benchmark]
    public void SpanByValue()
    {
        Span<MyLilStruct> span = [
            new MyLilStruct(false, MyLilEnum.Def),
            new MyLilStruct(true, MyLilEnum.One),
            new MyLilStruct(false, MyLilEnum.Two),
            new MyLilStruct(true, MyLilEnum.Three)
        ];

        AcceptSpanByValue(span);
    }
    
    private void AcceptSpanByValue(ReadOnlySpan<MyLilStruct> span)
    {
        
    }
    
    [Benchmark]
    public void SpanByRef()
    {
        Span<MyLilStruct> span = [
            new MyLilStruct(false, MyLilEnum.Def),
            new MyLilStruct(true, MyLilEnum.One),
            new MyLilStruct(false, MyLilEnum.Two),
            new MyLilStruct(true, MyLilEnum.Three)
        ];

        AcceptSpanByRef(span);
    }
    
    private void AcceptSpanByRef(in ReadOnlySpan<MyLilStruct> span)
    {
        
    }


    [Benchmark]
    public void LilStructByValue()
    {
        var str = new MyLilStruct(false, MyLilEnum.Def);
        AcceptLilStructByValue(str);
    }

    private void AcceptLilStructByValue(MyLilStruct str)
    {
        
    }
    
    [Benchmark]
    public void LilStructByRef()
    {
        var str = new MyLilStruct(false, MyLilEnum.Def);
        AcceptLilStructByRef(str);
    }

    private void AcceptLilStructByRef(in MyLilStruct str)
    {
        
    }
}