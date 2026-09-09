using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities: ("A", 2), ("B", 5), ("C", 3).
    // Dequeue twice to verify the highest priority item is returned and removed from the queue.
    // Expected Result: First dequeue returns "B" (priority 5). Second dequeue returns "C" (priority 3).
    // Defect(s) Found: The item with the highest priority is not removed from the queue (_queue.RemoveAt(highPriorityIndex) is missing),
    // causing subsequent dequeues to return the same item repeatedly.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        var first = priorityQueue.Dequeue();
        Assert.AreEqual("B", first);

        var second = priorityQueue.Dequeue();
        Assert.AreEqual("C", second);
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest priority item is at the very back of the queue: ("A", 2), ("B", 3), ("C", 5).
    // Dequeue the highest priority item.
    // Expected Result: Dequeue returns "C" (priority 5).
    // Defect(s) Found: The loop condition in Dequeue is index < _queue.Count - 1, which never checks
    // the last item in the queue. Thus, if the highest priority item is at the end, it is ignored.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);

        var result = priorityQueue.Dequeue();
        Assert.AreEqual("C", result);
    }

    [TestMethod]
    // Scenario: Enqueue multiple items where more than one item shares the highest priority:
    // ("A", 2), ("B", 5), ("C", 3), ("D", 5), ("E", 1).
    // Dequeue twice to check FIFO order among tied highest priorities.
    // Expected Result: First dequeue returns "B" (first item with priority 5). Second dequeue returns "D" (second item with priority 5).
    // Defect(s) Found: The condition in Dequeue uses >= (_queue[index].Priority >= _queue[highPriorityIndex].Priority)
    // which overwrites highPriorityIndex with later items, returning the last item with highest priority instead of the first (violating FIFO).
    public void TestPriorityQueue_TieBreakerFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 5);
        priorityQueue.Enqueue("E", 1);

        var first = priorityQueue.Dequeue();
        Assert.AreEqual("B", first);

        var second = priorityQueue.Dequeue();
        Assert.AreEqual("D", second);
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException should be thrown with the message "The queue is empty."
    // Defect(s) Found: None :)
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                    e.GetType(), e.Message)
            );
        }
    }
}