using System;
using System.Collections.Generic;

namespace Fontify.Core
{
	public interface ISystemFontCollectionModel
	{
		/// <summary>
		/// Updates the model
		/// </summary>
		void FetchFonts();

		/// <summary>
		/// Gets the fonts.
		/// </summary>
		/// <value>The fonts.</value>
		IEnumerable<SystemFont> Fonts { get; }
	}
}

