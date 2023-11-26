using System;

public class Lock
{
    private Exercise exercise;
    private int[] lockDigits;

    static Random random = new Random();

    public Lock(int digits)
    {
        this.exercise = GameHelper.GenerateRandomExercise((random.Next(2) == 0) ? Operation.ADDITION : Operation.SUBTRACTION, digits);
        this.lockDigits = new int[digits];
    }

    public Exercise GetExercise()
    {
        return this.exercise;
    }

    public int[] GetLockDigits()
    {
        return this.lockDigits;
    }

    public void rotateDigit(int index, bool up)
    {
        if (up)
        {
            lockDigits[index]++;
        }
        else
        {
            lockDigits[index]--;
        }
        lockDigits[index] %= 10;
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