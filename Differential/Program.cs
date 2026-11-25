using System;

class NewtonRaphson
{
    // The function f(x) - change it to match your problem
    static double F(double x) => x * x * x - 2 * x - 5;

    // First derivative f'(x)
    static double F1(double x) => 3 * x * x - 2;

    // Second derivative f''(x)
    static double F2(double x) => 6 * x;

    static void Main()
    {
        // Prompt the user to enter the initial guess x0
        Console.WriteLine("Enter the initial value x0:");

        // Read x0 safely; the value is stored in x (the current xi)
        if (!double.TryParse(Console.ReadLine(), out double x))
        {
            Console.WriteLine("Invalid value.");
            return;
        }

        double tolerance = 1e-6;   // Required accuracy
        int maxIterations = 100;   // Safety limit to avoid infinite loops

        Console.WriteLine($"\nx0 = {x:F6}\n");
        Console.WriteLine("i\t x(i)\t\t f(x(i))\t\t x(i+1)\t\t |conv|");

        for (int i = 0; i < maxIterations; i++)
        {
            double fx = F(x);     // f(xi)
            double dfx = F1(x);   // f'(xi)

            // The method fails if the derivative is zero (division by zero)
            if (dfx == 0)
            {
                Console.WriteLine("The derivative is zero, the method will not work.");
                return;
            }

            // Convergence condition: |f(x) * f''(x) / f'(x)^2| < 1
            double conv = Math.Abs(fx * F2(x) / (dfx * dfx));

            // Main formula: x(i+1) = x(i) - f(x(i)) / f'(x(i))
            double xNext = x - fx / dfx;

            Console.WriteLine($"{i}\t {x:F6}\t {fx:E3}\t {xNext:F6}\t {conv:F4}"
                              + (conv >= 1 ? "  <-- convergence condition not satisfied" : ""));

            // Stop when the change between two iterations is small enough
            if (Math.Abs(xNext - x) < tolerance)
            {
                Console.WriteLine($"\nApproximate solution: x = {xNext:F6}");
                Console.WriteLine($"Number of iterations: {i + 1}");
                return;
            }

            // Move to the next step: x(i+1) becomes the new x(i)
            x = xNext;
        }

        Console.WriteLine("Reached the maximum number of iterations without reaching the required accuracy.");
    }
}