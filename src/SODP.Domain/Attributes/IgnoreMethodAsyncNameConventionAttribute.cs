using System;

namespace SODP.Domain.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class IgnoreMethodAsyncNameConventionAttribute : Attribute { }
