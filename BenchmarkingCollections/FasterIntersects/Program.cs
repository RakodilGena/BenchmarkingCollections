// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FasterIntersects;

var summary = BenchmarkRunner.Run<Methods>();