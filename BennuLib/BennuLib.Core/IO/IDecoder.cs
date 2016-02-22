using System.Collections.Generic;
using System.IO;

namespace Bennu
{


	public interface IDecoder<T>
	{
		IEnumerable<string> SupportedExtensions { get; }
		bool TryDecode(Stream input, out T decoded);
		T Decode(Stream input);
	}
}

