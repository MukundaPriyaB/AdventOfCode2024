using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

public class Solutions
{
    static void Main(string[] args)
    {
        //AdditionOfDifferenceInList();
        //NumberOfSafeReports();
        //AdditionOfMultiplicationOutput();
        //GridPatternMatching();
        ElfRecordOrdering();

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
                        if (unsafeReport == 1)
                        {
                            for (int j = 0; j < (report.Length); j++)
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
                    safeReport++;
                }

            }
        }
        Console.WriteLine(" safe reports " + safeReport);


    }

    static int RecheckUnSafeReports(List<int[]> allUnsafeReports)
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
        newText = Regex.Replace(originalText, numPattern2, replacement, RegexOptions.IgnoreCase);

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

    //day 4
    public static void GridPatternMatching()
    {
        //string[,] grid1 = {

        //{ "M","M","M","S","X","X","M","A","S","M" },
        //{ "M","S","A","M","X","M","S","M","S","A" },
        //{ "A","M","X","S","X","M","A","A","M","M"},
        //{ "M","S","A","M","A","S","M","S","M","X"},
        //{ "X","M","A","S","A","M","X","A","M","M"},
        //{ "X","X","A","M","M","X","X","A","M","A"},
        //{ "S","M","S","M","S","A","S","X","S","S"},
        //{ "S","A","X","A","M","A","S","A","A","A"},
        //{ "M","A","M","M","M","X","M","M","M","M"},
        //{ "M","X","M","X","A","X","M","A","S","X"}

        //};

        string filePath = "InputDay4.txt";
        string[] lines = File.ReadAllLines(filePath);


        string[] firstLineParts = lines[0].Select(c => c.ToString()).ToArray();
        int numRows = lines.Length;
        int numCols = firstLineParts.Length;

        string[,] grid1 = new string[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            string[] parts = lines[i].Select(c => c.ToString()).ToArray();
            for (int j = 0; j < numCols; j++)
            {
                grid1[i, j] = parts[j];
            }
        }

        /*
         HORIZONTAL:

        XMAS : (j-1,j-2,j+1 all !null]
        i,j ==a
        i, j-1 ==m
        i,j-2 ==x
        i,j+1 ==s

        SAMX [j-1,j+1,j+2 all !null)
        i, j ==a
        i,j-1 == s
        i,j+1 ==m
        i,j+2 ==x  
        

        VERICAL:
            X  (i-1, i-2, i+1 not null)
            M
            A
            S

            i,j == a
            i-1,j == m
            i-2, j ==x
            i+1, j ==s

            S  (i-1, i+1,i+2 not null)
            A
            M
            X

            i, j ==a
            i-1, j==s
            i+1, j ==m
            i+2, j ==x


            DIAGNAL:

            X...	(i-1, j-1, i-2, j-2, i+1, j+1)
            .M..
            ..A.
            ...S

            i,j ==a
            i-1,j-1 ==m
            i-2,j-2 ==x
            i+1,j+1== s


            ...S	(i-1, j+1, i+1, j-1, i+2, j-2)
            ..A.
            .M..
            X...

            i,j==a
            i-1, j+1 ==s
            i+1 j-1 ==m
            i+2, j-2 ==x


            S... ( i+1, i+2, i-1, j+1, j+2, j-1)
            .A..
            ..M
            ...X

            i, j = a 
            i+1, j+1 = m
            i+2, j+2 = x
            i-1, j-1 = s

            ...X (i+1,i-1,i-2,j -1,j+1,j+2 )
            ..M.
            .A..
            S...


            i, j = a 
            i+1, j -1 == s
            i-1, j+1 = m
            i-2, j+2 = x
            */
        int countH = 0;
        int countV = 0;
        int countD = 0;

        int inputRows = grid1.GetLength(0);

        int inputColumns = grid1.GetLength(1);

        for (int i = 0; i < inputRows; i++)
        {
            for (int j = 0; j < inputColumns; j++)
            {

                if ((j - 1) >= 0 && (j - 2) >= 0 && (j + 1) < inputColumns)
                {
                    if (grid1[i, j] == "A" && grid1[i, j - 1] == "M" && grid1[i, j - 2] == "X" && grid1[i, j + 1] == "S")
                    {
                        countH++;
                    }
                }
                if ((j - 1) >= 0 && (j + 1) < inputColumns && (j + 2) < inputColumns)
                {
                    if (grid1[i, j] == "A" && grid1[i, j - 1] == "S" && grid1[i, j + 1] == "M" && grid1[i, j + 2] == "X")
                    {
                        countH++;
                    }
                }

                if ((i - 1) >= 0 && (i - 2) >= 0 && (i + 1) < inputRows)
                {
                    if (grid1[i, j] == "A" && grid1[i - 1, j] == "M" && grid1[i - 2, j] == "X" && grid1[i + 1, j] == "S")
                    {
                        countV++;
                    }
                }
                if ((i - 1) >= 0 && (i + 1) < inputRows && (i + 2) < inputRows)
                {
                    if (grid1[i, j] == "A" && grid1[i - 1, j] == "S" && grid1[i + 1, j] == "M" && grid1[i + 2, j] == "X")
                    {
                        countV++;
                    }
                }

                if ((i - 1) >= 0 && (i - 2) >= 0 && (i + 1) < inputRows && (j - 1) >= 0 && (j - 2) >= 0 && (j + 1) < inputColumns)
                {

                    if (grid1[i, j] == "A" && grid1[i - 1, j - 1] == "M" && grid1[i - 2, j - 2] == "X" && grid1[i + 1, j + 1] == "S")
                    {
                        countD++;
                    }


                }
                if ((i - 1) >= 0 && (i + 1) < inputRows && (i + 2) < inputRows && (j - 1) >= 0 && (j + 1) < inputColumns && (j - 2) >= 0)
                {
                    if (grid1[i, j] == "A" && grid1[i - 1, j + 1] == "S" && grid1[i + 1, j - 1] == "M" && grid1[i + 2, j - 2] == "X")
                    {
                        countD++;
                    }
                }
                if ((i - 1) >= 0 && (i + 1) < inputRows && (i + 2) < inputRows && (j - 1) >= 0 && (j + 1) < inputColumns && (j + 2) < inputColumns)
                {
                    if (grid1[i, j] == "A" && grid1[i + 1, j + 1] == "M" && grid1[i + 2, j + 2] == "X" && grid1[i - 1, j - 1] == "S")
                    {
                        countD++;
                    }
                }
                if ((i - 1) >= 0 && (i + 1) < inputRows && (i - 2) >= 0 && (j - 1) >= 0 && (j + 1) < inputColumns && (j + 2) < inputColumns)
                {
                    if (grid1[i, j] == "A" && grid1[i + 1, j - 1] == "S" && grid1[i - 1, j + 1] == "M" && grid1[i - 2, j + 2] == "X")
                    {
                        countD++;
                    }
                }

            }
        }
        Console.WriteLine($"Horizontal: {countH}, Vertical: {countV}, Diagnol: {countD}");
        Console.WriteLine(countH + countD + countV);

        //part 2
        int countMAS = 0;

        for (int i = 0; i < inputRows; i++)
        {
            for (int j = 0; j < inputColumns; j++)
            {
                if ((i - 1) >= 0 && (j - 1) >= 0 && (j + 1) < inputColumns && (i + 1) < inputRows)
                {
                    /*
                    M.M
                    .A.
                    S.S

                    i,j = a
                    i-1, j-1 = m
                    i-1, j+1 =m
                    i+1, j-1 = s
                    i+1, j+1 = s
                     */

                    if (grid1[i, j] == "A" && grid1[i - 1, j - 1] == "M" && grid1[i - 1, j + 1] == "M" && grid1[i + 1, j - 1] == "S" && grid1[i + 1, j + 1] == "S")
                    {
                        countMAS++;
                    }
                    /*
                    S.S
                    .A.
                    M.M

                    i, j =a
                    i-1, j-1 = s
                    i-1, j+1 =s
                    i+1, j-1 = m
                    i+1, j+1 = m
                     */
                    if (grid1[i, j] == "A" && grid1[i - 1, j - 1] == "S" && grid1[i - 1, j + 1] == "S" && grid1[i + 1, j - 1] == "M" && grid1[i + 1, j + 1] == "M")
                    {
                        countMAS++;
                    }

                    /*
                    M.S
                    .A.
                    M.S

                    i, j =a
                    i-1, j-1 = m
                    i-1, j+1 =s
                    i+1, j-1 = m
                    i+1, j+1 = s
                    
                     */
                    if (grid1[i, j] == "A" && grid1[i - 1, j - 1] == "M" && grid1[i - 1, j + 1] == "S" && grid1[i + 1, j - 1] == "M" && grid1[i + 1, j + 1] == "S")
                    {
                        countMAS++;
                    }

                    /*
                    S.M
                    .A.
                    S.M

                    i, j =a
                    i-1, j-1 = s
                    i-1, j+1 =m
                    i+1, j-1 = s
                    i+1, j+1 = m
                    */
                    if (grid1[i, j] == "A" && grid1[i - 1, j - 1] == "S" && grid1[i - 1, j + 1] == "M" && grid1[i + 1, j - 1] == "S" && grid1[i + 1, j + 1] == "M")
                    {
                        countMAS++;
                    }
                }
            }
        }

        Console.WriteLine("part 2 MAS number is :" + countMAS);



    }


    public static void ElfRecordOrdering()
    {
        string filePath = "InputDay5.txt"; // change it in 2 places
        string fileContent = File.ReadAllText(filePath);

        String[] rulesAndInputs = fileContent.Split("\r\n\r\n");

        String rules = rulesAndInputs[0];
        String[] records = rulesAndInputs[1].Split("\r\n");

        bool isSorted = true;

        double sum = 0;

        double middlePage = 0;

        List<String> wrongOrderedRecords = new List<string>();

        foreach (String record in records)
        {
            String[] inputLine = record.Split(",");
            String[] original = inputLine;
            for (int i = 0; i < original.Length - 1; i++)
            {
                if (new ElfRulesComparer().Compare(original[i], original[i + 1]) > 0)
                {
                    isSorted = false;
                    break;
                }
            }
            if (isSorted)
            {
                middlePage = Double.Parse(original[(original.Length - 1) / 2]); // because index values start from 0; 3rd element index is 2

                //Console.WriteLine("Middle Page is " + middlePage);
                sum += middlePage;
            }
            else
            {
                wrongOrderedRecords.Add(record);
            }
            isSorted = true;

        }

        Console.WriteLine("sum of correctly ordered lists :" + sum);

        double correctedSum = 0;
        foreach (String record in wrongOrderedRecords)
        {
            String[] inputLine = record.Split(",");
            String[] original = inputLine;
            Array.Sort(original, new ElfRulesComparer());
            middlePage = Double.Parse(original[(original.Length - 1) / 2]); // because index values start from 0; 3rd element index is 2
            //Console.WriteLine("Middle Page Corrected is " + middlePage);
            correctedSum += middlePage;

        }
        Console.WriteLine("sum of wrongly ordered lists after correction :" + correctedSum);


    }
}

public class ElfRulesComparer : IComparer<String>
{
    public int Compare(String x, String y)
    {
        string filePath = "InputDay5.txt";
        string fileContent = File.ReadAllText(filePath);

        String[] rulesAndInputs = fileContent.Split("\r\n\r\n");

        String rules = rulesAndInputs[0];

        String[] rulesArray = rules.Split("\r\n");
        foreach (String rule in rulesArray)
        {
            String[] nums = rule.Split("|");
            String a = nums[0];
            String b = nums[1];
            if (x == a && y == b) return -1;
        }

        return 1;
    }
}
