
internal static class PointerFunction
{
    internal unsafe static int* ToPointer(this int[] array)   // [] To Pointer
    {
        // = new double[array.Length];

        fixed (int* arr = array)
        {
            //Interop.CallFortranCode(arr);
            return arr;
        }
    }
}

