using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClientDependency.Core;
using umbraco.interfaces;

namespace _4Ben.DataTypes.MultiType.Helpers.PrevalueEditors
{
	/// <summary>
	/// Abstract class for the PreValue Editor.
	/// </summary>
	public abstract class AbstractPrevalueEditor : WebControl, IDataPrevalue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractPrevalueEditor"/> class.
		/// </summary>
		public AbstractPrevalueEditor()
			: base()
		{
		}

		/// <summary>
		/// Gets the editor.
		/// </summary>
		/// <value>The editor.</value>
		public virtual Control Editor
		{
			get
			{
				return this;
			}
		}

		/// <summary>
		/// Saves this instance.
		/// </summary>
		public virtual void Save()
		{
		}

		/// <summary>
		/// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}
	}
}
