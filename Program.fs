module f1.Main

open System
open System.IO
open f1.Business

[<EntryPoint>]
let main args =
    let filesPath = Environment.CurrentDirectory;
    let inputFilePath = Path.Combine(filesPath, "quiz.input")    
    let outputFilePath = Path.Combine(filesPath, "quiz.solution");
    try
        if not (File.Exists(inputFilePath))
        then
            let msg = String.Format("Cannot find input file '{0}'. Make sure to place the executable to the directory with the quiz.input", inputFilePath) 
            raise (FileNotFoundException(msg))
            
        use tradesReader = new TradeFileReader(inputFilePath)
        for x in tradesReader.ReadLines do
            Console.WriteLine("f {0}", x.ToString())
        
        Console.WriteLine("File '{0}' created", outputFilePath);
    with
        | exc -> printfn "%s" exc.Message
    0
