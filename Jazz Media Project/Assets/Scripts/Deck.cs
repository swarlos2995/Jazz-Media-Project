﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour 
{
    public List<Card> cards = new List<Card>();
    List<GameObject> expendedCards = new List<GameObject>();
    Stack<Card> deck = new Stack<Card>();
    Stack<Card> discardPile = new Stack<Card>();

    public int maxLength;

    // Start is called before the first frame update
    void Start()
    {

        initDeck();
    }

    private void Update()
    {
        if (deck.Count == 0)
        {
            print("deck is empty");

            shuffle();

            // this is a hotfix
            // the shuffle method should be used
            //initDeck();
        }
    }

    public Card dealCard()
    {
        BattleManager battle = GameObject.FindGameObjectWithTag("Battle Manager").GetComponent<BattleManager>();
        Card card = deck.Pop();
        discardPile.Push(card);

        return card;
    }

    public Card[] dealCards(int numCards)
    {
        List<Card> dealtCards = new List<Card>();

        for (int i = 0; i < numCards; i++)
        {
            dealtCards.Add(dealCard());
        }

        return dealtCards.ToArray();
    }

    // Initialises the deck stack by inserting all cards in a random order
    void initDeck()
    {
        List<Card> cardsCopy = new List<Card>(cards);

        for (int i = 0; i < cards.Count; i++)
        {
            int randIndex = (int)(Random.value * 100) % cardsCopy.Count;

            deck.Push(Instantiate(cardsCopy[randIndex]));

            cardsCopy.RemoveAt(randIndex);
        }
    }

    // reinserts the discard pile into the deck and shuffles the deck
    // Can't use initDeck() function because we need to take into account the
    // player's cards in hand
    public void shuffle()
    {
        List<Card> cardsToShuffle = new List<Card>();
        if (deck.Count > 0)
        {
            cardsToShuffle.AddRange(deck);
            deck.Clear();
        }
            

        if (discardPile.Count > 0)
        {
            cardsToShuffle.AddRange(discardPile);
            discardPile.Clear();
        }

        int size = cardsToShuffle.Count;

        for (int i = 0; i < size; i++)
        {
            int randIndex = (int)(Random.value * 100) % cardsToShuffle.Count;

            deck.Push(cardsToShuffle[randIndex]);

            cardsToShuffle.RemoveAt(randIndex);
        }
    }

    public bool discardCard(Card card)
    {
        discardPile.Push(card);

        return true;
    }


    // returns the card on the top of the deck
    public Card getNextCard()
    {
        if (deck.Count == 0)
        {
            shuffle();
        }

        return deck.Peek();
    }
}
