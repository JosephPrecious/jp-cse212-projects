public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Create a new array with the requested length.
        // 2. Loop through each position in the array.
        // 3. For each position, multiply the given number by the
        //    position plus one to get the correct multiple.
        // 4. Store the calculated multiple in the current position
        //    of the array.
        // 5. Return the completed array.

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // 1. Find the index where the last 'amount' elements begin.
        // 2. Create a new list containing the elements from that index
        //    to the end of the original list.
        // 3. Add the elements from the beginning of the original list
        //    up to the starting index to the new list.
        // 4. Clear the original list.
        // 5. Add all the elements from the new list back into the
        //    original list so that the existing list is modified.

        int startIndex = data.Count - amount;

        List<int> rotated = data.GetRange(startIndex, amount);

        rotated.AddRange(data.GetRange(0, startIndex));

        data.Clear();
        data.AddRange(rotated);
    }
}
