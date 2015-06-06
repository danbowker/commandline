namespace CommandLine.FSharp.Tests

open CommandLine
open CommandLine.FSharp

type FakeOptions() =
    [<Option(HelpText = "Define a string value here.")>]
    member val StringValue : string = "" with get, set

    [<Option('i', Min = 3, Max = 4, HelpText = "Define a int sequence here.")>]
    member val IntSequence : seq<int> = Seq.empty<int> with get, set

    [<Option('x', HelpText = "Define a boolean or switch value here.")>]
    member val BoolValue = false with get, set

    [<Value(0)>]
    member val LongValue = 0 with get, set