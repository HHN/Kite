namespace Assets._Scripts.Utilities
{
    public static class DestroyValidator
    {
        public static bool IsNullOrDestroyed(this object obj)
        {
            if (ReferenceEquals(obj, null)) return true;

            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

            return false;
        }
    }
}
