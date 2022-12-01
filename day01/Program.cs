using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

List<int> sums = lines.Aggregate((new List<int>(), 0),
    (tuple, next) => {
        if ( next == "" ){
            tuple.Item1.Add(tuple.Item2);
            tuple.Item2 = 0;
        }else{
            tuple.Item2 += Int32.Parse(next);
        }
        return tuple;
    },
    result => result.Item1
);

Console.WriteLine(sums.Max());

List<int> arr_sums = sums;

int sum3 = arr_sums.OrderByDescending(s=>s).Take(3).Sum();

Console.WriteLine(sum3);
