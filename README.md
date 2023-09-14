# BenchmarkingCollections
benchmarking collections

| Method                     | RepeatCount | Mean      | Error    | StdDev   | Gen0   | Allocated |
|--------------------------- |------------ |----------:|---------:|---------:|-------:|----------:|
| Array                      | 2000        |  39.79 us | 0.699 us | 0.933 us |      - |     264 B |
| ArrayAsIEnumerable         | 2000        | 344.01 us | 1.468 us | 1.226 us | 7.3242 |   64264 B |
| ArrayAsICollection         | 2000        | 344.80 us | 3.574 us | 3.168 us | 7.3242 |   64264 B |
| ArrayAsIList               | 2000        | 349.08 us | 5.247 us | 4.908 us | 7.3242 |   64264 B |
| ArrayAsIReadOnlyCollection | 2000        | 343.95 us | 2.375 us | 2.106 us | 7.3242 |   64264 B |
| ArrayAsIReadOnlyList       | 2000        | 346.34 us | 3.523 us | 3.295 us | 7.3242 |   64264 B |
| List                       | 2000        |  57.18 us | 1.072 us | 0.950 us |      - |     296 B |
| ListAsIEnumerable          | 2000        | 560.47 us | 4.713 us | 4.409 us | 8.7891 |   80297 B |
| ListAsICollection          | 2000        | 568.87 us | 7.171 us | 6.707 us | 8.7891 |   80297 B |
| ListAsIList                | 2000        | 560.94 us | 4.075 us | 3.613 us | 8.7891 |   80297 B |
| ListAsIReadOnlyCollection  | 2000        | 568.11 us | 7.731 us | 7.231 us | 8.7891 |   80297 B |
| ListAsIReadOnlyList        | 2000        | 560.38 us | 3.952 us | 3.300 us | 8.7891 |   80297 B |
| HashSet                    | 2000        | 171.40 us | 1.654 us | 1.547 us | 0.2441 |    2832 B |
| HashSetAsISet              | 2000        | 559.45 us | 5.483 us | 4.861 us | 9.7656 |   82833 B |
| HashSetAsIReadOnlySet      | 2000        | 566.33 us | 6.397 us | 5.983 us | 9.7656 |   82833 B |

anything which is not purely iterated as it is used to allocate enumerator on heap