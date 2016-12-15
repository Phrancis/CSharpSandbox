<Query Kind="Program" />

void Main()
{
    string RLE_File = 
@"#N Gosper glider gun
#C This was the first gun discovered.
#C As its name suggests, it was discovered by Bill Gosper.
#O Bill Gosper Nov. 1970
x = 36, y = 9, rule = B3/S23
24bo$22bobo$12b2o6b2o12b2o$11bo3bo4b2o12b2o$2o8bo5bo3b2o$2o8bo3bob2o4b
obo$10bo5bo7bo$11bo3bo$12b2o! ";
    var parser = new RunLengthEncodedParser(RLE_File);
}

public class RunLengthEncodedParser
{
    private string _name = "";
    private List<string> _comments = new List<String>{ };
    private string _author = "";
    private int _size_X = 0;
    private int _size_Y = 0;
    private List<int> _ruleBirth = new List<int>{ };
    private List<int> _ruleSurvival = new List<int>{ };
    private string _patternRaw = "";
    private char[ , ] _pattern;
    
    // constants
    public readonly char DEAD_CELL = 'b';
    public readonly char LIVE_CELL = 'o';
    public readonly char DEAD_CELL_DISPLAY = '.';
    public readonly char LIVE_CELL_DISPLAY = 'o';
    
    /// <summary>
    /// Parser for Run Length Encode (RLE) strings / files. 
    /// More information: http://www.conwaylife.com/w/index.php?title=Run_Length_Encoded
    /// </summary>
    /// <param name="RLE_File">A string containing the raw RLE file to be parsed.</param>
    public RunLengthEncodedParser(string RLE_File)
    {
        var splitLines = RLE_File.Trim().Split( new string[] { Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries ).ToList();        
        PopulateAttributes(splitLines);
        PopulatePattern();
    }
    
    /// <summary>
    /// Parse RLE file lines, using its syntax to assign its various values to class attributes.
    /// </summary>
    /// <param name"RLE_File_Lines">A list of all lines from the file.</param>
    private void PopulateAttributes(List<string> RLE_File_Lines)
    {
        foreach (string line in RLE_File_Lines)
        {
            // name line
            if (line.Trim().StartsWith("#N", StringComparison.OrdinalIgnoreCase))
            {
                this._name = line.TrimStart("#Nn ".ToCharArray());
            }
            // comment line
            else if (line.Trim().StartsWith("#C", StringComparison.OrdinalIgnoreCase))
            {
                this._comments.Add(line.TrimStart("#Cc ".ToCharArray()));
            }
            // author line
            else if (line.Trim().StartsWith("#O", StringComparison.OrdinalIgnoreCase))
            {
                this._author = line.TrimStart("#Oo ".ToCharArray());
            }
            // pattern size and cellular automaton rules
            else if (line.Trim().StartsWith("x", StringComparison.OrdinalIgnoreCase))
            {
                //input example: "x = 36, y = 9, rule = B3/S23"
                //resulting Params { "36", "9", "B3/S23"}
                var Params = line.Split(',').Select(x => x.Replace(" ", "").Split('=')[1]).ToList();

                this._size_X = Int32.Parse(Params[0]);
                this._size_Y = Int32.Parse(Params[1]);
                var RulesParams = Params[2].Split('/');
                this._ruleBirth = RulesParams[0].Replace("B", "").Select(x => Int32.Parse(x.ToString())).ToList();
                this._ruleSurvival = RulesParams[1].Replace("S", "").Select(x => Int32.Parse(x.ToString())).ToList();
            }
            else 
            {
                //all other lines are part of the raw pattern and should be concatenated together
                //the pattern will be parsed separately in another method
                this._patternRaw += line.Trim();
            }
        }
        //DEBUG SECTION
        Console.WriteLine("_name: " + this._name);
        Console.WriteLine("_comments: ");
        Console.WriteLine(string.Join(Environment.NewLine, this._comments));
        Console.WriteLine("_author: " + this._author);
        Console.WriteLine("_size_X: " + this._size_X);
        Console.WriteLine("_size_Y: " + this._size_Y);
        Console.WriteLine("_ruleBirth: { " + string.Join(", ", this._ruleBirth) + " }");
        Console.WriteLine("_ruleSurvival: { " + string.Join(", ", this._ruleSurvival) + " }");
        Console.WriteLine("_patternRaw: " + this._patternRaw);
    }
    
    /// <summary>
    /// Parses and assigns the raw pattern from this instance into a matrix of characters (i.e. the pattern).
    /// </summary>
    private void PopulatePattern()
    {
        this._pattern = new char[this._size_Y, this._size_X];
        var patternRows = this._patternRaw.Replace("!", "").Split('$').ToList();

        if (patternRows.Count() != this._size_Y) 
        {
            throw new ArgumentException($"Specified Y value is {this._size_Y} but the raw pattern rendered as {patternRows.Count()} rows.");
        }
        else
        {
            string numString;
            int numCells;
            int currentCell;
            
            // go over each row
            for (int y = 0; y < this._size_Y; y++)
            {
                // initialize counters
                numString = string.Empty;
                numCells = 0;
                currentCell = 0;
                
                // go over characters in each row
                foreach (char c in patternRows[y])
                {
                    // check if we encounter a number
                    if (IsIntegerChar(c))
                    {
                        numString += c;
                    }
                    // if we encounter a non-number (cell), we need to start adding characters to the pattern
                    else
                    {
                        // if cell was not preceded by a number, then we use 1
                        if (numString == string.Empty)
                        {
                            numCells = 1;
                        }
                        // otherwise, we add as many as the numbers prior to it
                        else
                        {
                            numCells = Int32.Parse(numString);
                        }
                        // here we actually add the number of cells
                        int endCell = currentCell + numCells;
                        for (int x = currentCell; x < endCell; x++, currentCell++)
                        {
                            this._pattern[y, x] = c;
                        }
                        // finally, we reset the number string so we are ready to read another number
                        numString = string.Empty;
                    }
                }
                // fill in remaining empty spaces in row
                for (int x = currentCell; x < this._size_X; x++)
                {
                    this._pattern[y, x] = DEAD_CELL;
                }
            }
        }
        /*debug*/Console.WriteLine(GetHumanFriendlyPattern());    
    }
    
    /// <summary>
    /// Check if provided character is an integer.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>Returns true if character is an integer.</returns>
    private bool IsIntegerChar(char c)
    {
        return "0123456789".Contains(c);
    }

    public string GetHumanFriendlyPattern()
    {
        var matrix = this._pattern;
        string humanFriendlyPattern = string.Empty;
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i,j] == DEAD_CELL) 
                { 
                    humanFriendlyPattern += DEAD_CELL_DISPLAY; 
                }
                else if (matrix[i,j] == LIVE_CELL)
                {
                    humanFriendlyPattern += LIVE_CELL_DISPLAY;
                }
                else
                {
                    humanFriendlyPattern += '?';
                }
                
            }
            humanFriendlyPattern += Environment.NewLine;
        }
        return humanFriendlyPattern;
    }    
}