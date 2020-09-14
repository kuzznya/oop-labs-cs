# INI file parser

[Source code](IniParser)

## Usage

Parser usage is very simple - just create a new instance of parser &
call Parse(filePath) method

```c#
var parser = new IniParser();
var config = parser.Parse(filename);
```

INI file format can be checked by using `CheckFileFormat(fileName)` method:

```c#
parser.CheckFileFormat(fileName);
```

After parsing configuration, the sections can be accessed by 
`Section(name)` method  
The properties are accessed by 
`GetInt(string key)`, `GetDouble(string key)`, `GetString(string key)` methods, by
`TryGetInt(string key, out int value)`, `TryGetDouble(string key, out double value)`, `TryGetString(string key, out string value)`, or by
`GetProperty(string key)` methods
