namespace f1.Business

open System
open f1.Constants

type TradeParser() = 
    let DescriptionIndex = 3
    let TradeColumnNumber = 7
    let BadFileFormat = "Input file has corrupted data"

    let toShortInst inst =        
        match inst with
        | "BUY" -> InstBuy
        | "SELL" -> InstSell
        | _ -> inst

    member this.ParseTrade (raw : String) =        
        let values = raw.Split([| ' ' |], StringSplitOptions.RemoveEmptyEntries);

        // number of segments may vary due to description but can't be less than required number 7
        if values.Length < TradeColumnNumber then null
        else
            if String.IsNullOrEmpty(values.[0]) then        
                raise (FormatException(String.Format("Bad inst type for string '{0}'", raw)))
            
            let trade = Trade(Description = "123")
            
            trade