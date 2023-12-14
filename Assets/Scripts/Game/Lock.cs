using System;

public class Lock
{
    private Exercise exercise;
    private int[] lockDigits;

    public Lock(int digits)
    {
        this.exercise = GameHelper.GenerateRandomExerciseForOperationAndDigits(
            GameHelper.GenerateRandomBool() ? Operation.ADDITION : Operation.SUBTRACTION,
            digits);
        this.lockDigits = new int[digits];
    }

    public Exercise GetExercise()
    {
        return this.exercise;
    }

    public int GetLockDigit(int index)
    {
        return this.lockDigits[index];
    }

    public void RotateDigit(int index, bool up)
    {
        if (up)
        {
            ++this.lockDigits[index];
        }
        else
        {
            --this.lockDigits[index];
            this.lockDigits[index] += 10;
        }
        this.lockDigits[index] %= 10;
    }

    public bool IsDigitCorrect(int index)
    {
        int localValue = (int)Math.Pow(10, this.lockDigits.Length - index);
        return this.lockDigits[index] == (this.exercise.GetResult() / localValue) % 10;
    }

    private int GetCurrentResult()
    {
        int result = this.lockDigits[0];
        for (int i = 1; i < this.lockDigits.Length; i++)
        {
            result *= 10;
            result += this.lockDigits[i];
        }
        return result;
    }

    public bool IsFinished()
    {
        return this.GetCurrentResult() == this.exercise.GetResult();
    }
}