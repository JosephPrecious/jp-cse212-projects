using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue A (priority 1), B (priority 5) and C (priority 3), in that order, and
    // check the contents of the queue (Requirement 1: Enqueue adds to the back of the queue
    // regardless of priority).
    // Expected Result: [A (Pri:1), B (Pri:5), C (Pri:3)]
    // Defect(s) Found: PASSED. No defect found. Enqueue() always adds the new item to the back of the
    // queue and does not reorder items by priority.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("[A (Pri:1), B (Pri:5), C (Pri:3)]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue A (priority 1), B (priority 5) and C (priority 3), then call Dequeue once
    // (Requirement 2: Dequeue returns the value of the highest priority item).
    // Expected Result: B
    // Defect(s) Found: PASSED. No defect found by this test. B is returned because it is in the middle
    // of the queue, which the buggy loop does check. (Other tests expose the defects in Dequeue.)
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Enqueue A (priority 1), B (priority 5) and C (priority 3), then call Dequeue three
    // times (Requirement 2: Dequeue removes the highest priority item and returns its value).
    // Expected Result: B, then C, then A (highest priority first, and each item removed once returned).
    // Defect(s) Found: FAILED at the second Dequeue (Expected: C, Actual: B). Dequeue() returns the
    // value of the highest priority item but never removes it from the queue, so the same item is
    // returned again on the next call.
    // Fix: after getting the value, remove the item with _queue.RemoveAt(highPriorityIndex).
    public void TestPriorityQueue_3()
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
    // Scenario: Enqueue A (priority 1), B (priority 2) and C (priority 9), so the highest priority
    // item is the last one in the queue, then call Dequeue once (Requirement 2).
    // Expected Result: C
    // Defect(s) Found: FAILED (Expected: C, Actual: B). The for loop in Dequeue() stops at
    // index < _queue.Count - 1, so the last item in the queue is never checked.
    // Fix: change the loop condition to index < _queue.Count.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (priority 5), B (priority 3), C (priority 5) and D (priority 1). A and C
    // share the highest priority. Call Dequeue four times (Requirement 3: when several items share the
    // highest priority, the one closest to the front is removed first).
    // Expected Result: A, C, B, D
    // Defect(s) Found: FAILED at the first Dequeue (Expected: A, Actual: C). The comparison in
    // Dequeue() uses >=, so when two items have the same priority the later one replaces the earlier
    // one and the item closest to the back is chosen instead of the one closest to the front.
    // Fix: change >= to > in the priority comparison.
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 5);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Call Dequeue on a new, empty queue (Requirement 4: an InvalidOperationException with
    // the message "The queue is empty." is thrown).
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: PASSED. No defect found. Dequeue() checks for an empty queue first and throws
    // an InvalidOperationException with the required message.
    public void TestPriorityQueue_6()
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
    // Scenario: Enqueue a single item, A (priority 1), Dequeue it, then call Dequeue again
    // (Requirements 2 and 4: after the last item is removed, the queue is empty and must throw).
    // Expected Result: First Dequeue returns A. Second Dequeue throws an InvalidOperationException
    // with the message "The queue is empty."
    // Defect(s) Found: FAILED ("Exception should have been thrown."). Because Dequeue() never removes
    // the item it returns, the queue is still not empty and A is returned a second time.
    // Fix: remove the item from the queue with _queue.RemoveAt(highPriorityIndex).
    public void TestPriorityQueue_7()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());

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
