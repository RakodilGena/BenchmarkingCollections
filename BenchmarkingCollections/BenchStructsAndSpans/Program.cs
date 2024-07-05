// See https://aka.ms/new-console-template for more information

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Running;
using BenchStructsAndSpans;

var s = Unsafe.SizeOf<Bench.MyLilStruct>();
Console.WriteLine(s);

var summary = BenchmarkRunner.Run<Bench>();