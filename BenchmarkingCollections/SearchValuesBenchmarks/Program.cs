// See https://aka.ms/new-console-template for more information

using System.Buffers;
using System.Collections.Frozen;
using System.Diagnostics;
using BenchmarkDotNet.Running;
using SearchValuesBenchmarks;

var summary = BenchmarkRunner.Run<BenchmarksInvalidEnd>();

return;

var validStr = "ABCD4809FG94VK451";
var invalidStr = "ABCD4809^FG94VK451";

char[] AllowedSymbolsArray = 
{
    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
    'O', 'P', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
};
HashSet<char> AllowedSymbolsHashSet = AllowedSymbolsArray.ToHashSet();
FrozenSet<char> AllowedSymbolsFrozenSet = AllowedSymbolsArray.ToFrozenSet();
SearchValues<char> AllowedSymbolsSearchValues = SearchValues.Create(AllowedSymbolsArray);

var resultsValid = new bool[]
{
    Naive_Valid(validStr),
    Span_Valid(validStr),
    HashSet_Valid(validStr),
    FrozenSet_Valid(validStr),
    SearchValues_Valid(validStr)
};
Debug.Assert(resultsValid.All(r => r));

var resultsInvalid = new bool[]
{
    Naive_Valid(invalidStr),
    Span_Valid(invalidStr),
    HashSet_Valid(invalidStr),
    FrozenSet_Valid(invalidStr),
    SearchValues_Valid(invalidStr)
};
Debug.Assert(resultsInvalid.All(r => r is false));

bool Naive_Valid(string str)
{
    foreach (var ch in str)
    {
        if (AllowedSymbolsArray.Contains(ch) is false)
            return false;
    }

    return true;
}
    

 bool Span_Valid(string str)
{
    return str.AsSpan().IndexOfAnyExcept(AllowedSymbolsArray) is -1;
}
    
    
    
 bool HashSet_Valid(string str)
{
    foreach (var ch in str)
    {
        if (AllowedSymbolsHashSet.Contains(ch) is false)
            return false;
    }

    return true;
}
    
bool FrozenSet_Valid(string str)
{
    foreach (var ch in str)
    {
        if (AllowedSymbolsFrozenSet.Contains(ch) is false)
            return false;
    }

    return true;
}
    
 bool SearchValues_Valid(string str)
{
    return str.AsSpan().IndexOfAnyExcept(AllowedSymbolsSearchValues) is -1;
}