using System.Collections.Generic;

namespace TilesDashboard.Plugin.CodeCoverageSlackNotification
{
    public static class MessageMoodDictionary
    {
        public readonly static IList<string> AmazingMessages = new List<string>
        {
            "Amazing work! :sunglasses:",
            "I didn't think I'd live enough to see such code coverage! :open_mouth:",
            "Are you sure you are not cheating? :face_with_raised_eyebrow:",
            "Impressive :muscle:",
            "You are like Gandalf in the programming world, almost a god :mage: "
        };

        public readonly static IList<string> GoodMessages = new List<string>
        {
            "Good work! :sunglasses:",
            "Good job! :sunglasses:",
            "Your goal has been achieved, now you can drink coffee :coffee: ",
            ":+1:",
            "How many programmers does it take to change a light bulb?\nNone, that's a hardware problem.",
            "How did you like my HTTP 200 joke?\nIt was Ok.",
            "Why was the mobile phone wearing glasses?\nBecause it lost its contacts.",
            "How do computers attack each other?\nBy using pop-up ads.",
            "What did the computer have during his break time?\nHe had a byte!",
            "Why did the mother put airbags on the computer?\nBecause the computer might crash.",
            "Why did the PowerPoint presentation decide to cross the road?\nBecause he wanted to get to the other slide.",
            "Why did the computer go to the dentist?\nTo get his Bluetooth checked.",
            "Why did the computer keep playing Titanic on screen?\nBecause it got synced.",
            "What is the biggest lie anyone can tell?\nI have read and agreed to all the terms and conditions",
            "How many Microsoft programmers does it take to change a light bulb?\nNone as according to them, darkness is the new standard.",
            "Why did the geek add body { padding-top: 1000px; } to his Facebook profile?\nHe wanted to keep a low profile."
        };

        public readonly static IList<string> CouldBeBetterMessages = new List<string>
        {
            "Good enough (for some) ;)",
            "Above limit, not... bad :upside_down_face:",
            "Could Be Better",
            "This is all you can do? :disappointed: ",
            "My grandma has better code coverage than you do :older_woman: "
        };

        public readonly static IList<string> BadMessages = new List<string>
        {
            ":-1:",
            "Do you know what Unit Tests are?",
            ":neutral_face: ",
            "Bad, very bad developers :police_car: ",
            "Today I'm in a good mood, so I won't complain :woozy_face:",
            ":see_no_evil:"
        };
    }
}
