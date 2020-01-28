using UnityEngine;

public static class RandomTexts
{
    private static readonly string[] _loadingScreenPossibleTexts =
        new string[] {
            "Videogames are a form of art. Change my mind.",
            "Gamejams! Gamejams! Gamejams!",
            "Space is a form of empty space.",
            "When you are not alive, you are dead.",
            "Loading screens are just a way to hide the mess the Devs made.",
            "Toss a coin to your arcade!",
            "Pepperidge farm remembers.",
            "Game developers are a rare breed that can survive for centuries with a ramen based diet.",
            "up, up, down, down, left, right, left, right, B, A, Start.",
            "There is a super secret easter egg with a 2% chance of spawn.",
            "8,192: The odds, one against, of encountering a shiny Pokémon under normal circumstances in the first 5 generations.",
            "Affirmative, Dave. I Read you.",
            "Universal Truth: Spamming buttons won't make the loading faster.",
            "They may make our lives! But they will never make our games!",
            "A duel with three people is actually called a Truel, but you won't have this, since the arcade only has 2 joysticks...",
            "Self aware quotes are the best.",
            "Hello World!",
            "Sonic the Hedgehog’s full name is actually Ogilvie Maurice Hedgehog.",
            "In Morse Code -.- means k.",
            "The Mew Truck trick is actually possible.",
            "Imagine a Toad with normal sized legs...",
            "Bread.",
            "No kink shaming here.",
            "uwu",
            "Always eat before every hunt!",
            "May the sapphire star light your way.",
            "Frogs are just fishes with legs.",
            "Im here to serve.",
            "What is my purpose?",
            "Wubba lubba dub dub",
            "Will you take the left red joystick or the right red joystick?",
            "Gubba nub nub doo rah kah.",
            "Inside me is a tiny dwarf live-coding everything you see. Hi!"
        };

    public static string RandomLoadingScreenText => 
        _loadingScreenPossibleTexts[Random.Range(0, _loadingScreenPossibleTexts.Length)];
}
