using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    
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
        List<int> pinList = new List<int> { 10 };
        Assert.AreEqual(endTurn,ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T02Bowl8ReturnsTidy()
    {
        List<int> pinList = new List<int> { 8 };
        //Assert.AreEqual(tidy, ActionMaster.BowlAndReturnActionToPerform(8));
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T03CompleteFrameNoStrikeNoSpareReturnsEndTurn()
    {
        List<int> pinList = new List<int> { 7, 2 };
        //ActionMaster.BowlAndReturnActionToPerform(7);
        //Assert.AreEqual(endTurn, ActionMaster.BowlAndReturnActionToPerform(2));
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T04Bowl2_8SpareReturnsEndTurn()
    {
        List<int> pinList = new List<int> { 2, 8 };
        ////Assert.AreEqual(tidy, ActionMaster.BowlAndReturnActionToPerform(2));

        //Assert.AreEqual(tidy, ActionMaster.NextAction(pinList));

        ////Assert.AreEqual(endTurn, ActionMaster.BowlAndReturnActionToPerform(8));
        
        //pinList.Add(8);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinList));
    }

    //[Test]
    //public void T05Bowl0_10SpareIncrementsRollBy1()
    //{
    //    List<int> pinList = new List<int> { 0, 10 };
    //    ActionMaster.BowlAndReturnActionToPerform(0);
    //    ActionMaster.BowlAndReturnActionToPerform(10);
    //    Assert.AreEqual(3, ActionMaster.roll);
    //}

    [Test]
    public void T06Roll20ReturnsTidyIfSpareOnSecondRoll()
    {
        //RollDummyFirst9Frames();
        List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 9 };
        //ActionMaster.BowlAndReturnActionToPerform(1);
        //Assert.AreEqual(tidy, ActionMaster.BowlAndReturnActionToPerform(9));
        Assert.AreEqual(reset, ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T07Roll20ReturnsTidyIfStrikeOnFirstRoll()
    {
        //RollDummyFirst9Frames();
        List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 10, 5 };
        //Assert.AreEqual(reset, ActionMaster.BowlAndReturnActionToPerform(10));
        //Assert.AreEqual(reset, ActionMaster.NextAction(pinList));

        //pinList.Add(5);
        //Assert.AreEqual(tidy, ActionMaster.BowlAndReturnActionToPerform(5));
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinList));
        
    }

    [Test]
    public void T08Roll20ReturnsResetIfStrikeOnSecondRoll()
    {
        //RollDummyFirst9Frames();
        List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 10, 10 };
        //Assert.AreEqual(reset, ActionMaster.BowlAndReturnActionToPerform(10));
        //Assert.AreEqual(reset, ActionMaster.NextAction(pinList));

        //pinList.Add(10);
        //Assert.AreEqual(reset, ActionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(reset, ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T09FinishGame3Strikes()
    {
        //RollDummyFirst9Frames();
        List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 10, 10, 10 };
        //Assert.AreEqual(reset, ActionMaster.BowlAndReturnActionToPerform(10));
        //Assert.AreEqual(reset, ActionMaster.NextAction(pinList));

        //pinList.Add(10);
        //Assert.AreEqual(reset, ActionMaster.BowlAndReturnActionToPerform(10));
        //Assert.AreEqual(reset, ActionMaster.NextAction(pinList));

        //pinList.Add(10);
        //Assert.AreEqual(endGame, ActionMaster.BowlAndReturnActionToPerform(10));
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinList));        
    }

    [Test]
    public void T10FinishGameNoBonus()
    {
        //RollDummyFirst9Frames();
        List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
        //Assert.AreEqual(tidy, ActionMaster.BowlAndReturnActionToPerform(1));
        //Assert.AreEqual(tidy, ActionMaster.NextAction(pinList));

        //pinList.Add(2);
        //Assert.AreEqual(endGame, ActionMaster.BowlAndReturnActionToPerform(2));
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinList));
    }

    [Test]
    public void T11PerfectGameReturnsEndGame()
    {
        List<int> pinList = new List<int> { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinList));
    }

    //private void RollDummyFirst9Frames()
    //{
    //    List<int> pinList = new List<int> { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
    //    ActionMaster.BowlAndReturnActionToPerform(pinList);
    //    //int[] scoreArray = new int[18] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
    //    //foreach (int score in scoreArray)
    //    //{
    //    //    ActionMaster.BowlAndReturnActionToPerform(score);
    //    //}
    //}
}
