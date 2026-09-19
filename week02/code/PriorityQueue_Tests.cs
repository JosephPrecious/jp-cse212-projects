using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue A (priority 1), B (priority 5), C (priority 3), then dequeue three times.
    // Expected Result: B, C, A (highest priority first, and each item is removed once dequeued).
    // Defect(s) Found: Failed on the second Dequeue. B was returned again instead of C because
    // Dequeue returned the value but never removed the item from the list (missing RemoveAt).
    // The loop bound also skipped the last item in the list, which affected this test once the
    // removal was fixed.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (priority 3), B (priority 3), C (priority 1), then dequeue three times.
    // Expected Result: A, B, C (when priorities tie, the item closest to the front goes first).
    // Defect(s) Found: Failed on the first Dequeue. B was returned instead of A because the
    // comparison used >= so a later item with an equal priority replaced the earlier one.
    // It must be > so the first item with the highest priority is kept.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (priority 1), B (priority 2), C (priority 9), so the highest priority
    // item is at the very back of the queue. Dequeue three times.
    // Expected Result: C, B, A
    // Defect(s) Found: Failed on the first Dequeue. B was returned instead of C because the loop
    // condition was index < _queue.Count - 1, so the last item in the queue was never compared.
    // It must be index < _queue.Count.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: An InvalidOperationException is thrown with the message "The queue is empty."
    // Defect(s) Found: None. This test passed before and after the fixes.
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

    [TestMethod]
    // Scenario: Enqueue A (priority 1) and B (priority 4). Dequeue once. Then enqueue C (priority 4)
    // and dequeue twice more.
    // Expected Result: B, C, A (an item dequeued earlier must not come back, and new items
    // are added to the back of the queue).
    // Defect(s) Found: Failed on the first Dequeue. A was returned instead of B because, with only
    // two items in the queue, the loop never ran (index < Count - 1) so the search never looked
    // past the first item. Items also stayed in the queue after being dequeued (missing RemoveAt).
    public void TestPriorityQueue_InterleavedEnqueueDequeue()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 4);

        Assert.AreEqual("B", priorityQueue.Dequeue());

        priorityQueue.Enqueue("C", 4);

        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }
}
