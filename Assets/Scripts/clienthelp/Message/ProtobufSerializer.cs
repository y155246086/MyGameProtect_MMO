using System;
using System.IO;

namespace Messages
{
	public interface IProtobufSerializer
	{
		void Serialize<T>(Stream dest, T value);
		object Deserialize(Stream source, Type type);
	}

#if !SERVER
	public class MetaDataProtobufSerializer : IProtobufSerializer
	{
		public void Serialize<T>(Stream dest, T value)
		{
			ProtoBuf.Serializer.Serialize(dest, value);
		}

		public object Deserialize(Stream source, Type type)
		{
			return ProtoBuf.Serializer.NonGeneric.Deserialize(type, source);
		}
	}

	public class TypeModelProtobufSerializer : IProtobufSerializer
	{
		private ProtoBuf.Meta.TypeModel typeModelSerializer = null;

		public TypeModelProtobufSerializer(ProtoBuf.Meta.TypeModel typeMode)
		{
			this.typeModelSerializer = typeMode;
		}

		public void Serialize<T>(Stream dest, T value)
		{
			typeModelSerializer.Serialize(dest, value);
		}

		public object Deserialize(Stream source, Type type)
		{
			return typeModelSerializer.Deserialize(source, null, type);
		}
	}
#endif
}
