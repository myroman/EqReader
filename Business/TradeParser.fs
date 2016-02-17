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
            let shortInst = values.[0]
            
            let getLastIndex () =
                let rec findInd i = 
                    if (i >= values.Length) then
                        raise(FormatException("Bad format: a description didn't contain STK terminator"))                                        
                    match values.[i] with
                    | "STK" -> i
                    | _ -> findInd (i + 1)
                findInd DescriptionIndex
                
            let lastDescIndex = getLastIndex()
            let getDescr lastDescIndex =                
                if (lastDescIndex > values.Length - DescriptionIndex) then                
                    raise(FormatException(String.Format("Not enough columns after description for string '{0}'", raw)))                
                String.Join(" ", values, DescriptionIndex, lastDescIndex + 1 - DescriptionIndex);
                
            let parseIntUnsafe raw =
                match Int32.TryParse(raw) with
                | (true, int) -> int
                | _ -> raise(FormatException(BadFileFormat))
                
            let parseDecimalUnsafe raw =
                match Decimal.TryParse(raw) with
                | (true, value) -> value
                | _ -> raise(FormatException(BadFileFormat))
                
            let trade = Trade(
                            shortInst, 
                            values.[1], 
                            values.[2], 
                            getDescr lastDescIndex, 
                            parseIntUnsafe values.[lastDescIndex + 1], 
                            parseDecimalUnsafe values.[lastDescIndex + 2], 
                            parseDecimalUnsafe values.[lastDescIndex + 3], 
                            0M)
            trade