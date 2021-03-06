﻿// Copyright 2005-2015 Giacomo Stelluti Scala & Contributors. All rights reserved. See doc/License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine.Infrastructure;

namespace CommandLine.Core
{
    internal static class Sequence
    {
        public static IEnumerable<Token> Partition(
            IEnumerable<Token> tokens,
            Func<string, Maybe<TypeDescriptor>> typeLookup)
        {
            return from tseq in tokens.Pairwise(
                (f, s) =>
                        f.IsName() && s.IsValue()
                            ? typeLookup(f.Text).Return(info =>
                                   info.TargetType == TargetType.Sequence
                                        ? new[] { f }.Concat(tokens.OfSequence(f, info))
                                        : new Token[] { }, new Token[] { })
                            : new Token[] { })
                   from t in tseq
                   select t;
        }

        private static IEnumerable<Token> OfSequence(this IEnumerable<Token> tokens, Token nameToken, TypeDescriptor info)
        {
            var nameIndex = tokens.IndexOf(t => t.Equals(nameToken));
            if (nameIndex >= 0)
            {
                return info.NextValue.Return(
                    _ => info.MaxItems.Return(
                            n => tokens.Skip(nameIndex + 1).Take(n),
                                 tokens.Skip(nameIndex + 1).TakeWhile(v => v.IsValue())),
                    tokens.Skip(nameIndex + 1).TakeWhile(v => v.IsValue()));
            }
            return new Token[] { };
        }
    }
}
