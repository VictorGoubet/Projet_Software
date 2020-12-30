using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solution.Problem1;
namespace TestSolution
{
    [TestClass]
    public class Test_Problem1
    {
        [TestMethod]
        public void TestEnqueue()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();

            myqueue.enqueue(5);

            //Act
            int actual = myqueue.root.data;

            //Assert
            Assert.AreEqual(5, actual);
        }

        [TestMethod]
        public void Testclear()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();

            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            myqueue.clear();

            //Assert
            Assert.AreEqual(null, myqueue.root);
        }
        [TestMethod]
        public void Testcount()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();

            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            int actual = myqueue.Count();

            //Assert
            Assert.AreEqual(3,actual);
        }

        [TestMethod]
        public void Testcount_empty()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();

            //Act
            int actual = myqueue.Count();

            //Assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void Testequals_whenTrue()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            Queue<int> myqueue2 = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);
            myqueue2.enqueue(5);
            myqueue2.enqueue(10);
            myqueue2.enqueue(7);

            //Act
            bool actual = myqueue.Equals(myqueue2);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Testequals_whenFalse()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            Queue<int> myqueue2 = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);
            myqueue2.enqueue(5);
            myqueue2.enqueue(12);
            myqueue2.enqueue(7);

            //Act
            bool actual = myqueue.Equals(myqueue2);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Testequals_when1Empty()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            Queue<int> myqueue2 = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            bool actual = myqueue.Equals(myqueue2);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Testequals_when2empty()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            Queue<int> myqueue2 = new Queue<int>();

            //Act
            bool actual = myqueue.Equals(myqueue2);

            //Assert
            Assert.AreEqual(myqueue, myqueue2);
        }

        [TestMethod]
        public void Testcontains_whenTrue()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            bool actual = myqueue.contains(5);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Testcontains_whenFalse()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            bool actual = myqueue.contains(12);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void Testcontains_whenEmpty()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            //Act
            bool actual = myqueue.contains(5);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void TestDequeue()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();
            myqueue.enqueue(5);
            myqueue.enqueue(10);
            myqueue.enqueue(7);

            //Act
            int? actual = myqueue.dequeue();

            //Assert
            Assert.AreEqual(7, actual);
            Assert.AreEqual(2, myqueue.Count());

        }

        [TestMethod]
        public void TestDequeue_whenEmpty()
        {
            //Arrange
            Queue<int> myqueue = new Queue<int>();

            //Act
            int? actual = myqueue.dequeue();
            
            //Assert
            Assert.AreEqual(null, actual);
        }

       


    }
}
