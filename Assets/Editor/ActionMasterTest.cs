using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster actionMaster;

    [SetUp]
    public void SetUp()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void T00PassingTest()
    {
        ////Arrange
        //var gameObject = new GameObject();

        ////Act
        ////Try to rename the GameObject
        //var newGameObjectName = "My game object";
        //gameObject.name = newGameObjectName;

        ////Assert
        ////The object has a new name
        //Assert.AreEqual(newGameObjectName, gameObject.name);


        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn,actionMaster.BowlAndReturnActionToPerform(10));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.BowlAndReturnActionToPerform(8));
    }

    [Test]
    public void T03CompleteFrameNoStrikeNoSpareReturnsEndTurn()
    {
        actionMaster.BowlAndReturnActionToPerform(7);
        Assert.AreEqual(endTurn, actionMaster.BowlAndReturnActionToPerform(2));
    }

    [Test]
    public void T04Bowl2_8SpareReturnsEndTurn()
    {
        Assert.AreEqual(tidy, actionMaster.BowlAndReturnActionToPerform(2));
        Assert.AreEqual(endTurn, actionMaster.BowlAndReturnActionToPerform(8));
    }

    [Test]
    public void T05Bowl0_10SpareIncrementsRollBy1()
    {
        actionMaster.BowlAndReturnActionToPerform(0);
        actionMaster.BowlAndReturnActionToPerform(10);
        Assert.AreEqual(3, actionMaster.roll);
    }

    [Test]
    public void T06Roll20ReturnsTidyIfSpareOnSecondRoll()
    {
        RollDummyFirst9Frames();
        actionMaster.BowlAndReturnActionToPerform(1);
        Assert.AreEqual(tidy, actionMaster.BowlAndReturnActionToPerform(9));
    }

    [Test]
    public void T07Roll20ReturnsTidyIfStrikeOnFirstRoll()
    {
        RollDummyFirst9Frames();
        Assert.AreEqual(reset, actionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(tidy, actionMaster.BowlAndReturnActionToPerform(5));
    }

    [Test]
    public void T08Roll20ReturnsResetIfStrikeOnSecondRoll()
    {
        RollDummyFirst9Frames();
        Assert.AreEqual(reset, actionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(reset, actionMaster.BowlAndReturnActionToPerform(10));
    }

    [Test]
    public void T09FinishGame3Strikes()
    {
        RollDummyFirst9Frames();
        Assert.AreEqual(reset, actionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(reset, actionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(endGame, actionMaster.BowlAndReturnActionToPerform(10));
    }

    [Test]
    public void T10FinishGameNoBonus()
    {
        RollDummyFirst9Frames();
        Assert.AreEqual(tidy, actionMaster.BowlAndReturnActionToPerform(1));
        Assert.AreEqual(endGame, actionMaster.BowlAndReturnActionToPerform(2));
    }

    private void RollDummyFirst9Frames()
    {
        int[] scoreArray = new int[18] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        foreach (int score in scoreArray)
        {
            actionMaster.BowlAndReturnActionToPerform(score);
        }
    }
}
