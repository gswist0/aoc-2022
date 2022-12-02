using System;
using System.IO;
using System.Collections;


string[] lines = File.ReadAllLines("input.txt");

int points = lines.Aggregate(new Int32(),
    (sum, next) => {
        char[] splitLineChar = next.ToCharArray();
        int[] splitLine = {Convert.ToInt32(splitLineChar[0]), Convert.ToInt32(splitLineChar[2])};
        splitLine[1] = splitLine[1] - 23;
        //outcome
        if(splitLine[0] == splitLine[1]) sum += 3;//draw
        else if(splitLine[0] == splitLine[1] - 1 || splitLine[0] == splitLine[1] + 2) sum+=6;//win
        //choice
        sum += splitLine[1] - 64;
        return sum;
    }
);

Console.WriteLine(points);


int points2 = lines.Aggregate(new Int32(),
    (sum, next) => {
        char[] splitLineChar = next.ToCharArray();
        int[] splitLine = {Convert.ToInt32(splitLineChar[0]), Convert.ToInt32(splitLineChar[2])};
        splitLine[1] = splitLine[1] - 23;
        int outcome = 65;
        switch(splitLine[1]){
            case 65:
                outcome = splitLine[0] - 1;
                if (outcome < 65) outcome = 67;
                break;
            case 66:
                outcome = splitLine[0];
                break;
            case 67:
                outcome = splitLine[0] + 1;
                if (outcome > 67) outcome = 65;
                break;
        }
        sum += outcome - 64;
        sum += (splitLine[1] - 65)*3;
        return sum;
    }
);

Console.WriteLine(points2);
