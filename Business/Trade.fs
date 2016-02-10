namespace f1.Business
open System

[<AllowNullLiteralAttribute>]
type Trade() =
    let InstBuy = "B"
    let InstSell = "S"

    let mutable description = ""
    
    member this.Description
        with get() = description
        and set(value) = description <- value
        
