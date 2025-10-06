using System;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

public class Solutions
{
    static void Main(string[] args)
    {
        //AdditionOfDifferenceInList();
        NumberOfSafeReports();
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

        List<int[]> allUnsafeReports = new List<int[]>();
        // day 2 part 1 and part 3
        int safeReportPart1 = 0;
        int unsafeReport = 0;
        int safeReportPart2 = 0;

        string filePath = "InputDay2Sample.txt";

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
                for (int i = 0; i < (report.Length - 1); i++)
                {

                    if (isTheListEqual)
                    {
                        //Console.WriteLine(report[i] + " " + report[i + 1]);
                        Console.WriteLine("equal values so unsafe report");
                        Array.ForEach(report, Console.WriteLine);
                        unsafeReport++;
                        if(unsafeReport == 1)
                        {
                            
                            int indexToRemove = i;
                            int[] newReport = report.Where((element, index) => index != indexToRemove).ToArray();

                            allUnsafeReports.Add(newReport);
                        }

                        //return; //why does it exit both loops?

                    }
                    else if (isTheListAscending)
                    {
                        if (!((report[i] < report[i + 1]) && ((report[i + 1] - report[i]) <= 3)))

                        {
                            //Console.WriteLine(report[i] + " " + report[i + 1]);
                            Console.WriteLine("ascending unsafe report");
                            Array.ForEach(report, Console.WriteLine);
                            unsafeReport++;

                            if (unsafeReport == 1)
                            {

                                int indexToRemove = i;
                                int[] newReport = report.Where((element, index) => index != indexToRemove).ToArray();
                                allUnsafeReports.Add(newReport);
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
                            //Console.WriteLine(report[i] + " " + report[i + 1]);
                            Console.WriteLine("descending unsafe report");
                            Array.ForEach(report, Console.WriteLine);
                            unsafeReport++;
                            if (unsafeReport == 1)
                            {

                                int indexToRemove = i;
                                int[] newReport = report.Where((element, index) => index != indexToRemove).ToArray();
                                allUnsafeReports.Add(newReport);
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
                    Console.WriteLine("safe report");
                    safeReportPart1++;

                }
            }
        }
        Console.WriteLine(" safe report part 1 " + safeReportPart1);

        Console.WriteLine(" all unsafeReports length " + allUnsafeReports.Count);
        int extraSafeReports = 0;
        if (allUnsafeReports.Count > 0)
        {
            extraSafeReports = RecheckUnSafeReports(allUnsafeReports);              
        }
        Console.WriteLine(" all safeReports " + (safeReportPart1 + extraSafeReports));

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
                    //Console.WriteLine(report[i] + " " + report[i + 1]);
                    Console.WriteLine("equal values so unsafe report");
                    Array.ForEach(report, Console.WriteLine);
                    unsafeReport++;

                    //return; //why does it exit both loops?

                }
                else if (isTheListAscending)
                {
                    if (!((report[i] < report[i + 1]) && ((report[i + 1] - report[i]) <= 3)))

                    {
                        //Console.WriteLine(report[i] + " " + report[i + 1]);
                        Console.WriteLine("ascending unsafe report");
                        Array.ForEach(report, Console.WriteLine);
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
                        //Console.WriteLine(report[i] + " " + report[i + 1]);
                        Console.WriteLine("descending unsafe report");
                        Array.ForEach(report, Console.WriteLine);
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
                Console.WriteLine("safe report");
                safeReport++;

            }

        }
        Console.WriteLine("the extra number of safe reports : " + safeReport);
        return safeReport;

    }


}


