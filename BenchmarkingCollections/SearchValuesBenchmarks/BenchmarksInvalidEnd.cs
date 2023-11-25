﻿using System.Buffers;
using System.Collections.Frozen;
using BenchmarkDotNet.Attributes;

namespace SearchValuesBenchmarks;

[MemoryDiagnoser()]
public class BenchmarksInvalidEnd
{
    private static readonly char[] AllowedSymbolsArray = 
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 
        'O', 'P', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    private static readonly HashSet<char> AllowedSymbolsHashSet = AllowedSymbolsArray.ToHashSet();

    private static readonly FrozenSet<char> AllowedSymbolsFrozenSet = AllowedSymbolsArray.ToFrozenSet();

    private static readonly SearchValues<char> AllowedSymbolsSearchValues = SearchValues.Create(AllowedSymbolsArray);
    
    [Params(
        "AFHB58AHBIF49GJCNSCYJKL234FZ^XCV",
        "AFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL23^4FZXCV",
        "AFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL^234FZXCV",
        "AFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJKL234FZXCVAFHB58AHBIF49GJCNSCYJK^L234FZXCV")]
    public string InvalidStringEnd = null!;
    
    
    
    [Benchmark]
    public bool Naive_InValid_End()
    {
        foreach (var ch in InvalidStringEnd)
        {
            if (AllowedSymbolsArray.Contains(ch) is false)
                return false;
        }

        return true;
    }
    
    [Benchmark]
    public bool HashSet_InValid_End()
    {
        foreach (var ch in InvalidStringEnd)
        {
            if (AllowedSymbolsHashSet.Contains(ch) is false)
                return false;
        }

        return true;
    }
    
    [Benchmark]
    public bool FrozenSet_InValid_End()
    {
        foreach (var ch in InvalidStringEnd)
        {
            if (AllowedSymbolsFrozenSet.Contains(ch) is false)
                return false;
        }

        return true;
    }
    
    [Benchmark]
    public bool Span_InValid_End()
    {
        return InvalidStringEnd.AsSpan().IndexOfAnyExcept(AllowedSymbolsArray) is -1;
    }

    [Benchmark]
    public bool SearchValues_InValid_End()
    {
        return InvalidStringEnd.AsSpan().IndexOfAnyExcept(AllowedSymbolsSearchValues) is -1;
    }
}