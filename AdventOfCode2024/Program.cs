using System;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

public class Solutions
{
    static void Main(string[] args)
    {
        AdditionOfDifferenceInList();
    }

    static void AdditionOfDifferenceInList()
    {


        List<int> leftSideList = new List<int>();
        List<int> rightSideList = new List<int>();

        int i1;
        int i2;

        string filePath = "InputDay1.txt";
        //string filePath = "C:\\workspace\\dotNetProjects\\AdevntOfCode2024InputTexts\\Day1Part1.txt";

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] partsWithoutEmpty = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                i1 = Convert.ToInt32(partsWithoutEmpty[0]);
                i2 = Convert.ToInt32(partsWithoutEmpty[1]);
                leftSideList.Add(i1);
                rightSideList.Add(i2);
            }
        }
        leftSideList.Sort();
        rightSideList.Sort();
        int differenceOfValues = 0;
        int sumOfTheDifferences = 0;
        for (int i = 0; i < leftSideList.Count; i++)
        {
            differenceOfValues = rightSideList[i] - leftSideList[i];

            if (differenceOfValues < 0)
            {
                differenceOfValues = -differenceOfValues;
            }
            sumOfTheDifferences += differenceOfValues;
        }

        Console.WriteLine("sum of the difference is "+ sumOfTheDifferences);

        // similarity score

        int count = 0;
        int totalCount = 0;
        int similarityScore = 0;
        foreach (int num in leftSideList)
        {
            count = rightSideList.Count(n => n == num);
            similarityScore = num * count;
            totalCount += similarityScore;

        } 

        Console.WriteLine("sum of similarity score is " + totalCount);
   }


}
