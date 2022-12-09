using System;
using System.IO;
using System.Collections;

string[] lines = File.ReadAllLines("input.txt");

Rope rope = new Rope();
Rope2 rope2 = new Rope2();

lines.ToList().ForEach(line => {
    string[] line_split = line.Split(' ');
    rope.Move(line_split[0][0], Int32.Parse(line_split[1]));
    rope2.Move(line_split[0][0], Int32.Parse(line_split[1]));
});

Console.WriteLine(Rope.MARKED_FIELDS.Count());
Console.WriteLine(Rope2.MARKED_FIELDS.Count());

class Rope{
    public static HashSet<(int, int)> MARKED_FIELDS = new HashSet<(int, int)>();
    private (int x, int y) headPos;
    private (int x, int y) tailPos;
    public Rope(){
        headPos = (0, 0);
        tailPos = (0, 0);
        MARKED_FIELDS.Add((0,0));
    }
    
    public void Move(char direction, int steps){
        for(int i = 0; i < steps; i++){
            switch(direction){
            case 'L':
                headPos.x = headPos.x - 1;
                break;
            case 'R':
                headPos.x = headPos.x + 1;
                break;
            case 'U':
                headPos.y = headPos.y - 1;
                break;
            case 'D':
                headPos.y = headPos.y + 1;
                break;
            }
            int stepX = headPos.x - tailPos.x;
            int stepY = headPos.y - tailPos.y;

            if (!(Math.Abs(stepX) <= 1 && Math.Abs(stepY) <= 1)){
                tailPos.x += Math.Sign(stepX);
                tailPos.y += Math.Sign(stepY);
             }
             MARKED_FIELDS.Add(tailPos);

        
        }
    }
}

class Rope2{
    public static HashSet<(int, int)> MARKED_FIELDS = new HashSet<(int, int)>();
    private (int x, int y)[] ropeKnots = new (int, int)[10];
    public Rope2(){
        for(int i = 0; i < 10; i++){
            ropeKnots[i] = (0, 0);
        }
        MARKED_FIELDS.Add((0,0));
    }

    public void Move(char direction, int steps){
        for(int i = 0; i < steps; i++){
            switch(direction){
                case 'L':
                    ropeKnots[0].x = ropeKnots[0].x - 1;
                    break;
                case 'R':
                    ropeKnots[0].x = ropeKnots[0].x + 1;
                    break;
                case 'U':
                    ropeKnots[0].y = ropeKnots[0].y - 1;
                    break;
                case 'D':
                    ropeKnots[0].y = ropeKnots[0].y + 1;
                    break;
            }
            for(int k = 1; k < ropeKnots.Length; k++){
                int stepX = ropeKnots[k-1].x - ropeKnots[k].x;
                int stepY = ropeKnots[k-1].y - ropeKnots[k].y;

                if (!(Math.Abs(stepX) <= 1 && Math.Abs(stepY) <= 1)){
                    ropeKnots[k].x += Math.Sign(stepX);
                    ropeKnots[k].y += Math.Sign(stepY);
                }
            }
            MARKED_FIELDS.Add(ropeKnots[^1]);
        }
        
        
    }

}
