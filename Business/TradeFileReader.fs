namespace f1.Business

open System
open System.Collections.Generic
open System.IO



type TradeFileReader(inputFilePath:string) =    
    let UserTimeFormat = "Customized report for '{0}' ({1})";
    let streamReader = new StreamReader(inputFilePath)
    let isNull (x:^T when ^T : not struct) = obj.ReferenceEquals (x, null)
    let rec loop n =
        if n < 2 then
            let line = streamReader.ReadLine()
            Console.WriteLine(line);
            loop(n + 1)
            
    member this.ReadLines = 
        seq {
            loop 0
            
            yield String.Format(UserTimeFormat, Environment.UserName, DateTime.Now.ToString("MM/d/yy HH:mm"));
            yield "BS Symbol    Cusip Description            Price($) Shares Commission       Gross($)         Net($)"
            yield "--+------+--------+--------------------+----------+------+----------+--------------+--------------"
        }