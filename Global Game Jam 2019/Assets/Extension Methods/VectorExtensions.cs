using UnityEngine;

/// <summary>
/// Vector extensions.
/// </summary>
public static class VectorExtensions
{
	#region Vector2

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector2</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector2 With(this Vector2 original, float? x = null, float? y = null)
	{
		return new Vector2(x ?? original.x, y ?? original.y);
	}

	#endregion

	#region Vector2Int

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector2Int</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public static Vector2Int With(this Vector2Int original, int? x = null, int? y = null)
	{
		return new Vector2Int(x ?? original.x, y ?? original.y);
	}

	#endregion

	#region Vector3

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector3</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
	{
		return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
	}

	/// <summary>
	/// Direction to another <c>Vector3</c>.
	/// </summary>
	/// <returns>The direction vector to <c>destination</c>.</returns>
	/// <param name="destination">Destination.</param>
	public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
	{
		return Vector3.Normalize(destination - source);
	}

	#endregion

	#region Vector3Int

	/// <summary>
	/// Change the values of x, y and/or z.
	/// </summary>
	/// <returns>The <c>Vector3Int</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public static Vector3Int With(this Vector3Int original, int? x = null, int? y = null, int? z = null)
	{
		return new Vector3Int(x ?? original.x, y ?? original.y, z ?? original.z);
	}

	#endregion

	#region Vector4

	/// <summary>
	/// Change the values of x, y, z and/or w.
	/// </summary>
	/// <returns>The <c>Vector4</c> with the specified values.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="w">The w coordinate.</param>
	public static Vector4 With(this Vector4 original, float? x = null, float? y = null, float? z = null, float? w = null)
	{
		return new Vector4(x ?? original.x, y ?? original.y, z ?? original.z, w ?? original.w);
	}

	#endregion
}
