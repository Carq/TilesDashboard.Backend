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
            ":+1:"
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
