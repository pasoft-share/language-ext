﻿using System;
using LanguageExt;
using static LanguageExt.Prelude;
using LanguageExt.UnitsOfMeasure;

namespace LanguageExt.Config
{
    public enum ArgumentTypeTag
    {
        Unknown,
        Bool,
        Int,
        Double,
        String,
        Time,
        ProcessId,
        ProcessName,
        ProcessFlags,
        Array,
        Process,
        Strategy,
        StrategyMatch,
        StrategyRedirect,
        Directive,
        Map,
        Dispatcher,
        DispatcherType,
        Cluster
    }

    public class ArgumentType
    {
        public readonly ArgumentTypeTag Tag;
        public readonly ArgumentType GenericType;

        ArgumentType(ArgumentTypeTag tag)
        {
            Tag = tag;
        }

        ArgumentType(ArgumentTypeTag tag, ArgumentType genericType)
        {
            Tag = tag;
            GenericType = genericType;
        }

        public readonly static ArgumentType Unknown =
            new ArgumentType(ArgumentTypeTag.Unknown);

        public readonly static ArgumentType Bool =
            new ArgumentType(ArgumentTypeTag.Bool);

        public readonly static ArgumentType Int = 
            new ArgumentType(ArgumentTypeTag.Int);

        public readonly static ArgumentType Double =
            new ArgumentType(ArgumentTypeTag.Double);

        public readonly static ArgumentType String =
            new ArgumentType(ArgumentTypeTag.String);

        public readonly static ArgumentType Time =
            new ArgumentType(ArgumentTypeTag.Time);

        public readonly static ArgumentType ProcessId =
            new ArgumentType(ArgumentTypeTag.ProcessId);

        public readonly static ArgumentType ProcessName =
            new ArgumentType(ArgumentTypeTag.ProcessName);

        public readonly static ArgumentType ProcessFlags =
            new ArgumentType(ArgumentTypeTag.ProcessFlags);

        public static ArgumentType Process =>
            new ArgumentType(ArgumentTypeTag.Process);

        public static ArgumentType Strategy =>
            new ArgumentType(ArgumentTypeTag.Strategy);

        public static ArgumentType StrategyMatch =>
            new ArgumentType(ArgumentTypeTag.StrategyMatch);

        public static ArgumentType StrategyRedirect =>
            new ArgumentType(ArgumentTypeTag.StrategyRedirect);

        public static ArgumentType Array(ArgumentType genericType) =>
            new ArgumentType(ArgumentTypeTag.Array, genericType);

        public static ArgumentType Map(ArgumentType genericType) =>
            new ArgumentType(ArgumentTypeTag.Map, genericType);

        public static ArgumentType Directive =>
            new ArgumentType(ArgumentTypeTag.Directive);

        public static ArgumentType Dispatcher =>
            new ArgumentType(ArgumentTypeTag.Dispatcher);

        public static ArgumentType DispatcherType =>
            new ArgumentType(ArgumentTypeTag.DispatcherType);

        public static ArgumentType Cluster =>
            new ArgumentType(ArgumentTypeTag.Cluster);

    }

    public class FieldSpec
    {
        public readonly string Name;
        public readonly ArgumentType Type;

        internal FieldSpec(string name, ArgumentType type)
        {
            Name = name;
            Type = type;
        }

        public static FieldSpec Int(string name) =>
            new FieldSpec(name, ArgumentType.Int);

        public static FieldSpec Bool(string name) =>
            new FieldSpec(name, ArgumentType.Bool);

        public static FieldSpec Double(string name) =>
            new FieldSpec(name, ArgumentType.Double);

        public static FieldSpec String(string name) =>
            new FieldSpec(name, ArgumentType.String);

        public static FieldSpec Time(string name) =>
            new FieldSpec(name, ArgumentType.Time);

        public static FieldSpec ProcessId(string name) =>
            new FieldSpec(name, ArgumentType.ProcessId);

        public static FieldSpec ProcessName(string name) =>
            new FieldSpec(name, ArgumentType.ProcessName);

        public static FieldSpec ProcessFlags(string name) =>
            new FieldSpec(name, ArgumentType.ProcessFlags);

        public static FieldSpec Process(string name) =>
            new FieldSpec(name, ArgumentType.Process);

        public static FieldSpec Strategy(string name) =>
            new FieldSpec(name, ArgumentType.Strategy);

        public static FieldSpec StrategyMatch(string name) =>
            new FieldSpec(name, ArgumentType.StrategyMatch);

        public static FieldSpec StrategyRedirect(string name) =>
            new FieldSpec(name, ArgumentType.StrategyRedirect);

        public static FieldSpec Array(string name, ArgumentType genericType) =>
            new FieldSpec(name, ArgumentType.Array(genericType));

        public static FieldSpec Map(string name, ArgumentType genericType) =>
            new FieldSpec(name, ArgumentType.Map(genericType));

        public static FieldSpec Directive(string name) =>
            new FieldSpec(name, ArgumentType.Directive);

        public static FieldSpec Dispatcher(string name) =>
            new FieldSpec(name, ArgumentType.Dispatcher);

        public static FieldSpec DispatcherType(string name) =>
            new FieldSpec(name, ArgumentType.DispatcherType);

        public static FieldSpec Cluster(string name) =>
            new FieldSpec(name, ArgumentType.Cluster);
    }

    public class ArgumentsSpec
    {
        public readonly FieldSpec[] Args;
        public readonly Func<Map<string,ValueToken>, object> Ctor;

        public ArgumentsSpec(Func<Map<string, ValueToken>, object> ctor, params FieldSpec[] args)
        {
            Args = args;
            Ctor = ctor;
        }

        public ArgumentsSpec(params FieldSpec[] args)
            :
            this(null, args)
        {
        }

        public static ArgumentsSpec Variant(Func<Map<string, ValueToken>, object> ctor, params FieldSpec[] args) =>
            new ArgumentsSpec(ctor, args);

        public static ArgumentsSpec Variant(params FieldSpec[] args) =>
            new ArgumentsSpec(args);
    }

    public class FuncSpec
    {
        public readonly string Name;
        public readonly ArgumentsSpec[] Variants;

        public FuncSpec(string name, params ArgumentsSpec[] variants)
        {
            Name = name;
            Variants = variants;
        }

        public static FuncSpec Attr(string name, params ArgumentsSpec[] variants) =>
            new FuncSpec(name, variants);

        public static FuncSpec AttrNoArgs(string name) =>
            new FuncSpec(name, new[] { new ArgumentsSpec() });

        public static FuncSpec AttrNoArgs(string name, Func<string, ValueToken, object> f) =>
            new FuncSpec(name, new[] { new ArgumentsSpec() });

        public static FuncSpec Attr(string name, params FieldSpec[] args) =>
            new FuncSpec(name, new [] { new ArgumentsSpec(args) });

        public static FuncSpec Attr(string name, Func<Map<string, ValueToken>, object> f,  params FieldSpec[] args) =>
            new FuncSpec(name, new[] { new ArgumentsSpec(f, args) });

    }
}
