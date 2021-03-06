﻿using System;

namespace CustomIOC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public Type Contract { get; private set; }

        public ExportAttribute()
        {
        }

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }
    }
}
