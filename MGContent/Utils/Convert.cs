namespace MGContent;

/// <summary>
/// Utilities for converting types.
/// </summary>
static partial class Utils
{
	public static Vector2 ImVec2ToVector2(ImVec2 vec)
	{
		return new Vector2(vec.X, vec.Y);
	}

	public static ImVec2 Vector2ToImVec2(Vector2 vec)
	{
		return new ImVec2(vec.X, vec.Y);
	}
}
