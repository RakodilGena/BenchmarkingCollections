// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using UnionLists;

var summary = BenchmarkRunner.Run<Methods>();