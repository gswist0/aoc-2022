using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

List<Monkey> monkeys = new List<Monkey>();

Monkey tempMonkey = new Monkey(0);

int productOfTests = 1;

lines.ToList().ForEach(line => {
    string[] line_split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if(line_split.Length != 0)
        switch(line_split[0]){
            case "Monkey":
                tempMonkey = new Monkey(Int32.Parse(line_split[1][0].ToString()));
                break;
            case "Starting":
                List<Int64> startingItems = new List<Int64>();
                for(int i = 2; i < line_split.Length; i++){
                    Int64 nr = Int64.Parse(line_split[i].Substring(0,2));
                    startingItems.Add(nr);
                }
                tempMonkey.StartingItems = startingItems;
                break;
            case "Operation:":
                char operation = line_split[4][0];
                tempMonkey.Operation = operation;
                int number;
                bool success = Int32.TryParse(line_split[5], out number);
                if(success)
                    tempMonkey.OperationNumber = number;
                else
                    tempMonkey.OperationNumber = null;
                break;
            case "Test:":
                tempMonkey.TestNumber = Int32.Parse(line_split[3]);
                productOfTests *= Int32.Parse(line_split[3]);
                break;
            case "If":
                if(line_split[1] == "true:"){
                    tempMonkey.TestTrue = Int32.Parse(line_split[5]);
                }else{
                    tempMonkey.TestFalse = Int32.Parse(line_split[5]);
                    monkeys.Add(tempMonkey);
                }
                break;
        }
});

int NUMBER_OF_ROUNDS = 10000;


for(int i = 0; i < NUMBER_OF_ROUNDS; i++){
    for(int j = 0; j < monkeys.Count(); j++){
        Monkey monkey = monkeys[j];
        monkey.Inspects += monkey.StartingItems.Count();
        for(int k = 0; k < monkey.StartingItems.Count(); k++) {//inspect
            Int64 tempItem = monkey.StartingItems[k];//do shit
            Int64 tempOperationNumber = monkey.OperationNumber is Int64 val ? val : tempItem;
            if(monkey.Operation == '*'){
                tempItem *= tempOperationNumber; 
            }else if(monkey.Operation == '+'){
                tempItem += tempOperationNumber;
            }
            tempItem %= productOfTests;
            //tempItem = tempItem / 3;//bored
            int newMonkey = tempItem % monkey.TestNumber == 0 ? monkey.TestTrue : monkey.TestFalse; // test
            monkeys[newMonkey].StartingItems.Add(tempItem);
        }
        monkey.StartingItems = new List<Int64>();
    }

}

Int64 monkeyBusiness = monkeys.Select(monkey => monkey.Inspects).OrderByDescending(s=>s).Take(2).Aggregate((Int64)1, 
    (prod, next) => {
        prod *= next;
        return prod;
    }
);

Console.WriteLine(monkeyBusiness);

class Monkey{
    private int number;
    private List<Int64> startingItems;
    private int testNumber;
    private int testTrue;
    private int testFalse;
    private char operation;
    private Int64? operationNumber;
    private int inspects;

    public Monkey(int number){
        this.number = number;
        inspects = 0;
    }

    public int Number { get => number; set => number = value; }
    public List<Int64> StartingItems { get => startingItems; set => startingItems = value; }
    public int TestNumber { get => testNumber; set => testNumber = value; }
    public int TestTrue { get => testTrue; set => testTrue = value; }
    public char Operation { get => operation; set => operation = value; }
    public Int64? OperationNumber { get => operationNumber; set => operationNumber = value; }
    public int TestFalse { get => testFalse; set => testFalse = value; }
    public int Inspects { get => inspects; set => inspects = value; }

}
