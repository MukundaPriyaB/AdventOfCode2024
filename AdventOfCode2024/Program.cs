using System;
using System.Collections;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class Solutions
{
    static void Main(string[] args)
    {
        //AdditionOfDifferenceInList();
        //NumberOfSafeReports();
        AdditionOfMultiplicationOutput();
    }


    //day 1 puzzle code
    static void AdditionOfDifferenceInList()
    {
        //day 1 part 1

        List<int> leftSideList = new List<int>();
        List<int> rightSideList = new List<int>();

        int i1;
        int i2;

        string filePath = "InputDay1.txt";

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

        Console.WriteLine("sum of the difference is " + sumOfTheDifferences);

        // day 1 part 2
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

    //day2 puzzle code
    static void NumberOfSafeReports()
    {

        // day 2 part 1 and part 2
        int safeReport = 0;
        int unsafeReport = 0;

        string filePath = "InputDay2.txt";

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] partsWithoutEmpty = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int[] report = partsWithoutEmpty.Select(int.Parse).ToArray(); // doesnt allow convert.toInt , why?
                bool isTheListAscending = false;
                bool isTheListDescending = false;
                bool isTheListEqual = false;
                if (report[0] < report[1])
                {
                    isTheListAscending = true;
                }
                else if (report[0] > report[1])
                {
                    isTheListDescending = true;
                }
                else
                {
                    isTheListEqual = true;
                }
                int conditionMet = 0;
                unsafeReport = 0;
                List<int[]> allUnsafeReportsSmall = new List<int[]>();
                int safeReportExtra = 0;
                for (int i = 0; i < (report.Length - 1); i++)
                {
                    

                    if (isTheListEqual)
                    {
                        unsafeReport++;
                        if(unsafeReport == 1)
                        {
                            for (int j = 0; j< (report.Length); j++)
                            {
                                int[] newReport3 = report.Where((element, index) => index != j).ToArray();
                                allUnsafeReportsSmall.Add(newReport3);
                            }
                            safeReportExtra = RecheckUnSafeReports(allUnsafeReportsSmall);
                        }

                        //return; //why does it exit both loops?

                    }
                    else if (isTheListAscending)
                    {
                        if (!((report[i] < report[i + 1]) && ((report[i + 1] - report[i]) <= 3)))

                        {
                            unsafeReport++;

                            if (unsafeReport == 1)
                            {
                                for (int j = 0; j < (report.Length); j++)
                                {
                                    int[] newReport3 = report.Where((element, index) => index != j).ToArray();
                                    allUnsafeReportsSmall.Add(newReport3);
                                }
                                safeReportExtra = RecheckUnSafeReports(allUnsafeReportsSmall);
                            }
                        }
                        else
                        {
                            conditionMet++;
                        }

                    }
                    else if (isTheListDescending)
                    {
                        if (!((report[i] > report[i + 1]) && ((report[i] - report[i + 1]) <= 3)))
                        {
                            unsafeReport++;
                            if (unsafeReport == 1)
                            {

                                int indexToRemove = i;
                                for (int j = 0; j < (report.Length); j++)
                                {
                                    int[] newReport3 = report.Where((element, index) => index != j).ToArray();
                                    allUnsafeReportsSmall.Add(newReport3);
                                }
                                int[] newReport = report.Where((element, index) => index != indexToRemove).ToArray();
                                safeReportExtra = RecheckUnSafeReports(allUnsafeReportsSmall);
                            }

                        }
                        else
                        {
                            conditionMet++;
                        }
                    }

                }

                if (conditionMet == (report.Length - 1))
                {
                    safeReport++;

                }
                if (safeReportExtra >= 1)
                {
                    safeReport ++;
                }
                
            }
        }
        Console.WriteLine(" safe reports " + safeReport);


    }

    static int RecheckUnSafeReports(List <int[]> allUnsafeReports)
    {
        int safeReport = 0;
        foreach (int[] report in allUnsafeReports)
        {
            bool isTheListAscending = false;
            bool isTheListDescending = false;
            bool isTheListEqual = false;
            if (report[0] < report[1])
            {
                isTheListAscending = true;
            }
            else if (report[0] > report[1])
            {
                isTheListDescending = true;
            }
            else
            {
                isTheListEqual = true;
            }
            int conditionMet = 0;
            int unsafeReport = 0;
            for (int i = 0; i < (report.Length - 1); i++)
            {

                if (isTheListEqual)
                {
                    unsafeReport++;

                    //return; //why does it exit both loops?

                }
                else if (isTheListAscending)
                {
                    if (!((report[i] < report[i + 1]) && ((report[i + 1] - report[i]) <= 3)))

                    {
                        unsafeReport++;

                    }
                    else
                    {
                        conditionMet++;
                    }

                }
                else if (isTheListDescending)
                {
                    if (!((report[i] > report[i + 1]) && ((report[i] - report[i + 1]) <= 3)))
                    {
                        unsafeReport++;

                    }
                    else
                    {
                        conditionMet++;
                    }
                }

            }

            if (conditionMet == (report.Length - 1))
            {
                safeReport++;

            }

        }
        return safeReport;

    }

    public static void AdditionOfMultiplicationOutput()
    {
        string filePath = "InputDay3.txt";
        string fileContent = File.ReadAllText(filePath);
        //Console.WriteLine($"Original string: {fileContent}");

        string pattern = @"mul\(\d+,\d+\)"; // Matches one or more digits  

        // Extract only the "mul()" occurrences
        string extractedMul = string.Join("", Regex.Matches(fileContent, pattern));
        //Console.WriteLine($"Extracted mul(): {extractedMul}");
        //mul(2,4)mul(5,5)
        MultiplyTwoValuesFromSTring(extractedMul);

        //PART2
        //do and dont 

        string input2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        
        string pattern2 = @"(do\(\)|don't\(\)|mul\(\d+,\d+\))";

        string extractedMulAndDos = string.Join("", Regex.Matches(fileContent, pattern2));
        //Console.WriteLine($"Extracted mul() with dos and dont: {extractedMulAndDos}");

        string originalText = extractedMulAndDos;
        string numPattern2 = @"don't\(\)(.*?)do\(\)";
        string replacement = "";
        string newText = Regex.Replace(originalText, numPattern2, replacement, RegexOptions.IgnoreCase);

        //Console.WriteLine("==========================");

        //Console.WriteLine(newText);

        originalText = newText;
        numPattern2 = @"do\(\)";
        replacement = "";
        newText = Regex.Replace(originalText, numPattern2,replacement, RegexOptions.IgnoreCase);

        //Console.WriteLine("===========================================");
        //Console.WriteLine(newText);
        //Console.WriteLine("===========================================");
        newText += "end";

        originalText = newText;
        numPattern2 = @"don't\(\)(.*?)end";
        replacement = "";
        newText = Regex.Replace(originalText, numPattern2, replacement, RegexOptions.IgnoreCase);
        //Console.WriteLine("===========================================");
        //Console.WriteLine(newText);
        //Console.WriteLine("===========================================");
        MultiplyTwoValuesFromSTring(newText);


    }

    public static double MultiplyTwoValuesFromSTring(string extractedMul)
    {

        double sum = 0;
        double multipliedValue = 0;

        string numPattern = @"\d+";
        List<double> numbers = new List<double>();
        List<double> numbers2 = new List<double>();
        bool odd = true;

        MatchCollection matches = Regex.Matches(extractedMul, @"\d+");

        foreach (Match match in matches)
        {
            if (double.TryParse(match.Value, out double num) && odd)
            {

                numbers.Add(num);
                odd = false;
            }
            else if (double.TryParse(match.Value, out double num2) && !odd)
            {
                numbers2.Add(num2);
                odd = true;
            }

        }

        for (int i = 0; i < numbers.Count; i++)
        {
            for (int j = 0; j < numbers2.Count; j++)
            {
                if (i == j)
                {
                    multipliedValue = numbers[i] * numbers2[j];
                    sum = sum + multipliedValue;
                }
            }
        }

        Console.WriteLine(sum);
        return sum;
    }



}

