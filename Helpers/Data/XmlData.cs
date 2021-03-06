﻿using System;
using System.Xml;

using umbraco.cms.businesslogic.datatype;

namespace _4Ben.DataTypes.MultiType.Helpers.Data
{
	/// <summary>
	/// Overrides the <see cref="umbraco.cms.businesslogic.datatype.DefaultData"/> object to return the value as XML.
	/// </summary>
	public class XmlData : DefaultData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlData"/> class.
		/// </summary>
		/// <param name="dataType">Type of the data.</param>
		public XmlData(BaseDataType dataType)
			: base(dataType)
		{
		}

		/// <summary>
		/// Converts the data value to XML.
		/// </summary>
		/// <param name="data">The data to convert to XML.</param>
		/// <returns></returns>
		public override XmlNode ToXMl(XmlDocument data)
		{
			// check that the value isn't null and starts with an opening angle-bracket.
			if (this.Value != null && !string.IsNullOrEmpty(this.Value.ToString()) && this.Value.ToString().StartsWith("<"))
			{
				// load the value into an XML document.
				XmlDocument xd = new XmlDocument();
				xd.LoadXml(this.Value.ToString());

				// return the XML node.
				return data.ImportNode(xd.DocumentElement, true);
			}
			else
			{
				// otherwise render the value as default (in CDATA)
				return base.ToXMl(data);
			}
		}
	}
}
