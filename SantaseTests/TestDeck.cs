using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Santase.Logic.Cards;
using Santase.Logic;

namespace SantaseTests
{
    [TestFixture]

    public class TestDeck
    {


        [Test]
        public void TrumpCardShouldNotBeNull()
        {
            var deck = new Deck();

            Assert.True(deck.TrumpCard != null);
        
        }


        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(20)]
        public void CardsLeftShouldDecreaseAfterDrawning(int count)
        {
            var deck = new Deck();
            var cards = 24;

            for (int i = 0; i < count; i++)
            {
                deck.GetNextCard();
                cards--;
            }


            Assert.AreEqual(cards, deck.CardsLeft);

        }

        [Test]
        public void GetNextCardShouldThrowExceptionWhenCalled25Times()
        {
            var deck = new Deck();

            for (int i = 0; i < 24; i++)
            {
                deck.GetNextCard();
            }

            Assert.Throws<InternalGameException>(() => deck.GetNextCard());
        }

        [Test]
        public void GetNextCardShouldNotChangeTrumpCard()
        {
            var deck = new Deck();
            var firstTrump = deck.TrumpCard;

            deck.GetNextCard();
            var currentTrump = deck.TrumpCard;

            Assert.AreEqual(firstTrump, currentTrump);

           
        }

        [Test]
        public void ChangeTrumpCardShouldWorkProperly()
        {
            var deck = new Deck();
            var card = new Card(CardSuit.Heart, CardType.Nine);

            deck.ChangeTrumpCard(card);
            var trumpCard = deck.TrumpCard;

            Assert.AreEqual(card, trumpCard);
        }

        [Test]
        public void ChangeTrumpCardShouldChangeTheLastCard()
        {
            var deck = new Deck();
            var card = new Card(CardSuit.Diamond, CardType.Nine);
            deck.ChangeTrumpCard(card);
            var cardsCount = deck.CardsLeft;

            for (int i = 0; i < cardsCount-1; i++)
            {
                deck.GetNextCard();
            }

            var lastCard = deck.GetNextCard();

            Assert.AreEqual(card, lastCard);

        }
    }
}
