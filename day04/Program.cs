using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

var pairsCount = lines.Aggregate((count: new Int32(), count2: new Int32()),
    (tuple, next) => {
        string[] elfs = next.Split(",");
        string[] elf1 = elfs[0].Split("-");
        string[] elf2 = elfs[1].Split("-");
        var elf1t = (Int32.Parse(elf1[0]),Int32.Parse(elf1[1]));
        var elf2t = (Int32.Parse(elf2[0]),Int32.Parse(elf2[1]));
        if((elf1t.Item1 >= elf2t.Item1 && elf1t.Item2 <= elf2t.Item2) || (elf2t.Item1 >= elf1t.Item1 && elf2t.Item2 <= elf1t.Item2))
            tuple.count++;
        if((elf1t.Item1 >= elf2t.Item2 && elf1t.Item1 <= elf2t.Item2) || (elf1t.Item2 >= elf2t.Item1 && elf1t.Item2 <= elf2t.Item2) || (elf2t.Item1 >= elf1t.Item2 && elf2t.Item1 <= elf1t.Item2) || (elf2t.Item2 >= elf1t.Item1 && elf2t.Item2 <= elf1t.Item2) )
            tuple.count2++;
        return tuple;
    }
);

Console.WriteLine(pairsCount.count + "\n" + pairsCount.count2);


