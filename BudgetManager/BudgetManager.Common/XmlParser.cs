using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BudgetManager.Common
{
	/// <summary>
	/// XML parser helper class
	/// </summary>
	public static class XmlParser
	{
		/// <summary>
		/// XML to object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		public static T XmlToObject<T>(string xml) where T : new()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (T));
			using (StringReader stringReader = new StringReader(xml))
			{
				using (XmlTextReader xmlTextReader = new XmlTextReader(stringReader))
				{
					try
					{
						T o = (T) serializer.Deserialize(xmlTextReader);
						return o;
					}
					catch
					{
						return default(T);
					}
				}
			}
		}

		/// <summary>
		/// Object to XML.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj">The obj.</param>
		/// <returns></returns>
		public static string ObjectToXml<T>(T obj)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (T));
			using (StringWriter stringWriter = new StringWriter())
			{
				try
				{
					serializer.Serialize(stringWriter, obj);
					return stringWriter.ToString();
				}
				catch
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Removes all namespaces.
		/// </summary>
		/// <param name="xml">XML string</param>
		/// <returns></returns>
		public static string RemoveAllNamespaces(string xml)
		{
			XElement xmlNoNamespaces = RemoveAllNamespaces(XElement.Parse(xml));
			return xmlNoNamespaces.ToString();
		}

		/// <summary>
		/// Removes all namespaces.
		/// </summary>
		/// <param name="xmlElement">The XML element.</param>
		/// <returns></returns>
		private static XElement RemoveAllNamespaces(XElement xmlElement)
		{
			if (!xmlElement.HasElements)
			{
				XElement xElement = new XElement(xmlElement.Name.LocalName);
				xElement.Value = xmlElement.Value;
				foreach (XAttribute attribute in xmlElement.Attributes())
				{
					xElement.Add(attribute);
				}
				return xElement;
			}
			return new XElement(xmlElement.Name.LocalName, xmlElement.Elements().Select(e => RemoveAllNamespaces(e)));
		}

		/// <summary>
		/// Removes all empty elements.
		/// </summary>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		public static string RemoveAllEmptyElements(string xml)
		{
			XDocument xmlDoc = RemoveAllEmptyElements(XDocument.Parse(xml));
			return xmlDoc.ToString(SaveOptions.DisableFormatting);
		}

		/// <summary>
		/// Removes all empty elements.
		/// </summary>
		/// <param name="xmlDoc">The XML doc.</param>
		/// <returns></returns>
		private static XDocument RemoveAllEmptyElements(XDocument xmlDoc)
		{
			xmlDoc.Descendants()
				.Where(e => e.IsEmpty || string.IsNullOrWhiteSpace(e.Value))
				.Remove();
			return xmlDoc;
		}
	}
}