using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string filePath = "numbers.txt";
        List<int> numbers = new List<int>();

        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                if (int.TryParse(line, out int number))
                {
                    numbers.Add(number);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return;
        }

        if (numbers.Count == 0)
        {
            Console.WriteLine("No valid numbers found in the file.");
            return;
        }

        int max = numbers.Max();
        int min = numbers.Min();
        double mean = numbers.Average();
        double median;
        numbers.Sort();

        if (numbers.Count % 2 == 0)
        {
            median = (numbers[numbers.Count / 2 - 1] + numbers[numbers.Count / 2]) / 2.0;
        }
        else
        {
            median = numbers[numbers.Count / 2];
        }

        List<int> longestIncreasingSequence = GetLongestSequence(numbers, true);
        List<int> longestDecreasingSequence = GetLongestSequence(numbers, false);

        Console.WriteLine($"Максимальне число: {max}");
        Console.WriteLine($"Мінімальне число: {min}");
        Console.WriteLine($"Медіана: {median}");
        Console.WriteLine($"Середнє арифметичне: {mean}");
        Console.WriteLine($"Найбільша зростаюча послідовність: {string.Join(", ", longestIncreasingSequence)}");
        Console.WriteLine($"Найбільша спадна послідовність: {string.Join(", ", longestDecreasingSequence)}");
    }

    static List<int> GetLongestSequence(List<int> numbers, bool isIncreasing)
    {
        List<int> longestSequence = new List<int>();
        List<int> currentSequence = new List<int> { numbers[0] };

        for (int i = 1; i < numbers.Count; i++)
        {
            if (isIncreasing ? numbers[i] > numbers[i - 1] : numbers[i] < numbers[i - 1])
            {
                currentSequence.Add(numbers[i]);
            }
            else
            {
                if (currentSequence.Count > longestSequence.Count)
                {
                    longestSequence = new List<int>(currentSequence);
                }
                currentSequence = new List<int> { numbers[i] };
            }
        }

        if (currentSequence.Count > longestSequence.Count)
        {
            longestSequence = currentSequence;
        }

        return longestSequence;
    }
}
