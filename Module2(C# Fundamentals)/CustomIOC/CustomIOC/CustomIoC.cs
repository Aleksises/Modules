using CustomIOC.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CustomIOC
{
    public class CustomIoC
    {
        private Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        public void AddType(Type type)
        {
            AddType(type, type);
        }

        public void AddType(Type type, Type dependencyType)
        {
            if (_registrations.ContainsKey(dependencyType))
            {
                throw new InvalidOperationException($"You've already registered {dependencyType.Name}");
            }
            _registrations.Add(dependencyType, type);
        }

        public void AddAssembly(Assembly assembly)
        {
            var classes = assembly.GetTypes().Where(t => t.IsClass);
            var importClasses = classes.Where(c => c.CustomAttributes.Any(a => a.AttributeType == typeof(ImportConstructorAttribute)));
            var exportClasses = classes.Where(c => c.CustomAttributes.Any(a => a.AttributeType == typeof(ExportAttribute)));

            foreach (var exportClass in exportClasses)
            {
                var exportAttribute = (ExportAttribute)Attribute.GetCustomAttribute(exportClass, typeof(ExportAttribute));
                if (exportAttribute.Contract != null)
                {
                    AddType(exportClass, exportAttribute.Contract);
                }
                else
                {
                    AddType(exportClass);
                }
            }

            foreach (var importClass in importClasses)
            {
                AddType(importClass);
            }

        }

        public T CreateInstance<T>() where T : class
        {
            var objectType = typeof(T);
            if (_registrations.ContainsKey(objectType))
            {
                return (T)GetInstance(objectType);
            }
            else
            {
                throw new ArgumentException($"You didn't register {objectType.Name}!");
            }
        }

        public object CreateInstance(Type type)
        {
            if (_registrations.ContainsKey(type))
            {
                return GetInstance(type);
            }
            else
            {
                throw new ArgumentException($"You didn't register {type.Name}!");
            }
        }

        private object GetInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();
            
            if (constructors.Any())
            {
                var constructor = constructors.Single();
                var parameters = constructor.GetParameters().Select(p => p.ParameterType);
                var dependencies = parameters.Select(d => CreateInstance(d)).ToArray();

                return Activator.CreateInstance(implementationType, dependencies);
            }

            return Activator.CreateInstance(_registrations[implementationType]);
        }
    }
}
