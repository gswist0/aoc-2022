using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

CPU cpu = new CPU();

lines.ToList().ForEach(line => {
    cpu.executeCommand(line);
});

while(!cpu.workDone()){
    cpu.executeCycle();
}

Console.WriteLine(cpu.SignalStrength);

for(int i = 0; i < 6; i++){
    string line = "";
    for(int j = 0; j < 40; j++){
        line += cpu.Pixels[i*40 + j];
    }
    Console.WriteLine(line);
}

class CPU{
    private int cycle;
    private int X;
    private List<(int command , int cyclesLeft)> commandQueue;
    private int currentCyclesTaken;
    private int signalStrength;
    private char[] pixels;

    public int SignalStrength { get => signalStrength; }
    public char[] Pixels { get => pixels;}

    public CPU(){
        cycle = 0;
        X = 1;
        commandQueue = new List<(int, int)>();
        currentCyclesTaken = 0;
        signalStrength = 0;
        pixels = new char[240];
    }

    public bool workDone(){
        return currentCyclesTaken == cycle;
    }

    public void executeCommand(string command){
        string[] command_split = command.Split(' ');

        if(command_split[0] == "addx"){
            commandQueue.Add((Int32.Parse(command_split[1]), currentCyclesTaken + 2));
            currentCyclesTaken += 2;
            executeCycle();
        }
        else if(command_split[0] == "noop"){
            currentCyclesTaken++;
            executeCycle();
        }
    }

    public void executeCycle(){
        cycle++;
        if(cycle % 40 == 20){
            signalStrength += X * cycle;
        }
        drawPixel();
        List<(int, int)> scheduled_for_removal = new List<(int, int)>();
        for(int i = 0; i < commandQueue.Count(); i++){
            if(commandQueue[i].cyclesLeft == cycle){
                X += commandQueue[i].command;
                scheduled_for_removal.Add(commandQueue[i]);
            }
        }
        scheduled_for_removal.ForEach(command => commandQueue.Remove(command));
        
    }

    public void drawPixel(){
        int pos = X + ((cycle - 1) / 40) * 40;
        if(cycle - 1 == pos || cycle - 1 == pos - 1 || cycle - 1 == pos + 1){
            pixels[cycle-1] = '#';
        }else{
            pixels[cycle-1] = '.';
        }
    }
}
