﻿// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

namespace CommandLine.Tests.Fakes
{
    class FakeOptionsWithNullables
    {
        [Option('i', "nullable-int")]
        public int? NullableInt { get; set; }

        [Value(0)]
        public long? NullableLong { get; set; }
    }
}
