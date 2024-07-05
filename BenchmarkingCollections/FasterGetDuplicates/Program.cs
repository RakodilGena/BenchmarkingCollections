// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FasterGetDuplicates;

var summary = BenchmarkRunner.Run<Bench>();