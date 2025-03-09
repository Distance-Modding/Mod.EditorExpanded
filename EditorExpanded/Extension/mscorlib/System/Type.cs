#pragma warning disable RCS1110
using System;
using System.Collections.Generic;
using System.Linq;

public static class System__TypeExtensions
{
	/// <summary>
	/// Returns attributes of type <typeparamref name="T"/>
	/// </summary>
	public static IEnumerable<T> GetCustomAttributes<T>(this Type type, bool inherit = true) where T : Attribute
	=> type.GetCustomAttributes(inherit).OfType<T>();

	/// <summary>
	/// Returns an attribute of type <typeparamref name="T"/>
	/// </summary>
	public static T GetCustomAttribute<T>(this Type type, bool inherit = true) where T : Attribute
	=> type.GetCustomAttributes<T>(inherit).FirstOrDefault();
}