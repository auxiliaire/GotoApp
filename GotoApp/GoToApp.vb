Imports System

Module GoToModule

    Enum CalculationMethod As Byte
        Recursion
        GoToLoop
    End Enum

    Structure CalculationResult
        Public value As Double
        Public elapsedTime As TimeSpan
    End Structure

    Delegate Function Fibonacci(num As Double) As Double

    Function FibRecursion(num As Double, Optional a As Double = 0, Optional b As Double = 1) As Double
        If num = 1 Then
            Return b
        End If
        Return FibRecursion(num - 1, b, a + b)
    End Function

    Function FibGoTo(num As Double, Optional a As Double = 0, Optional b As Double = 1) As Double
        Dim c As Double
chk:    If num = 1 Then
            Return b
        End If
        num -= 1
        c = b
        b = a + b
        a = c
        GoTo chk
    End Function

    Sub PrintHeader(repetitions As UInt16)
        Console.ForegroundColor = ConsoleColor.Cyan
        Console.WriteLine("Calculating the 1000th Fibonacci number " + repetitions.ToString() + "x with two different methods:")
        Console.ResetColor()
    End Sub

    Sub PrintVerdict(recurResult As CalculationResult, goToResult As CalculationResult)
        Dim speedFactor As Double
        Dim winner, looser As String

        If recurResult.elapsedTime > goToResult.elapsedTime Then
            speedFactor = Math.Round(recurResult.elapsedTime / goToResult.elapsedTime)
            winner = "GoTo loop"
            looser = "normal recursion"
        Else
            ' This case is unlikely
            speedFactor = Math.Round(goToResult.elapsedTime / recurResult.elapsedTime)
            winner = "Normal recursion"
            looser = "GoTo loop"
        End If
        Console.ForegroundColor = ConsoleColor.Yellow
        Console.WriteLine(winner + " is about " + speedFactor.ToString() + " times faster than " + looser)
        Console.ResetColor()
    End Sub

    Sub PrintResults(recurResult As CalculationResult, goToResult As CalculationResult)
        Console.WriteLine()
        Console.Write("Normal recursion result: ")
        Console.WriteLine(recurResult.value)
        Console.Write("GoTo loop result:        ")
        Console.WriteLine(goToResult.value)
        Console.WriteLine()
        Console.WriteLine("Normal recursion average time consumption: " + recurResult.elapsedTime.TotalMilliseconds.ToString() + " milliseconds")
        Console.WriteLine("GoTo loop average time consumption:        " + goToResult.elapsedTime.TotalMilliseconds.ToString() + " milliseconds")
        Console.WriteLine()
        PrintVerdict(recurResult, goToResult)
        Console.WriteLine()
        Console.WriteLine("Press Enter to exit program")
        Console.ReadLine()
    End Sub

    Function GetCalculationFn(calculationMethod As CalculationMethod) As Fibonacci
        Dim fib As Fibonacci
        If calculationMethod = CalculationMethod.Recursion Then
            fib = Function(num As Double)
                      Return FibRecursion(num)
                  End Function
        Else
            fib = Function(num As Double)
                      Return FibGoTo(num)
                  End Function
        End If
        Return fib
    End Function

    Function LoopCalculate(repetitions As UInt16, calculationMethod As CalculationMethod)
        Dim calculationResult As CalculationResult
        Dim startTime As DateTime
        Dim endTime As DateTime
        Dim fib As Fibonacci = GetCalculationFn(calculationMethod)

        startTime = DateTime.Now
        For i = 1 To repetitions
            calculationResult.value = fib(1000)
        Next
        endTime = DateTime.Now

        ' Calculating average time consumption:
        calculationResult.elapsedTime = endTime.Subtract(startTime) / repetitions

        Return calculationResult
    End Function

    Sub Main()
        Dim fib As Double
        Dim repetitions As UInt16 = 1000
        Dim recurResult, goToResult As CalculationResult

        PrintHeader(repetitions)

        ' Warmup (the first run of whichever takes longer for some reasons):
        fib = FibRecursion(1000)

        Console.WriteLine("- Normal recursion")
        recurResult = LoopCalculate(repetitions, CalculationMethod.Recursion)

        Console.WriteLine("- GoTo loop")
        goToResult = LoopCalculate(repetitions, CalculationMethod.GoToLoop)

        PrintResults(recurResult, goToResult)
    End Sub
End Module
