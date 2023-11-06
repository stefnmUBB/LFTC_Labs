using Lab3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab3.Lexic
{
    public class SymbolsTable
    {
        public static List<string> ReservedAtomsList =
            new List<string> { "%ID%", "%CONST%" }
            .Concat(Resources.rKeyWords.Split('\n'))
            .Concat(Resources.rSymbols.Split('\n'))
            .ToList();

        public readonly Symbol[] TS;
        public readonly (int Cod, int TSPos)[] FIP;

        public SymbolsTable(List<Token> tokens)
        {            
            var symbols = new SortedDictionary<string, Symbol>();
            FIP = new (int, int)[tokens.Count];

            var solveQ = new List<(int fipPos, Symbol s)>();

            for(int i=0;i<tokens.Count;i++)
            {
                FIP[i].TSPos = -1;
                
                var index = ReservedAtomsList.IndexOf(tokens[i].Text);                

                if (index >= 2)
                    FIP[i].Cod = index;
                else
                {
                    if (index == 0 || index == 1) throw new InvalidOperationException();

                    var s = symbols.TryGetValue(tokens[i].Text, out var sym) ? sym : symbols[tokens[i].Text] = new Symbol(tokens[i].Text);

                    FIP[i].Cod = IsNumberConstLiteral(tokens[i]) ? 1
                        : IsIdentifierName(tokens[i]) ? 0 : throw new ArgumentException($"Invalid token: '{tokens[i]}'");

                    solveQ.Add((i, s));
                }
            }
            int k = 0;            

            symbols.Values.ToList().ForEach(_ => _.Id = k++);


            solveQ.ForEach(_ => FIP[_.fipPos].TSPos = _.s.Id);
            TS = symbols.Values.ToArray();
        }


        static bool IsNumberConstLiteral(Token x) => x.Type == "CONST";
        static bool IsIdentifierName(Token x) => x.Type == "ID";        
    }
}
