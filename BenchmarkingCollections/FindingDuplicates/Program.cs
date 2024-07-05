// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FindingDuplicates;

var summary = BenchmarkRunner.Run<BenchClass>();