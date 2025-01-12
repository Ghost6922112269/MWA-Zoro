using Alkahest.Core.Game;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;

namespace Alkahest.Core.Net.Game.Serialization
{
    public abstract class PacketFieldInfo
    {
        public PropertyInfo Property { get; }

        public PacketFieldOptionsAttribute Attribute { get; }

        public bool IsArray { get; }

        public bool IsByteArray { get; }

        public bool IsString { get; }

        public bool IsPrimitive { get; }

        protected PacketFieldInfo(PropertyInfo property, PacketFieldOptionsAttribute attribute)
        {
            Property = property ?? throw new ArgumentNullException(nameof(property));
            Attribute = attribute;

            var type = property.PropertyType;
            var isArray = type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
            var elemType = isArray ? type.GetGenericArguments()[0] : null;
            var isByteArray = elemType == typeof(byte);

            IsArray = isArray && !isByteArray;
            IsByteArray = isArray && isByteArray;
            IsString = type == typeof(string);
            IsPrimitive = IsPrimitiveType(type);
        }

        internal static bool IsPrimitiveType(Type type)
        {
            if (type.IsEnum)
                type = type.GetEnumUnderlyingType();

            return type.IsPrimitive ||
                type == typeof(Vector3) ||
                type == typeof(GameId) ||
                type == typeof(SkillId) ||
                type == typeof(Angle) ||
                type == typeof(TemplateId) ||
                type == typeof(Appearance);
        }
    }
}
