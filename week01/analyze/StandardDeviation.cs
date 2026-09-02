/// <summary>
/// These 3 functions will (in different ways) calculate the standard
/// deviation from a list of numbers.  The standard deviation
/// is defined as the square root of the variance.  The variance is 
/// defined as the average of the squared differences from the mean.
/// </summary>
public static class StandardDeviation {
    public static void Run() {
        var numbers = new[] { 600, 470, 170, 430, 300 };
        Console.WriteLine(StandardDeviation1(numbers)); // Should be 147.322 
        Console.WriteLine(StandardDeviation2(numbers)); // Should be 147.322 
        Console.WriteLine(StandardDeviation3(numbers)); // Should be 147.322 
    }

    private static double StandardDeviation1(int[] numbers) { // 5n  + 6 => O(n)
        var total = 0.0;// 1
        var count = 0;// 1
        foreach (var number in numbers) { // n
            total += number; // n
            count += 1; // n
        }

        var avg = total / count; // 1
        var sumSquaredDifferences = 0.0;//1
        foreach (var number in numbers) { // n
            sumSquaredDifferences += Math.Pow(number - avg, 2); //n
        }

        var variance = sumSquaredDifferences / count; // 1
        return Math.Sqrt(variance);//1
    }

    private static double StandardDeviation2(int[] numbers) {// 3n2 + 3n + 7 => n2
        var sumSquaredDifferences = 0.0;// 1
        var countNumbers = 0;// 1
        foreach (var number in numbers) { //n
            var total = 0;//n
            var count = 0;//n
            foreach (var value in numbers) {//n2
                total += value;//n2
                count += 1;//n2
            }

            var avg = total / count;//1
            sumSquaredDifferences += Math.Pow(number - avg, 2);//1
            countNumbers += 1;//1
        }

        var variance = sumSquaredDifferences / countNumbers;//1
        return Math.Sqrt(variance);//1
    }

    private static double StandardDeviation3(int[] numbers) { //2n+5 => n
        var count = numbers.Length;//1
        var avg = (double)numbers.Sum() / count;//1
        var sumSquaredDifferences = 0.0;//1
        foreach (var number in numbers) {//n
            sumSquaredDifferences += Math.Pow(number - avg, 2);//n
        }

        var variance = sumSquaredDifferences / count;//1
        return Math.Sqrt(variance);//1
    }
}