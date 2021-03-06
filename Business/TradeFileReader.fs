namespace f1.Business

open System
open System.Collections.Generic
open System.IO

type TradeFileReader(inputFilePath:string) =    
    let UserTimeFormat = "Customized report for '{0}' ({1})";
    let streamReader = new StreamReader(inputFilePath)
    
    let tradeParser = TradeParser()
    
    let isNull (x:Object) =         
        obj.ReferenceEquals (x, null)
        
    let rec loop n =
        if n < 2 then
            let line = streamReader.ReadLine()
            loop(n + 1)        
    let readTrade () =
        let tradeRaw = streamReader.ReadLine()
        if isNull(tradeRaw) || tradeRaw.Trim() = String.Empty then null else tradeParser.ParseTrade(tradeRaw)
        
    let formatTrade (trade:Trade) =
        let formatTrade = "{0, -2} {1, 6} {2} {3, -20} {4, 10} {5, 6} {6, 10} {7, 14} {8, 14}"
        String.Format(formatTrade,
            trade.Inst,
            trade.Symbol,
            trade.Cusip,
            trade.Description,
            trade.Price.ToString(),//"F4"
            trade.Shares,
            trade.Commission.ToString(),
            trade.Gross.ToString(),
            trade.Net.ToString())
            
    member this.ReadLines = 
        seq {
            loop 0
            
            yield String.Format(UserTimeFormat, Environment.UserName, DateTime.Now.ToString("MM/d/yy HH:mm"));
            yield "BS Symbol    Cusip Description            Price($) Shares Commission       Gross($)         Net($)"
            yield "--+------+--------+--------------------+----------+------+----------+--------------+--------------"
            
            let trade:Trade ref = ref (readTrade())
            let cont = ref (not (isNull(!trade)))            
            while !cont do
            
                yield formatTrade(!trade)
                trade := readTrade()
                
                if isNull(!trade) then                    
                    cont := false                
        }
    interface System.IDisposable with 
        member this.Dispose() = 
            streamReader.Dispose()