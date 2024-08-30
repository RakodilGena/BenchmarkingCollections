// See https://aka.ms/new-console-template for more information


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


// var tests = new Tests();
// tests.Count = 10;
//
// var list1 = tests.GetReversedCollection_List();
// var list2 = tests.GetReversedCollection_Stack();
//
// var a = 5;

var summary = BenchmarkRunner.Run<Tests>();

[MemoryDiagnoser(true)]
public class Tests
{
    [Params(2, 3, 4, 5, 10, 20, 50, 100, 1000)]
    public int Count;

    [Benchmark(Baseline = true)]
    public List<int> GetReversedCollection_List()
    {
        var list = new List<int>();
        for (int i = 0; i < Count; i++)
        {
            list.Insert(0, i);
        }

        return list;
    }
    
    
    [Benchmark]
    public List<int> GetReversedCollection_Stack()
    {
        var stack = new Stack<int>();
        for (int i = 0; i < Count; i++)
        {
            stack.Push(i);
        }

        return stack.ToList();
    }
    
}