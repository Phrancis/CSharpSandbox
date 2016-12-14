<Query Kind="Program" />

void Main()
{
    string RLE_File = @"#N Gosper glider gun
#C This was the first gun discovered.
#c As its name suggests, it was discovered by Bill Gosper.
#O Bill Gosper Nov. 1970
x = 36, y = 9, rule = B3/S23
24bo$22bobo$12b2o6b2o12b2o$11bo3bo4b2o12b2o$2o8bo5bo3b2o$2o8bo3bob2o4b
obo$10bo5bo7bo$11bo3bo$12b2o! ";
    var parser = new RunLengthEncodedParser(RLE_File);
}

public class RunLengthEncodedParser
{
    private string _name = "";
    private IEnumerable<string> _comments = new List<String>{ };
    private string _author = "";
    private int _size_X = 0;
    private int _size_Y = 0;
    private List<int> _ruleBirth = new List<int>{ };
    private List<int> _ruleSurvival = new List<int>{ };
    private string _patternRaw = "";
    
    /// <summary>
    /// Parser for Run Length Encode (RLE) strings / files. 
    /// More information: http://www.conwaylife.com/w/index.php?title=Run_Length_Encoded
    /// </summary>
    public RunLengthEncodedParser(string RLE_File)
    {
        // based on http://stackoverflow.com/a/13437536/3626537
        var splitLines = RLE_File.Trim().Split( new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries ).ToList();
        // debug
        Console.WriteLine(splitLines);
        
        PopulateAttributes(splitLines);
    }
    
    /// <summary>
    /// Parse RLE file lines, using its syntax to assign its various values to class attributes.
    /// </summary>
    private void PopulateAttributes(List<string> RLE_File_Lines)
    {
        this._name = RLE_File_Lines.FirstOrDefault(line => 
            line.Trim()
                .StartsWith("#N", StringComparison.OrdinalIgnoreCase)
        ).TrimStart("#N ".ToCharArray());
        if (this._name == null) { this._name = ""; }
        // debug
        Console.WriteLine("_name: " + this._name);
        
        this._comments = RLE_File_Lines.Where(line => 
            line.Trim()
                .StartsWith("#C", StringComparison.OrdinalIgnoreCase)
        ).Select(line => 
            line.TrimStart("#Cc ".ToCharArray())
        );
        // debug
        Console.WriteLine("_comments: ");
        Console.WriteLine(this._comments);
    }
}