using System;
using System.IO;
using System.Collections;

string text = File.ReadAllText("input.txt");
const int COUNT = 14;

List<char> current_letters = new List<char>();

for(int i = 0; i < text.Length; i++){
    char letter = text[i];
    if(current_letters.Exists(x => x == letter)){
        current_letters = current_letters.Skip(current_letters.IndexOf(letter) + 1).ToList();
    }
    current_letters.Add(letter);
    if(current_letters.Count() == COUNT){
        Console.WriteLine(i + 1);
        break;
    }
}
