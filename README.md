# CSharp_DataParallelism
Some examples of data parallelism with TPL using Parallel.For, ForEach, Partitioner, and more ...

---
|Feature |Description |
|--------|------------|
|Parallel.For | A couple of examples that demonstrate 'Parallel.For' inculding one example that reads image files from the system and manipulates them. This example also compares a Parallel.For process against a standard 'for' process and outputs the time difference. |
|Parallel.ForEach | A couple of examples that demonstrate 'Parallel.ForEach' inculding one example that reads image files from the system and manipulates them. This example also compares a Parallel.ForEach process against a standard 'foreach' process and outputs the time difference. |
|Interlocked | Demonstrates how to provide atomic operations for variables that are shared by multiple threads |
| Thread Local Variables | Demonstrates how to avoid the overhead of synchronizing a large number of accesses to a shared state. Instead of writing to a shared resource on each iteration, you compute and store the value until all iterations for the task are complete |
|Exception Handling | An example of how to catch exceptions during a 'Parallel.ForEach' process. This example captures the exceptions and outputs them to a 'ConcurrentQueue' collection for later processing. |
|Cancelling a Parallel Loop | Uses 'CancellationTokenSource' to capture a canellation request |
|Performance Enhancements | Demonstrates the use of 'Partitioner' to increase performance when iterating over a small body collection. This example compares a standard 'foreach' against a 'Parallel.ForEach' without using Partitioner, and against a 'Parallel.ForEach' while using Partitioner. All processes are timed and results printed to console.|

---
####Language Features
|Feature|
|-------|
|Parallel.For|
|Parallel.ForEach|
|Interlocked.Add|
|Partitioner|
|AggregateException|
|ArgumentException|
|ConcurrentQueue|
|CancellationTokenSource|
|CancellationTokenSource.Token|
|StopWatch|
|System.IO|
|Directory|
|Graphics|
|Bitmap|
|Action Delegates|
|Func Delegates|

---
####Resources
| Title | Author | Publisher |
|--------------|---------|--------|
|Pro C# 5.0 and the .NET 4.5 Framework| Andrew Troelsen | APRESS |
| [Task Parallelism (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd537609(v=vs.110).aspx) |  | MSDN |
| [Data Parallelism (Task Parallel Library)](https://msdn.microsoft.com/en-us/library/dd537608(v=vs.110).aspx) |  | MSDN |
| [Parallel.ForEach() Vs Foreach() Loop in C#](http://www.c-sharpcorner.com/UploadFile/efa3cf/parallel-foreach-vs-foreach-loop-in-C-Sharp/)| Banketeshvar Narayan | C# Corner |
