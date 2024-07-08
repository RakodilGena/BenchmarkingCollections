// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FastestEnumCollectionContains;

var summary = BenchmarkRunner.Run<Methods>();