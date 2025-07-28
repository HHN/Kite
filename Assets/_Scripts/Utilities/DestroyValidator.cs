namespace Assets._Scripts.Utilities
{
    /// <summary>
    /// Provides an extension method to safely check if an object, especially a Unity Engine object,
    /// is null or has been destroyed. This is crucial in Unity to prevent "MissingReferenceException"
    /// when checking objects that might have been destroyed but still hold a non-null C# reference.
    /// </summary>
    public static class DestroyValidator
    {
        /// <summary>
        /// Determines whether an object is null or, if it's a Unity Engine object, whether it has been destroyed.
        /// This extension method helps prevent MissingReferenceException errors.
        /// </summary>
        /// <param name="obj">The object instance to check.</param>
        /// <returns>
        /// True if the object is genuinely null, or if it's a Unity.Object that has been destroyed;
        /// otherwise, returns false.
        /// </returns>
        public static bool IsNullOrDestroyed(this object obj)
        {
            if (ReferenceEquals(obj, null)) return true;

            if (obj is UnityEngine.Object) return obj as UnityEngine.Object == null;

            return false;
        }
    }
}
