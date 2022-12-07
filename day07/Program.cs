using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");


Node workingNode = new Node("/", null);
Node firstNode = workingNode;

foreach (string line in lines.Skip(1)){
    if(line[0] == '$'){//command
        string[] command_split = line.Split(' ');
        if(command_split[1] == "cd"){//cd
            if(command_split[2] == ".."){//cd ..
                workingNode = workingNode.Parent;
            }else{//cd whatever
                Node new_folder = new Node(command_split[2], workingNode);
                workingNode.AddChild(new_folder);
                workingNode = new_folder;
            }  
        }
    }else{//file/dir
        string[] line_split = line.Split(' ');
        if(line_split[0] != "dir"){//file
            int size = Int32.Parse(line_split[0]);
            workingNode.AddChild(new Node(line_split[1], workingNode, size));
        }
    }
}

int currentSize = 0;
void SumUpSizes(Node node){
    if(node.Children.Count > 0){
        foreach(Node child in node.Children){
            SumUpSizes(child);
        }
    }
    if(node.GetSizeOfAll() <= 100000 && node.Size == 0){//nie dodawac plikow TYLKO FOLDERY!!!
        currentSize += node.GetSizeOfAll();
    }
}

SumUpSizes(firstNode);
Console.WriteLine(currentSize);

int TOTAL_DISK_SPACE = 70000000;
int NEEDED_DISK_SPACE = 30000000;

int free_space = TOTAL_DISK_SPACE - firstNode.GetSizeOfAll();
int space_needed_to_be_freed = NEEDED_DISK_SPACE - free_space;

int best_size = int.MaxValue;
void FindBestFolder(Node node){
    if(node.Children.Count > 0){
        foreach(Node child in node.Children){
            FindBestFolder(child);
        }
    }
    int size = node.GetSizeOfAll();
    if(size >= space_needed_to_be_freed && size < best_size){
        best_size = size;
    }
}
FindBestFolder(firstNode);
Console.WriteLine(best_size);


class Node{
    private string name;
    private List<Node> children;
    private int size;
    private Node parent;

    public string Name { get => name;}
    internal List<Node> Children { get => children; }
    public int Size { get => size; }
    internal Node Parent { get => parent; }

    public Node(string name, Node parent, int size = 0){
        children = new List<Node>();
        this.name = name;
        this.size = size;
        this.parent = parent;
    }

    public void AddChild(Node child){
        children.Add(child);
    }

    public int GetSizeOfAll(){
        int ret = size;
        foreach(Node child in children){
            ret += child.GetSizeOfAll();
        }
        return ret;
    }
}
