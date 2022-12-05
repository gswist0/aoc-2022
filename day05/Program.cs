using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

List<string> setup = lines.Take(9).ToList();
setup.Reverse();
List<string> instructions = lines.Skip(10).ToList();
List<Stack<char>> stacks = new List<Stack<char>>();
List<Stack<char>> stacks2 = new List<Stack<char>>();

for(int i = 0; i < 9; i++){
    stacks.Add(new Stack<char>());
    stacks2.Add(new Stack<char>());
}

foreach(string line in setup.Skip(1)){
    for(int i = 0; i < line.Length; i++){
        char sign = line[i];
        if(sign != '[' && sign != ']' && sign != ' '){
            int index = Convert.ToInt32(setup.First()[i]) - 49;
            stacks[index].Push(sign);
            stacks2[index].Push(sign);
        }
    }
}



foreach(string instruction in instructions){
    string[] newInstruction = instruction.Replace("move"," ").Replace("from"," ").Replace("to"," ").Split(' ', StringSplitOptions.RemoveEmptyEntries);
    for(int i = 0; i < Int32.Parse(newInstruction[0]); i++){
        char sign = stacks[Int32.Parse(newInstruction[1]) - 1].Pop();
        stacks[Int32.Parse(newInstruction[2]) - 1].Push(sign);
    }
}

string result = "";

foreach(Stack<char> stack in stacks){
    result += stack.Peek();
}

Console.WriteLine(result);

foreach(string instruction in instructions){
    string[] newInstruction = instruction.Replace("move"," ").Replace("from"," ").Replace("to"," ").Split(' ', StringSplitOptions.RemoveEmptyEntries);
    Stack<char> pushStack = new Stack<char>();
    for(int i = 0; i < Int32.Parse(newInstruction[0]); i++){
        char sign = stacks2[Int32.Parse(newInstruction[1]) - 1].Pop();
        pushStack.Push(sign);
    }
    foreach(char sign in pushStack)
        stacks2[Int32.Parse(newInstruction[2]) - 1].Push(sign);
}


string result2 = "";

foreach(Stack<char> stack in stacks2){
    result2 += stack.Peek();
}

Console.WriteLine(result2);
