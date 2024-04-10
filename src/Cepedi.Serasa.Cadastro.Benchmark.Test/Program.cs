// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using Cepedi.Serasa.Cadastro.Benchmark.Test;
using Cepedi.Serasa.Cadastro.Benchmark.Test.Helpers;
using Cepedi.Serasa.Cadastro.Benchmark.Tests;

//var summary = BenchmarkRunner.Run<StringConcatenationVsStringBuilderBenchmark>();
//var summary = BenchmarkRunner.Run<IterationBenchmark>();
//var summary = BenchmarkRunner.Run<ArrayCopyBenchmark>();
var summary = BenchmarkRunner.Run<DapperVsEfCoreBenchmark>();
