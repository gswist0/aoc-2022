using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

int[,] grid = new int[lines[0].Length, lines.Length];

for(int i = 0; i < lines.Length; i++){
    for(int j = 0; j < lines[i].Length; j++){
        grid[i,j] = (int)lines[i][j];
    }
}

int visibleTreeCount = 0;
int highestScenicScore = 0;

for(int i = 0; i < grid.GetLength(0); i++){
    for(int j = 0; j < grid.GetLength(1); j++){
        if(i == 0 || j == 0 || i == grid.GetLength(0) - 1 || j == grid.GetLength(1) - 1){
            visibleTreeCount++;
        }
        else{
            int treeHeight = grid[i,j];
            int visibleSides = 4;
            int leftCount = 0;
            int rightCount = 0;
            int upCount = 0;
            int downCount = 0;
            for(int x = i - 1; x >= 0; x--){
                leftCount++;
                if(grid[x,j] >= treeHeight){
                    visibleSides--;
                    break;
                }
            }
            for(int x = i + 1; x <= grid.GetLength(0) - 1; x++){
                rightCount++;
                if(grid[x,j] >= treeHeight){
                    visibleSides--;
                    break;
                }
            }
            for(int y = j - 1; y >= 0; y--){
                upCount++;
                if(grid[i,y] >= treeHeight){
                    visibleSides--;
                    break;
                }
            }
            for(int y = j + 1; y <= grid.GetLength(1) - 1; y++){
                downCount++;
                if(grid[i,y] >= treeHeight){
                    visibleSides--;
                    break;
                }
            }
            int scenicScore = leftCount * rightCount * upCount * downCount;
            if(scenicScore > highestScenicScore)
                highestScenicScore = scenicScore;
            if(visibleSides > 0)
                visibleTreeCount++;
        }
    }
}

Console.WriteLine(visibleTreeCount);
Console.WriteLine(highestScenicScore);
