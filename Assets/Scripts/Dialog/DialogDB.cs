using System;
using System.Collections.Generic;
using System.IO;
using NPC;

namespace Dialog
{
    public static class DialogDB
    {
        private static Random random = new Random();
        private static List<string> _returningBooks = new List<string>();
        private static List<string> _wantingBooks = new List<string>();

        static DialogDB()
        {
            ParseSentences(_returningBooks, "returning_books");
            ParseSentences(_wantingBooks, "wanting_books");
        }

        private static void ParseSentences(List<string> db, string fileName)
        {
            using (StreamReader sr = File.OpenText("Assets/Database/" + fileName + ".txt"))
            {
                string sentence;
                while ((sentence = sr.ReadLine()) != null)
                {
                    db.Add(sentence);
                }

                Console.WriteLine();
            }
        }

        public static string GetRandomSentence(NPCAction action, string actionInfo)
        {
            string formattedSentence;
            switch (action)
            {
                case NPCAction.ReturningBook:
                    formattedSentence = _returningBooks[random.Next(_returningBooks.Count)];
                    break;
                case NPCAction.WantingBook:
                    formattedSentence = _wantingBooks[random.Next(_wantingBooks.Count)];
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (formattedSentence.HasPlaceholder())
            {
                formattedSentence = String.Format(formattedSentence, actionInfo);
            }

            return formattedSentence;
        }
    }
}