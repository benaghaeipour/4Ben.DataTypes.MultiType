using System;
using System.Web.UI;

using ClientDependency.Core;
using ClientDependency.Core.Controls;
using umbraco;

namespace _4Ben.DataTypes.MultiType.Helpers
{
	/// <summary>
	/// Extension methods for embedded resources
	/// </summary>
	public static class ResourceExtensions
	{

		/// <summary>
		/// Adds an embedded resource to the ClientDependency output by name
		/// </summary>
		/// <param name="ctl">The CTL.</param>
		/// <param name="resourceName">Name of the resource.</param>
		/// <param name="type">The type.</param>
		public static void AddResourceToClientDependency(this Control ctl, string resourceName, ClientDependencyType type)
		{
            ctl.Page.AddResourceToClientDependency(typeof(_4Ben.DataTypes.MultiType.Helpers.ResourceExtensions), resourceName, type, 100);
		}


		/// <summary>
		/// Adds an embedded resource to the ClientDependency output by name
		/// </summary>
		/// <param name="page">The Page to add the resource to</param>
		/// <param name="resourceContainer">The type containing the embedded resourcre</param>
		/// <param name="resourceName">Name of the resource.</param>
		/// <param name="type">The type.</param>
		/// <param name="priority">The priority.</param>
		public static void AddResourceToClientDependency(this Page page, Type resourceContainer, string resourceName, ClientDependencyType type, int priority)
		{
			// get the urls for the embedded resources
			var resourceUrl = page.ClientScript.GetWebResourceUrl(resourceContainer, resourceName);


            Control target = page.Header;
            if (target != null) //if there's no <head runat="server" /> don't throw an exception.
            {                

                ////// check the Umbraco version, v4.6.0 shipped with ClientDependency 1.2
                ////if (GlobalSettings.VersionMajor >= 4 && GlobalSettings.VersionMinor >= 6)
                ////{
                ////    // add the resources to client dependency
                ////    ClientDependencyLoader.Instance.RegisterDependency(resourceUrl, type);
                ////}
                ////else
                ////{
                // Umbraco v4.5.x shipped with earlier version of ClientDependency - which had an issue with querystrings in virtual paths.
                switch (type)
                {
                    case ClientDependencyType.Css:
                        target.Controls.Add(
                            new LiteralControl("<link type='text/css' rel='stylesheet' href='" + page.Server.HtmlEncode(resourceUrl) + "' />"));
                        break;

                    case ClientDependencyType.Javascript:
                        target.Controls.Add(
                            new LiteralControl("<script type='text/javascript' src='" + page.Server.HtmlEncode(resourceUrl) + "'></script>"));
                        break;

                    default:
                        break;
                }
            }
			////}
		}
	}
}
