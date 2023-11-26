using System;
using System.Collections.Generic;

class Container
{
    private int
        currentContent,
        optimalContent;

    public Container(int optimalContent)
    {
        this.currentContent = 0;
        this.optimalContent = optimalContent;
    }

    public Container(int optimalContent, int currentContent)
    {
        this.currentContent = currentContent;
        this.optimalContent = optimalContent;
    }

    public int GetCurrentContent()
    {
        return this.currentContent;
    }

    public void SetCurrentContent(int value)
    {
        this.currentContent = value;
    }

    public int GetOptimalContent()
    {
        return this.optimalContent;
    }

    public bool ReachedOptimalContent()
    {
        return this.currentContent == this.optimalContent;
    }
}

public class Box
{
    private List<Container> containers = new List<Container>();
    private Container defaultContainer;

    private bool labeledWithExercises;
    private List<Exercise> containerLabels = new List<Exercise>();

    static Random random = new Random();

    public Box(bool labeledWithExercises, Grade grade, int numberOfContainers)
    {
        this.labeledWithExercises = labeledWithExercises;
        if (this.labeledWithExercises)
        {
            int allElements = 0;
            for (int i = 0; i < numberOfContainers; i++)
            {
                Exercise exercise = GameHelper.GenerateRandomExercise(grade, 1);
                allElements += exercise.GetResult();
                this.containers.Add(new Container(exercise.GetResult()));
                this.containerLabels.Add(exercise);
            }
            this.defaultContainer = new Container(0, allElements);
        }
        else
        {
            int numberOfItemsPerContainer = random.Next(1, 10);
            for (int i = 0; i < numberOfContainers; i++)
            {
                this.containers.Add(new Container(numberOfItemsPerContainer));
            }
            this.defaultContainer = new Container(0, numberOfContainers * numberOfItemsPerContainer);
        }
    }

    public Exercise GetContainerLabel(int index)
    {
        if (this.labeledWithExercises)
        {
            return this.containerLabels[index];
        }
        else
        {
            return null;
        }
    }

    public bool GetContainerReachedOptimalContent(int index)
    {
        if (index == -1)
        {
            return this.defaultContainer.ReachedOptimalContent();
        }
        else
        {
            return this.containers[index].ReachedOptimalContent();
        }
    }

    public int GetContainerOptimalContent(int index)
    {
        if (index == -1)
        {
            return this.defaultContainer.GetOptimalContent();
        }
        else
        {
            return this.containers[index].GetOptimalContent();
        }
    }

    public int GetContainerCurrentContent(int index)
    {
        if (index == -1)
        {
            return this.defaultContainer.GetCurrentContent();
        }
        else
        {
            return this.containers[index].GetCurrentContent();
        }
    }

    public void MoveItemBetweenContainers(int indexFrom, int indexTo)
    {
        if (indexFrom == -1)
        {
            this.defaultContainer.SetCurrentContent(this.defaultContainer.GetCurrentContent() - 1);
        }
        else
        {
            this.containers[indexFrom].SetCurrentContent(this.containers[indexFrom].GetCurrentContent() - 1);
        }

        if (indexTo == -1)
        {
            this.defaultContainer.SetCurrentContent(this.defaultContainer.GetCurrentContent() + 1);
        }
        else
        {
            this.containers[indexFrom].SetCurrentContent(this.containers[indexFrom].GetCurrentContent() + 1);
        }
    }

    public bool IsFinished()
    {
        for (int i = 0; i < this.containers.Count; i++)
        {
            if (!this.containers[i].ReachedOptimalContent())
            {
                return false;
            }
        }
        return true;
    }
}