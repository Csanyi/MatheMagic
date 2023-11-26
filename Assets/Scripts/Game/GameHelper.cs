using System;

public enum Grade
{
    FIRST,
    SECOND,
    THIRD,
    FOURTH
}

public enum Operation
{
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION
}

public class Exercise
{
    int operand1,
        operand2,
        result;
    Operation operationType;

    public Exercise(int operand1, int operand2, int result, Operation operationType)
    {
        this.operand1 = operand1;
        this.operand2 = operand2;
        this.result = result;
        this.operationType = operationType;
    }

    public int GetOperand1() { return this.operand1; }
    public int GetOperand2() { return this.operand2; }
    public Operation GetOperationType() { return this.operationType; }
    public int GetResult() { return this.result; }

    String GetOperationSign()
    {
        switch (this.operationType)
        {
            case Operation.ADDITION:
                return "+";
            case Operation.SUBTRACTION:
                return "-";
            case Operation.MULTIPLICATION:
                return "×";
            case Operation.DIVISION:
                return "÷";
            default:
                return "";
        }
    }

    public String PrintExercise()
    {
        return this.operand1 + " " + GetOperationSign() + " " + this.operand2 + " = " + this.result;
    }

    public String PrintExerciseWithoutResult()
    {
        return this.operand1 + " " + GetOperationSign() + " " + this.operand2 + " = ?";
    }
}

public class GameHelper
{
    static Random random = new Random();

    public static int GenerateRandomNumber(int n, int m)
    {
        return random.Next(n, m + 1);
    }

    public static Exercise GenerateRandomExercise(Operation operation, int digits)
    {
        // TODO
        return new Exercise(1, 2, 3, operation);
    }

    public static Exercise GenerateRandomExercise(Grade grade)
    {
        // TODO
        return new Exercise(1, 2, 3, Operation.ADDITION);
    }

    public static Exercise GenerateRandomExercise(Grade grade, int digits)
    {
        // TODO
        return new Exercise(1, 2, 3, Operation.ADDITION);
    }
}
