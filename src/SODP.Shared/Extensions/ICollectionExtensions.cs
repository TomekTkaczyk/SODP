using System;
using System.Collections;
using System.Collections.Generic;

namespace SODP.Shared.Extensions
{
	public static class ICollectionExtensions
	{
		public static IReadOnlyCollection<T> AsReadOnly<T>(this ICollection<T> collection)
		{
			if(collection is null) throw new ArgumentNullException(nameof(collection));

			return collection as IReadOnlyCollection<T> ?? new ReadOnlyCollectionAdapter<T>(collection);
		}
	}

	internal sealed class ReadOnlyCollectionAdapter<T> : IReadOnlyCollection<T>
	{
		readonly ICollection<T> _source;

		public int Count => _source.Count;

		public ReadOnlyCollectionAdapter(ICollection<T> source) => _source = source;
		
		public IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
