# GoToApp

This is a small demonstration about the effectiveness of using GoTo instead of function calls.
When performance is crucial, GoTo can be a means of optimization.
It also reminds us that calling functions is not free.
It needs to save the stack pointer, create a new stack frame, initialize local variables if any.
Similarly, the return of a function needs to store the return value, remove the stack frame, and
restore the stack pointer. Although all this is hidden from us, and happens in the background,
it's still happening, while GoTo is just one single operation in machine code - hence the
performance gain.

The app calculates the 1000th Fibonacci number a given number of times both using function calls
and GoTo, measures the time consumption, and compares the result.

Although the overall result may depend on other factors as well, usually code with GoTo performs
considerably better. This can mean 3-13 times the performance of the code written using function
calls.

## Code

This program is written in Microsoft Visual Basic for .NET framework 6.0. Another version is
available written in vanilla C [here](https://github.com/auxiliaire/goto-c).

## Screenshots

![Output of the application](/GotoApp/Screenshots/goto-basic.jpg "GoTo performance comparison 1")

![The application window](/GotoApp/Screenshots/goto-basic-window.jpg "GoTo performance comparison 2")
