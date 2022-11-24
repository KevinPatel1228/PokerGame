using Poker_Game;
using System;

public class Card
{
	public enum FaceValue { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };
	// Note: Ace is both the lowest and highest card
	public enum Suit { Hearts, Diamonds, Clubs, Spades };

	public FaceValue faceValue;
	public Suit suit;

	public Card()
	{
		suit = Suit.Spades;
		faceValue = FaceValue.Ace;
	}

	public Card(Suit theSuit_, FaceValue theFaceValue_)
	{
		suit = theSuit_;
		faceValue = theFaceValue_;
	}

	public override string ToString()
	{
		return faceValue + " of " + suit;
	}
}
