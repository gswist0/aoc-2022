using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

int sum = lines.Aggregate(new Int32(),
    (currentSum, next) => {
        string first = next.Substring(0, (int)(next.Length / 2));
        string last = next.Substring((int)(next.Length / 2), next.Length - (int)(next.Length / 2));
        IEnumerable<char> common = first.Intersect(last);
        foreach ( char sign in common ){
            currentSum += Char.IsLower(sign) ? (int)sign - 96 : (int)sign - 38;
        }
        return currentSum;
    }
);

Console.WriteLine(sum);

int sum2 = lines.Aggregate((currentSum: new Int32(), lines: new List<string>()),
    (tuple, next) => {
        tuple.lines.Add(next);
        if(tuple.lines.Count != 3)
            return tuple;
        IEnumerable<char> common = (tuple.lines[0].Intersect(tuple.lines[1])).Intersect(tuple.lines[2]);
        foreach ( char sign in common ){
            tuple.currentSum += Char.IsLower(sign) ? (int)sign - 96 : (int)sign - 38;
        }
        tuple.lines.Clear();
        return tuple;
    }
    , tuple => tuple.currentSum
);

Console.WriteLine(sum2);
