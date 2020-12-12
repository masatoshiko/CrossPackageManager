using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGettext;

namespace CrossPackageManager
{
	internal class T
	{
		private static readonly ICatalog _Catalog = new Catalog("CrsPkg", "./locale");


		public static string _(string text)
		{
			return _Catalog.GetString(text);
		}

		public static string _(string text, params object[] args)
		{
			return _Catalog.GetString(text, args);
		}

		public static string _n(string text, string pluralText, long n)
		{
			return _Catalog.GetPluralString(text, pluralText, n);
		}

		public static string _n(string text, string pluralText, long n, params object[] args)
		{
			return _Catalog.GetPluralString(text, pluralText, n, args);
		}

		public static string _p(string context, string text)
		{
			return _Catalog.GetParticularString(context, text);
		}

		public static string _p(string context, string text, params object[] args)
		{
			return _Catalog.GetParticularString(context, text, args);
		}

		public static string _pn(string context, string text, string pluralText, long n)
		{
			return _Catalog.GetParticularPluralString(context, text, pluralText, n);
		}

		public static string _pn(string context, string text, string pluralText, long n, params object[] args)
		{
			return _Catalog.GetParticularPluralString(context, text, pluralText, n, args);
		}
	}
}
