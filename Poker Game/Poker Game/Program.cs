using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Poker_Game
{
    /*
     *  POKER GAME
     *  Welcome to the Poker Game! Here you can test your luck
     *  and try out the poker game! you can either play the game, and try to win
     *  some earnings, or you can test out each winning possibilities.
     *
     * TESTER'S NAME: Ishpal
     */
    class Program
    {
        public const int NUM_CARDS_IN_HAND = 5;
        public const int ROYAL_FLUSH = 250;
        public const int STRAIGHT_FLUSH = 50;
        public const int FOUR_OF_A_KIND = 25;
        public const int FULL_HOUSE = 9;
        public const int FLUSH = 6;
        public const int STRAIGHT = 4;
        public const int THREE_OF_A_KIND = 3;
        public const int TWO_PAIR = 2;
        public const int PAIR = 1;
        public static int bettingAmount;
        public static int playerBankroll = 1000;
        static void Main(string[] args)
        {
            int playerChoice;
            Console.WriteLine("Welcome to the Poker Game! please select an option!");
            Console.WriteLine("1 : play Poker game");
            Console.WriteLine("2 : Test Poker game");

            while (!int.TryParse(Console.ReadLine(), out playerChoice) || (playerChoice != 1 && playerChoice !=2)) //User input validation
                Console.WriteLine("Invalid Input! Please try again!");

            if(playerChoice == 1)
                PlayPoker();
            else
                TestPoker();

            Console.WriteLine("Thank you for playing/testing my Poker Game!");
            Console.ReadLine();
        }

        /// <summary>
        /// This Function is the main poker game, where you
        /// test out your luck to try and win more cash!
        /// </summary>
        public static void PlayPoker()
        {
            string continuePlayingPoker;
            int count;
            int MAX_DISCARD = 4; //maximum number of card discards allowed
            Deck Deck = new Deck(); //only create a new deck once
            bool continuePlaying = true;
            playerBankroll = 1000;
            while(continuePlaying == true)
            {
                count = 0; //number of times the player has discarded a card
                int playerChoice;
                Deck.Shuffle();
                Card[] playerHand = new Card[NUM_CARDS_IN_HAND];

                Console.WriteLine("Pleaser choose a betting ammount... (0..{0})", playerBankroll);
                while (!int.TryParse(Console.ReadLine(), out bettingAmount) || bettingAmount <= 0 || bettingAmount > playerBankroll)
                    Console.WriteLine("Invalid betting ammount! please try again!");
                playerBankroll -= bettingAmount;
                Console.WriteLine("Money in bank: ${0}", playerBankroll);

                for (int i = 0; i < 5; i++)
                    playerHand[i] = Deck.DealACard();

                Console.WriteLine("Player Hand :");
                Console.WriteLine("[1]" + playerHand[0]);
                Console.WriteLine("[2]" + playerHand[1]);
                Console.WriteLine("[3]" + playerHand[2]);
                Console.WriteLine("[4]" + playerHand[3]);
                Console.WriteLine("[5]" + playerHand[4]);
                Console.WriteLine("Choose the cards you would like to change, or press 0 to continue...");
                Console.WriteLine();

                while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 0 || playerChoice > 5)
                    Console.WriteLine("Invalid Choice! please try again!");

                bool[] cardChosen = new bool[5]; // makes sure the same card cannot be chosen twice
                while (playerChoice != 0 && count != MAX_DISCARD)
                {
                    switch (playerChoice)
                    {
                        case 1:
                            if (cardChosen[0] == true)
                                Console.WriteLine("Card already chosen, please try again");
                            else
                            {
                                playerHand[playerChoice - 1] = Deck.DealACard(); //-1 because arrays start from 0
                                cardChosen[0] = true;
                                count++;
                            }
                            break;
                        case 2:
                            if (cardChosen[1] == true)
                                Console.WriteLine("Card already chosen, please try again");
                            else
                            {
                                playerHand[playerChoice - 1] = Deck.DealACard(); //-1 because arrays start from 0
                                cardChosen[1] = true;
                                count++;
                            }
                            break;
                        case 3:
                            if (cardChosen[2] == true)
                                Console.WriteLine("Card already chosen, please try again");
                            else
                            {
                                playerHand[playerChoice - 1] = Deck.DealACard(); //-1 because arrays start from 0
                                cardChosen[2] = true;
                                count++;
                            }
                            break;
                        case 4:
                            if (cardChosen[3] == true)
                                Console.WriteLine("Card already chosen, please try again");
                            else
                            {
                                playerHand[playerChoice - 1] = Deck.DealACard(); //-1 because arrays start from 0
                                cardChosen[3] = true;
                                count++;
                            }
                            break;
                        case 5:
                            if (cardChosen[4] == true)
                                Console.WriteLine("Card already chosen, please try again");
                            else
                            {
                                playerHand[playerChoice - 1] = Deck.DealACard(); //-1 because arrays start from 0
                                cardChosen[4] = true;
                                count++;
                            }
                            break;
                    }
                    if (count != MAX_DISCARD)
                    {
                        Console.WriteLine("Any other cards? (0 to continue...)");
                        while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice < 0 || playerChoice > 5)
                            Console.WriteLine("Invalid Choice! please try again!");
                    }
                }
                Console.WriteLine("Player Hand :");
                Console.WriteLine("[1]" + playerHand[0]);
                Console.WriteLine("[2]" + playerHand[1]);
                Console.WriteLine("[3]" + playerHand[2]);
                Console.WriteLine("[4]" + playerHand[3]);
                Console.WriteLine("[5]" + playerHand[4]);
                Console.WriteLine();
                int payout = EvaluateHand(playerHand);
                playerBankroll += bettingAmount * payout;

                Console.WriteLine("Continue playing? Y/N");
                continuePlayingPoker = Console.ReadLine();
                while(continuePlayingPoker.ToUpper() != "Y" && continuePlayingPoker.ToUpper() != "N")
                {
                    Console.WriteLine("Error! Invalid input, please try again");
                    continuePlayingPoker = Console.ReadLine();
                }
                if (continuePlayingPoker.ToUpper() == "N")
                    continuePlaying = false;
            }
        }
        /// <summary>
        /// Test function. Here you can test out each winning possibilities
        /// </summary>
        public static void TestPoker()
        {
            int playerChoice = 1; //place holder
            Card[] playerHand = new Card[NUM_CARDS_IN_HAND];

            while (playerChoice != 0)
            {
                playerBankroll = 1000; //stays 1000 every time the user tries another option
                Console.WriteLine("1: Test Royal Flush");
                Console.WriteLine("2: Test Straight Flush");
                Console.WriteLine("3: Test Four of a Kind");
                Console.WriteLine("4: Test Full House");
                Console.WriteLine("5: Test Flush");
                Console.WriteLine("6: Test Straight");
                Console.WriteLine("7: Test Three of a Kind");
                Console.WriteLine("8: Test 2 Pair");
                Console.WriteLine("9: Test Pair");
                Console.WriteLine("0: Quit");
                Console.WriteLine();

                while (!int.TryParse(Console.ReadLine(), out playerChoice) || playerChoice > 9 || playerChoice < 0)
                {
                    Console.WriteLine("Incorrect value inputted! please try again");
                }

                switch (playerChoice)
                {
                    case 1:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Jack);
                        playerHand[2] = new Card(Card.Suit.Spades, Card.FaceValue.Queen);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.King);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Ace);
                        break;
                    case 2:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Two);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Three);
                        playerHand[2] = new Card(Card.Suit.Spades, Card.FaceValue.Four);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Five);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Six);

                        break;
                    case 3:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        playerHand[1] = new Card(Card.Suit.Hearts, Card.FaceValue.Ten);
                        playerHand[2] = new Card(Card.Suit.Clubs, Card.FaceValue.Ten);
                        playerHand[3] = new Card(Card.Suit.Diamonds, Card.FaceValue.Ten);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        break;
                    case 4:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        playerHand[1] = new Card(Card.Suit.Diamonds, Card.FaceValue.Ten);
                        playerHand[2] = new Card(Card.Suit.Hearts, Card.FaceValue.Ten);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Queen);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Queen);

                        break;
                    case 5:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Two);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Four);
                        playerHand[2] = new Card(Card.Suit.Spades, Card.FaceValue.Five);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Seven);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        break;
                    case 6:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Six);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Seven);
                        playerHand[2] = new Card(Card.Suit.Diamonds, Card.FaceValue.Eight);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Nine);
                        playerHand[4] = new Card(Card.Suit.Hearts, Card.FaceValue.Ten);
                        break;
                    case 7:
                        playerHand[0] = new Card(Card.Suit.Diamonds, Card.FaceValue.Four);
                        playerHand[1] = new Card(Card.Suit.Clubs, Card.FaceValue.Four);
                        playerHand[2] = new Card(Card.Suit.Hearts, Card.FaceValue.Four);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Seven);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Nine);
                        break;
                    case 8:
                        playerHand[0] = new Card(Card.Suit.Hearts, Card.FaceValue.Six);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Six);
                        playerHand[2] = new Card(Card.Suit.Diamonds, Card.FaceValue.Eight);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Eight);
                        playerHand[4] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        break;
                    case 9:
                        playerHand[0] = new Card(Card.Suit.Spades, Card.FaceValue.Three);
                        playerHand[1] = new Card(Card.Suit.Spades, Card.FaceValue.Four);
                        playerHand[2] = new Card(Card.Suit.Spades, Card.FaceValue.Ten);
                        playerHand[3] = new Card(Card.Suit.Spades, Card.FaceValue.Jack);
                        playerHand[4] = new Card(Card.Suit.Hearts, Card.FaceValue.Jack);
                        break;
                    case 0:
                        playerChoice = 0;
                        break;
                }
                if(playerChoice != 0)
                {
                    Console.WriteLine("Pleaser choose a betting ammount... (0..{0})", playerBankroll);
                    while (!int.TryParse(Console.ReadLine(), out bettingAmount) || bettingAmount <= 0 || bettingAmount > playerBankroll)
                        Console.WriteLine("Invalid betting ammount! please try again!");
                    playerBankroll -= bettingAmount;
                    Console.WriteLine();
                    Console.WriteLine("Money in bank: ${0}", playerBankroll);

                    Console.WriteLine("Player Hand :");
                    Console.WriteLine("[1]" + playerHand[0]);
                    Console.WriteLine("[2]" + playerHand[1]);
                    Console.WriteLine("[3]" + playerHand[2]);
                    Console.WriteLine("[4]" + playerHand[3]);
                    Console.WriteLine("[5]" + playerHand[4]);
                    Console.WriteLine();

                    EvaluateHand(playerHand);
                }
            }
        }
        /// <summary>
        /// This function recieves player's hand, and checks if it contains a winning hand. If not, then it returns 0, meaning the player 
        /// has woon nothing
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns></returns>
        public static int EvaluateHand(Card[] playerHand)
        {
            playerHand = SortByCardValue(playerHand);

            if (IsStraight(playerHand) && IsFlush(playerHand) && playerHand[4].faceValue == Card.FaceValue.Ace)
            {
                Print("Royal Flush", ROYAL_FLUSH);
                return ROYAL_FLUSH;
            }
            if (IsStraight(playerHand) && IsFlush(playerHand))
            {
                Print("Straight Flush", STRAIGHT_FLUSH);
                return STRAIGHT_FLUSH;
            }

            if (IsFourOfAKind(playerHand))
            {
                Print("Four Of A Kind", FOUR_OF_A_KIND);
                return FOUR_OF_A_KIND;
            }

            if (IsFullHouse(playerHand))
            {
                Print("Full House", FULL_HOUSE);
                return FULL_HOUSE;
            }
            if (IsFlush(playerHand))
            {
                Print("Flush", FLUSH);
                return FLUSH;
            }
            if (IsStraight(playerHand))
            {
                Print("Straight", STRAIGHT);
                return STRAIGHT;
            }
            if (IsTreeOfAKind(playerHand))
            {
                Print("Three Of a Kind", THREE_OF_A_KIND);
                return THREE_OF_A_KIND;
            }
            if (IsTwoPair(playerHand))
            {
                Print("Two Pair", TWO_PAIR);
                return TWO_PAIR;
            }
            if (IsPair(playerHand))
            {
                Print("Pair", PAIR);
                return PAIR;
            }
            else
            {
                Print("None", 0);
                return 0;
            }
                
        }
        public static bool IsFlush(Card[] playerHand)
        {
            SortBySuit(playerHand);
            return playerHand[0].suit == playerHand[4].suit;
            /*
             * since we sorted by suit, if the lowest suit equals the highest suit,
             * it means that there is a flush, and we thus return true.            
             */
        }
        public static bool IsStraight(Card[] playerHand)
        {
            SortByCardValue(playerHand);

            //Since ace could be the highest or lowest value, we make a different validation for it
            if (playerHand[4].faceValue == Card.FaceValue.Ace) //check if the hand contains an ace
            {
                bool aceAsLowestValue;
                bool aceAsHighestValue;

                aceAsLowestValue = playerHand[0].faceValue == Card.FaceValue.Two && playerHand[1].faceValue == Card.FaceValue.Three && playerHand[2].faceValue == Card.FaceValue.Four && playerHand[3].faceValue == Card.FaceValue.Five;
                aceAsHighestValue = playerHand[0].faceValue == Card.FaceValue.Ten && playerHand[1].faceValue == Card.FaceValue.Jack && playerHand[2].faceValue == Card.FaceValue.Queen && playerHand[3].faceValue == Card.FaceValue.King;

                if (aceAsLowestValue == true)
                    return aceAsLowestValue;
                else
                    return aceAsHighestValue;
            }
            else //if there is no ace in player hand, we use a more simple method with loops
            {
                Card testCardValue = new Card();
                testCardValue.faceValue = playerHand[0].faceValue + 1;

                for(int i = 1; i < 5; i++)
                {
                    if (playerHand[i].faceValue != testCardValue.faceValue)
                        return false;

                    testCardValue.faceValue++;
                }
                return true;
            }
        }
        public static bool IsFourOfAKind(Card[] playerHand)
        {
            //since 4 cards must be the same, and the 5th one could be anything, we first rank the cards by face value
            playerHand = SortByCardValue(playerHand);
            //then, the 5th card, not part of the four of a kind, could be the lowest or highest card value
            //knowing that, we just have to check cards 1 to 4 and 2 to 5 to see if we have a four of a kind

            bool oneToFour = playerHand[0].faceValue == playerHand[1].faceValue &&
                             playerHand[1].faceValue == playerHand[2].faceValue &&
                             playerHand[2].faceValue == playerHand[3].faceValue;

            bool twoToFive = playerHand[1].faceValue == playerHand[2].faceValue &&
                             playerHand[2].faceValue == playerHand[3].faceValue &&
                             playerHand[3].faceValue == playerHand[4].faceValue;

            if (oneToFour == true)
                return oneToFour;
            else
                return twoToFive;
                    
        }
        public static bool IsFullHouse(Card[] playerHand)
        {
            //this is similar to the four of a kind method
            playerHand = SortByCardValue(playerHand);

            bool case1;
            bool case2;

            case1 = playerHand[0].faceValue == playerHand[1].faceValue &&
                    playerHand[1].faceValue == playerHand[2].faceValue &&
                    playerHand[3].faceValue == playerHand[4].faceValue;

            case2 = playerHand[2].faceValue == playerHand[3].faceValue &&
                    playerHand[3].faceValue == playerHand[4].faceValue &&
                    playerHand[0].faceValue == playerHand[1].faceValue;

            if (case1 == true)
                return case1;
            else
                return case2;
                   
        }
        public static bool IsTreeOfAKind(Card[] playerHand)
        {
            //similar to full house and four of a kind, but the three of a kind could be place in 3 different ways:
            //xxxoo
            //oxxxo
            //ooxxx
            //soo we must consider these 3 possibilities

            playerHand = SortByCardValue(playerHand);
            bool case1;
            bool case2;
            bool case3;

            case1 = playerHand[0].faceValue == playerHand[1].faceValue &&
                    playerHand[1].faceValue == playerHand[2].faceValue &&
                    playerHand[3].faceValue != playerHand[0].faceValue &&
                    playerHand[4].faceValue != playerHand[0].faceValue &&
                    playerHand[3].faceValue != playerHand[4].faceValue;

            case2 = playerHand[1].faceValue == playerHand[2].faceValue &&
                    playerHand[2].faceValue == playerHand[3].faceValue &&
                    playerHand[0].faceValue != playerHand[1].faceValue &&
                    playerHand[4].faceValue != playerHand[1].faceValue &&
                    playerHand[0].faceValue != playerHand[4].faceValue;

            case3 = playerHand[2].faceValue == playerHand[3].faceValue &&
                    playerHand[3].faceValue == playerHand[4].faceValue &&
                    playerHand[0].faceValue != playerHand[2].faceValue &&
                    playerHand[1].faceValue != playerHand[2].faceValue &&
                    playerHand[0].faceValue != playerHand[1].faceValue;

            if (case1 == true)
                return case1;
            else if (case2 == true)
                return case2;
            else
                return case3;
                    
        }
        public static bool IsTwoPair(Card[] playerHand)
        {
            playerHand = SortByCardValue(playerHand);
            bool case1;
            bool case2;
            bool case3;

            case1 = playerHand[0].faceValue == playerHand[1].faceValue &&
                    playerHand[2].faceValue == playerHand[3].faceValue;

            case2 = playerHand[0].faceValue == playerHand[1].faceValue &&
                    playerHand[3].faceValue == playerHand[4].faceValue;

            case3 = playerHand[1].faceValue == playerHand[2].faceValue &&
                    playerHand[3].faceValue == playerHand[4].faceValue;

            if (case1 == true)
                return case1;
            else if (case2 == true)
                return case2;
            else
                return case3;

        }
        public static bool IsPair(Card[] playerHand)
        {
            playerHand = SortByCardValue(playerHand);

            bool case1 = playerHand[0].faceValue == playerHand[1].faceValue && playerHand[0].faceValue > Card.FaceValue.Ten;
            bool case2 = playerHand[1].faceValue == playerHand[2].faceValue && playerHand[1].faceValue > Card.FaceValue.Ten;
            bool case3 = playerHand[2].faceValue == playerHand[3].faceValue && playerHand[2].faceValue > Card.FaceValue.Ten;
            bool case4 = playerHand[3].faceValue == playerHand[4].faceValue && playerHand[3].faceValue > Card.FaceValue.Ten;

            if (case1 == true)
                return case1;
            if (case2 == true)
                return case2;
            if (case3 == true)
                return case3;
            else
                return case4;

        }
        /// <summary>
        /// This function receives the player's hand, and sorts it by suit, from lowest to highest. This facilitates validation
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns> playerHand </returns>
        public static Card[] SortBySuit(Card[] playerHand)
        {
            for(int i = 0; i < playerHand.Length; i++)
            {
                int smallestValue = i; //for now, the smallest value in the hand is the first card.
                for(int j = i + 1; j < playerHand.Length; j++)
                {
                    if(playerHand[j].suit > playerHand[smallestValue].suit)
                        smallestValue = j;
                }
                Card cardHolder = playerHand[i];
                playerHand[i] = playerHand[smallestValue];
                playerHand[smallestValue] = cardHolder;
            }
            return playerHand;
        }
        /// <summary>
        /// This function behaves similarly to the SortBySuit Function. It recieves the player's hand, and sorts
        /// the cards by face value, from lowest to highest. This also facilitates validation
        /// </summary>
        /// <param name="playerHand"></param>
        /// <returns> PlayerHand </returns>
        public static Card[] SortByCardValue(Card[] playerHand)
        {
            for(int i = 0; i < playerHand.Length; i++)
            {
                int smallestValue = i;
                for(int j = i + 1; j < playerHand.Length; j++)
                {
                    if (playerHand[j].faceValue < playerHand[smallestValue].faceValue)
                        smallestValue = j;
                }
                Card cardHolder = playerHand[i];
                playerHand[i] = playerHand[smallestValue];
                playerHand[smallestValue] = cardHolder;
            }
            return playerHand;
        }
        public static void Print(string handRating, int payout)
        {
            Console.WriteLine("Winning Hand: " + handRating);
            Console.WriteLine("You won: " + payout * bettingAmount);
            Console.WriteLine("Money in bank: $" + (payout * bettingAmount + playerBankroll));
            Console.WriteLine();
        }
    }
}
