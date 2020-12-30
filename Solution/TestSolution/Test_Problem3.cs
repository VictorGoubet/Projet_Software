using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solution.Problem3;
namespace TestSolution
{
    [TestClass]
    public class Test_Problem3
    {

        [TestMethod]
        public void TestAddingPlayers()
        {
            Monopoly game = Monopoly.GetInstance();

            //Adding players
            game.AddPlayer("Jean");
            game.AddPlayer("Paul");
            game.AddPlayer("Jacques");

            Assert.AreEqual(game[0], new Player("Jean", null));

        }

        [TestMethod]
        public void TestGoToJailwDouble()
        {
            Monopoly game = Monopoly.GetInstance();
            game[0].Play(4, 4);
            Assert.AreEqual(game[0].InPrison, true);

        }

        [TestMethod]
        public void TestGoToJailOn30()
        {
            Monopoly game = Monopoly.GetInstance();
            game.Restart(); //Because with the sigleton pattern it keeps the ulterior state
            //Adding players
            game.AddPlayer("Jean");
            game.AddPlayer("Paul");
            game.AddPlayer("Jacques");

            game[0].Play(4, 6);
            game[0].Play(4, 6);
            game[0].Play(4, 6);
            Assert.AreEqual(game[0].InPrison, true);

        }

        [TestMethod]
        public void OutOfJail1wDouble()
        {
            Monopoly game = Monopoly.GetInstance();

            game[0].Play(4, 4);

            Assert.AreEqual(game[0].InPrison, false);
            Assert.AreEqual(game[0].CurrentPosition.value, 18);

        }

        [TestMethod]
        public void OutOfJail1w3Rounds()
        {
            Monopoly game = Monopoly.GetInstance();

            game[0].Play(4, 4); //Go to Jail (it simulate 3 doubles in a row)

            game[0].Play(2, 6);
            game[0].Play(3, 6);
            game[0].Play(4, 6);


            Assert.AreEqual(game[0].InPrison, false);
            Assert.AreEqual(game[0].CurrentPosition.value, 20);

        }

        [TestMethod]
        public void TestSetPosition()
        {
            Monopoly game = Monopoly.GetInstance();
            game[0].GoToCase(39);
            Assert.AreEqual(game[0].CurrentPosition.value, 39);

        }

        [TestMethod]
        public void TestCircularity()
        {
            Monopoly game = Monopoly.GetInstance();
            Assert.AreEqual(game[0].CurrentPosition.next.value, 0);

        }


    }
}