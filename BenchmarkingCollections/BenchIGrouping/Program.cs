﻿// See https://aka.ms/new-console-template for more information

using BenchIGrouping;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run(typeof(Benchmarks));