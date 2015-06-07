namespace CommandLine.FSharp.Tests

    open CommandLine

    type FakeOptions() =
        let mutable stringValue = ""
        let mutable intSequence = Seq.empty<int>
        let mutable boolValue = false
        let mutable longValue = 0L

        [<Option(HelpText = "Define a string value here.")>]
        member this.StringValue with get() = stringValue and set(value) = stringValue <- value

        [<Option('i', Min = 3, Max = 4, HelpText = "Define a int sequence here.")>]
        member this.IntSequence with get() = intSequence and set(value) = intSequence <- value

        [<Option('x', HelpText = "Define a boolean or switch value here.")>]
        member this.BoolValue with get() = boolValue and set(value) = boolValue <- value

        [<Value(0)>]
        member this.LongValue with get() = longValue and set(value) = longValue <- value

    module parse_all_options =
        open Xunit
        open CommandLine.FSharp

        let parsed = ArgParser.ParseOptions<FakeOptions> ParserConfig.Default [| "--string"; "hello fsharp"; "-i3"; "-x"; "999" |]

        [<Fact>]
        let parse_string_value() =
            Assert.Equal("hello fsharp", parsed.Value.StringValue)
