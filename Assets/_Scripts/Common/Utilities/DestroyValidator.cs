namespace Assets._Scripts.Common.Utilities
{
    public static class DestroyValidator
    {
        public static bool IsNullOrDestroyed(this System.Object obj)
        {
            if (ReferenceEquals(obj, null)) return true;

            if (obj is UnityEngine.Object) return (obj as UnityEngine.Object) == null;

            return false;
        }
    }
}
